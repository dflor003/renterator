using System.Web;
using System.Web.Mvc;
using Ninject;
using Renterator.Services.Dto;
using Renterator.Services.Interfaces;

namespace Renterator.Web.Controllers
{
    public partial class BalancesController : Controller
    {
        private readonly IAccountBalanceService accountBalanceService;

        [Inject]
        public BalancesController(IAccountBalanceService accountBalanceService)
        {
            this.accountBalanceService = accountBalanceService;
        }

        public UserContext UserContext
        {
            get { return (UserContext)HttpContext.User; }
        }

        [HttpGet]
        public virtual ActionResult Current()
        {
            AccountBalanceView model = accountBalanceService.GetAccountBalanceView(UserContext.UserId);

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Bills()
        {
            return null;
        }
    }
}