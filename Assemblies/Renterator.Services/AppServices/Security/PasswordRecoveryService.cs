using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Renterator.Common;
using Renterator.Common.Caching;
using Renterator.DataAccess.Infrastructure;
using Renterator.DataAccess.Model;
using Renterator.Services.AppServices.Common;
using Renterator.Services.Dto;
using Renterator.Services.Infrastructure;
using Renterator.Services.Templates;

namespace Renterator.Services.AppServices.Security
{
    public class PasswordRecoveryService : IPasswordRecoveryService
    {
        #region Fields

#if !DEBUG
        // Used in release mode
        private const string MsgSmtpError = @"A critical error occurred while resetting your password. Please contact support."; 
#endif
        private const string MsgInvalidEmail = @"The email is invalid or the account is unavailable for login at this time. Please contact support for assistance.";
        private const string MsgEmailSent = @"An email has been sent to your email address. Please consult the email for further instructions on resetting your password.";
        private const string MsgInvalidResetToken = @"The password reset token you have provide is not valid or has expired. Please try again or contact support.";
        private const string MsgPasswordResetSuccess = @"Your password has been successfully change. You will now be redirected to the login page.";

        private const string FromEmail = @"no-reply@5minutemobile.com";
        private const string FromDisplayName = @"5 Minute Mobile";
        private const string EmailSubject = "5 Minute Mobile - Password Assistance";
        private const double EmailExpirationMinutes = 10;

        private readonly IDataAccessor dataAccessor;
        private readonly IEmailService emailService;
        private readonly ICache cache;
        private readonly IHttpRuntimeHelper runtime;

        #endregion

        #region Constructors

        public PasswordRecoveryService(IDataAccessor dataAccessor, IEmailService emailService, ICache cache, IHttpRuntimeHelper runtime)
        {
            this.dataAccessor = Utils.NullArgumentCheck("dataAccessor", dataAccessor);
            this.emailService = Utils.NullArgumentCheck("emailService", emailService);
            this.cache = Utils.NullArgumentCheck("cache", cache);
            this.runtime = Utils.NullArgumentCheck("runtime", runtime);
        }

        #endregion

        #region Public Methods

        public Result SendForgotPasswordEmail(string email)
        {
            // Error checks
            Utils.NullArgumentCheck("email", email);

            // Check for email's existence
            var userInfo =
                (from user in dataAccessor.Users
                 where user.Email == email
                 select new { user.Email, user.FirstName, user.LastName })
                    .FirstOrDefault();
            if (userInfo == null)
            {
                return new Result(false, new LogMessage(MessageType.Error, MsgInvalidEmail));
            }

            // Get email url and body
            string linkUrl = GenerateForgotPasswordLink(userInfo.Email);
            string emailBody = GenerateForgotPasswordEmailBody(linkUrl);

            // Build the email
            string userFullName = string.Format("{0} {1}", userInfo.FirstName, userInfo.LastName);
            MailMessage message = new MailMessage
            {
                From = new MailAddress(FromEmail, FromDisplayName),
                To = { new MailAddress(userInfo.Email, userFullName) },
                Subject = EmailSubject,
                Body = emailBody,
                IsBodyHtml = true
            };

            // Send the email
            try
            {
                emailService.Send(message);
            }
            catch (Exception ex)
            {
#if DEBUG
                string resultMessage = string.Format("{0} \r\n --- \r\n{1}", ex.Message, ex.StackTrace);
#else
                string resultMessage = MsgSmtpError;
#endif
                return new Result(false, new LogMessage(MessageType.Error, resultMessage));
            }

            // Success, return success message
            return new Result(true, new LogMessage(MessageType.Success, MsgEmailSent));
        }

        public Result<PasswordResetInfo> IsValidPasswordResetToken(string tokenString)
        {
            Guid resetToken;
            string userEmail;

            // Parse token to string and retrieve user email from cache, return error msg if anything fails
            if (string.IsNullOrWhiteSpace(tokenString) ||
                !Guid.TryParse(tokenString, out resetToken) ||
                !cache.TryGet(CacheKeys.ForgotPasswordGuid(resetToken), out userEmail))
            {
                return new Result<PasswordResetInfo>(null, false, new LogMessage(MessageType.Error, MsgInvalidResetToken));
            }

            // Valid, return success
            PasswordResetInfo resultInfo = new PasswordResetInfo { Token = resetToken, Email = userEmail };
            return new Result<PasswordResetInfo>(resultInfo, true);
        }

        public Result ResetPassword(string tokenString, string userEmail, string newPassword)
        {
            // Re-validate token first
            Result<PasswordResetInfo> isTokenValidResult = IsValidPasswordResetToken(tokenString);
            if (!isTokenValidResult.IsSuccess)
            {
                return isTokenValidResult;
            }

            // Remove token from cache
            cache.Remove(CacheKeys.ForgotPasswordGuid(isTokenValidResult.Value.Token));

            // Get the user object
            User user = dataAccessor.Users.FirstOrDefault(x => x.Email == userEmail);
            if (user == null)
            {
                return new Result(false, new LogMessage(MessageType.Error, MsgInvalidEmail));
            }

            // Reset password if we have a valid user
            user.PasswordHash = PasswordHashHelper.CreateHash(newPassword);
            dataAccessor.Update(user);
            dataAccessor.SaveChanges();

            // Return a success message
            return new Result(true, new LogMessage(MessageType.Success, MsgPasswordResetSuccess));
        }

        #endregion

        #region NonPublic Methods

        internal string GenerateForgotPasswordLink(string email)
        {
            // Generate a guid and insert it into cache
            Guid linkId = Guid.NewGuid();
            DateTimeOffset tokenExpiration = DateTimeOffset.Now.AddMinutes(EmailExpirationMinutes);
            cache.Set(CacheKeys.ForgotPasswordGuid(linkId), tokenExpiration, email);

            // Build the change password link
            string resultUrl = string.Format(@"~/PasswordRecovery/PasswordReset/?token={0}", linkId);
            return runtime.MakeAbsolutePath(resultUrl);
        }

        internal string GenerateForgotPasswordEmailBody(string linkUrl)
        {
            // Invoke T4 template
            ForgotPasswordEmailTemplate emailTemplate = new ForgotPasswordEmailTemplate
            {
                Session = new Dictionary<string, object>
                {
                    { "ForgotPasswordLinkUrl", linkUrl }
                }
            };
            emailTemplate.Initialize();

            return emailTemplate.TransformText();
        }

        #endregion
    }
}
