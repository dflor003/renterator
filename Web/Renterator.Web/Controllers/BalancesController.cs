using System.Web.Mvc;
using Ninject;
using Renterator.Services.Dto;
using Renterator.Services.Interfaces;

namespace Renterator.Web.Controllers
{
    public partial class BalancesController : Controller
    {
        private readonly IBillManagementService billManagementService;

        [Inject]
        public BalancesController(IBillManagementService billManagementService)
        {
            this.billManagementService = billManagementService;
        }

        public UserContext UserContext
        {
            get { return (UserContext)HttpContext.User; }
        }

        [HttpGet]
        public virtual ActionResult Current()
        {
            AccountBalanceView model = billManagementService.GetAccountBalanceView(UserContext.UserId);

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Bills()
        {
            return null;
        }
    }
}