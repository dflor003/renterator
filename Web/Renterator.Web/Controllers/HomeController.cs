using System.Web.Mvc;
using Renterator.Common;
using Renterator.Services.AppServices.Security;
using Renterator.Services.Dto;
using Renterator.Services.Infrastructure;
using Renterator.Web.Helpers;

namespace Renterator.Web.Controllers
{
    [AllowAnonymous]
    public partial class HomeController : Controller
    {
        private readonly IAuthenticationService authenticationService;

        public HomeController(IAuthenticationService authenticationService)
        {
            this.authenticationService = Utils.NullArgumentCheck("authenticationService", authenticationService);
        }

        [HttpPost]
        public virtual JsonResult Login(LoginInfo loginInfo)
        {
            Result result = authenticationService.Login(loginInfo);
            return Json(result);
        }

        [HttpPost]
        public virtual JsonResult CreateAccount(UserInfo userInfo, string businessName, string businessType)
        {
            Result creationResult = authenticationService.CreateAccount(userInfo);
            return Json(creationResult);
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            return View(Views.Index);
        }

        [HttpGet]
        public virtual ActionResult AccountSetup()
        {
            return View(Views.AccountSetup);
        }
    }
}
