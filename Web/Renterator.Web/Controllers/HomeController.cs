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
