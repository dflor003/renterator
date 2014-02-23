using System.Web.Http;
using Renterator.Services.AppServices.Security;
using Renterator.Services.Dto;
using Renterator.Services.Infrastructure;

namespace Renterator.Web.Controllers.Api
{
    [AllowAnonymous]
    [RoutePrefix("api")]
    public class AccountsController : ApiController
    {
        private readonly IAuthenticationService authenticationService;

        public AccountsController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("accounts/login")]
        public Result Login(LoginInfo loginInfo)
        {
            return authenticationService.Login(loginInfo);
        }

        [HttpPost]
        [Route("accounts/")]
        public Result CreateAccount(UserAccountCreationInfo userInfo)
        {
            return authenticationService.CreateAccount(userInfo);
        }
    }
}