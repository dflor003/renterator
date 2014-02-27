using System.Web.Mvc;
using System.Web.Security;
using Renterator.Services.Dto;

namespace Renterator.Web.Controllers
{
    public partial class HomeController : Controller
    {
        public UserContext CurrentUser
        {
            get { return (UserContext)HttpContext.User; }
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            if (!CurrentUser.IsAdmin)
            {
                return RedirectToAction("Current", "Balances");
            }

            return RedirectToAction("Bills", "Balances");
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Login()
        {
            return View(Views.Login);
        }

        [HttpGet]
        public virtual ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction(MVC.Home.Login());
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult AccountSetup()
        {
            return View(Views.AccountSetup);
        }
    }
}
