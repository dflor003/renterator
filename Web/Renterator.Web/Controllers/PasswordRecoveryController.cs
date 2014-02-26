using System.Linq;
using System.Web.Mvc;
using Renterator.Common;
using Renterator.Services.AppServices.Security;
using Renterator.Services.Dto;
using Renterator.Services.Infrastructure;
using Renterator.Services.Interfaces;
using Renterator.Web.Helpers;

namespace Renterator.Web.Controllers
{
    [AllowAnonymous]
    public partial class PasswordRecoveryController : Controller
    {
        private readonly IPasswordRecoveryService passwordService;

        public PasswordRecoveryController(IPasswordRecoveryService passwordService)
        {
            this.passwordService = Utils.NullArgumentCheck("passwordService", passwordService);
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            return View(Views.ForgotPassword);
        }

        [HttpGet]
        public virtual ActionResult PasswordReset(string token)
        {
            Result<PasswordResetInfo> isValidResult = passwordService.GetPasswordResetInfo(token);
            ////if (!isValidResult.IsSuccess)
            ////{
            ////    return View(Views.InvalidToken, isValidResult.Messages.Single());
            ////}

            return View(Views.PasswordReset, isValidResult.Value);
        }

        [HttpPost]
        public virtual JsonResult SendForgotPasswordEmail(string email)
        {
            Result result = passwordService.SendForgotPasswordEmail(email);
            return Json(result);
        }

        [HttpPost]
        public virtual JsonResult ResetPassword(string token, string email, string newPassword)
        {
            Result result = passwordService.ResetPassword(token, email, newPassword);
            return Json(result);
        }
    }
}
