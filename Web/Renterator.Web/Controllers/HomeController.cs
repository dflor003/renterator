using System.Web.Mvc;

namespace Renterator.Web.Controllers
{
    public partial class HomeController : Controller
    {
        [HttpGet]
        public virtual ActionResult Index()
        {
            return View(Views.Index);
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Login()
        {
            return View(Views.Login);
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult AccountSetup()
        {
            return View(Views.AccountSetup);
        }
    }
}
