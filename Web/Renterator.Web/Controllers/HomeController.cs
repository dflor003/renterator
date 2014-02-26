using System.Web.Mvc;
using System.Web.Security;

namespace Renterator.Web.Controllers
{
    public partial class HomeController : Controller
    {
        [HttpGet]
        public virtual ActionResult Index()
        {
            return RedirectToAction("Current", "Balances");
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
