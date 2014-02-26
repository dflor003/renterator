using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Caching;
using System.Transactions;
using System.Web;
using System.Web.Security;
using AutoMapper;
using Renterator.Common;
using Renterator.Common.Caching;
using Renterator.DataAccess.Infrastructure;
using Renterator.DataAccess.Model;
using Renterator.Services.Dto;
using Renterator.Services.Infrastructure;
using Renterator.Services.Interfaces;
using ValidationException = Renterator.Services.Infrastructure.ValidationException;

namespace Renterator.Services.AppServices.Security
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields

        private const double TokenExpirationHours = 4;

        private const string MsgLoginInfoNotEntered = @"Please enter a valid email and password.";
        private const string MsgInvalidUsernameOrPwd = @"Invalid email or password.";
        private const string MsgAccountWithEmailExists = @"An account with the given email already exists.";

        private readonly ICache cache;
        private readonly IDataAccessor dataAccessor;
        private readonly HttpContextBase context;
        #endregion

        #region Constructors

        public AuthenticationService(IDataAccessor dataAccessor, ICache cache)
            : this(dataAccessor, cache, new HttpContextWrapper(HttpContext.Current))
        {
        }

        internal AuthenticationService(IDataAccessor dataAccessor, ICache cache, HttpContextBase context)
        {
            this.cache = Utils.NullArgumentCheck("cache", cache);
            this.dataAccessor = Utils.NullArgumentCheck("dataAccessor", dataAccessor);
            this.context = Utils.NullArgumentCheck("context", context);
        }

        #endregion

        #region Public Methods

        public Result CreateAccount(UserAccountCreationInfo userInfo)
        {
            User ignored;
            return CreateAccount(userInfo, out ignored);
        }

        public Result CreateAccount(UserAccountCreationInfo userInfo, out User user)
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
                throw new ValidationException(MsgAccountWithEmailExists);
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
                throw new ValidationException(MsgLoginInfoNotEntered);
            }

            // Get matching pwd and hash from db
            User userInfo =
                (from user in dataAccessor.Users.Include(u => u.Roles)
                 where user.Email == loginInfo.Email
                 select user).FirstOrDefault();

            // Check match
            if (userInfo == null || !PasswordHashHelper.ValidatePassword(loginInfo.Password, userInfo.PasswordHash))
            {
                throw new ValidationException(MsgInvalidUsernameOrPwd);
            }

            // Login succeeded, set forms cookie
            string token = SetAndReturnFormsAuthenticationCookie(userInfo.Email);
            
            // Set up cached user context
            UserContext userContext = new UserContext(token, userInfo);
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy { SlidingExpiration = TimeSpan.FromHours(1) };
            cache.Set(token, cacheItemPolicy, userContext);

            return new Result();
        }

        public void Dispose()
        {
            dataAccessor.Dispose();
        }

        #endregion

        #region NonPublic Methods

        private string SetAndReturnFormsAuthenticationCookie(string userName)
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

            return encryptedTicket;
        }

        #endregion
    }
}
