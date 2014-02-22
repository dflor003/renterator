using System.Web.Mvc;

namespace Renterator.Web.Controllers
{
    [System.Web.Mvc.AllowAnonymous]
    public partial class JasmineController : Controller
    {
        public virtual ActionResult Index()
        {
#if DEBUG
            return View("SpecRunner");
#else
            throw new HttpResponseException(HttpStatusCode.Forbidden);   
#endif
        }

    }
}
