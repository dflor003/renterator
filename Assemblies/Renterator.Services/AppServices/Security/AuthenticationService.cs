using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Security;
using AutoMapper;
using Renterator.Common;
using Renterator.DataAccess.Infrastructure;
using Renterator.DataAccess.Model;
using Renterator.Services.Dto;
using Renterator.Services.Infrastructure;

namespace Renterator.Services.AppServices.Security
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields

        private const double TokenExpirationHours = 4;

        private const string MsgLoginInfoNotEntered = @"Please enter a valid email and password.";
        private const string MsgInvalidUsernameOrPwd = @"Invalid email or password.";
        private const string MsgAccountWithEmailExists = @"An account with the given email already exists.";

        private readonly IDataAccessor dataAccessor;
        private readonly HttpContextBase context;
        private readonly Random random;

        #endregion

        #region Constructors

        public AuthenticationService(IDataAccessor dataAccessor)
            : this(dataAccessor, new HttpContextWrapper(HttpContext.Current), new Random())
        {
        }

        internal AuthenticationService(IDataAccessor dataAccessor, HttpContextBase context, Random randomGenerator)
        {
            this.dataAccessor = Utils.NullArgumentCheck("dataAccessor", dataAccessor);
            this.context = Utils.NullArgumentCheck("context", context);
            this.random = Utils.NullArgumentCheck("randomGenerator", randomGenerator);
        }

        #endregion

        #region Public Methods

        public Result CreateAccount(UserInfo userInfo)
        {
            User ignored;
            return CreateAccount(userInfo, out ignored);
        }

        public Result CreateAccount(UserInfo userInfo, out User user)
        {
            // Validate dto
            ValidationHelper.ValidateModel(userInfo);

            // Create user object
            user = Mapper.Map<User>(userInfo);
            user.PasswordHash = PasswordHashHelper.CreateHash(userInfo.Password);
            user.LastLoginDate = DateTime.Now;

            // Validate user
            ValidationHelper.ValidateModel(user);

            // Other validations
            string email = user.Email;
            if (dataAccessor.Users.Any(other => email == other.Email))
            {
                return new Result(false, new LogMessage(MessageType.Error, MsgAccountWithEmailExists));
            }

            // Do save
            dataAccessor.Create(user);
            dataAccessor.SaveChanges();

            return Login(new LoginInfo { Email = userInfo.Email, Password = userInfo.Password });
        }

        public Result Login(LoginInfo loginInfo)
        {
            // Check for null/empty
            if (loginInfo == null || string.IsNullOrWhiteSpace(loginInfo.Email) ||
                string.IsNullOrWhiteSpace(loginInfo.Password))
            {
                return new Result(false, new LogMessage(MessageType.Error, MsgLoginInfoNotEntered));
            }

            // Get matching pwd and hash from db
            var dbInfo =
                (from user in dataAccessor.Users
                 where user.Email == loginInfo.Email
                 select new
                 {
                     user.Email,
                     user.PasswordHash
                 }).FirstOrDefault();

            // Check match
            if (dbInfo == null || !PasswordHashHelper.ValidatePassword(loginInfo.Password, dbInfo.PasswordHash))
            {
                return new Result(false, new LogMessage(MessageType.Error, MsgInvalidUsernameOrPwd));
            }

            // Login succeeded, set forms cookie
            SetFormsAuthenticationCookie(dbInfo.Email);
            return new Result(true);
        }

        public string GenerateWebsiteCode()
        {
            const string TenantIdCharacters = @"abcdefghijklmnopqrstuvwxyz0123456789";
            const int TenantIdLength = 8;

            char[] resultChars = new char[TenantIdLength];
            for (int i = 0; i < resultChars.Length; i++)
            {
                resultChars[i] = TenantIdCharacters[random.Next(TenantIdCharacters.Length)];
            }

            return new string(resultChars);
        }

        public void Dispose()
        {
            dataAccessor.Dispose();
        }

        #endregion

        #region NonPublic Methods

        private void SetFormsAuthenticationCookie(string userName)
        {
            // Token values
            const int AspNetVersion = 4;
            const bool IsTokenPersistent = false;
            DateTime dateIssued = DateTime.Now;

            // Create authentication ticket
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                AspNetVersion,
                userName,
                dateIssued,
                dateIssued.AddHours(TokenExpirationHours),
                IsTokenPersistent,
                string.Empty, // Needs to be string.Empty, does not encrypt if it's null
                FormsAuthentication.FormsCookiePath);

            // Add cookie to response
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            context.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket));
        }

        #endregion
    }
}
