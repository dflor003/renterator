using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Renterator.Services.Infrastructure;

namespace Renterator.Web.Helpers
{
    public class HandleAjaxErrorAttribute : System.Web.Mvc.IExceptionFilter, System.Web.Http.Filters.IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            string message = filterContext.HttpContext.IsDebuggingEnabled
                ? string.Format("An error has occurred: {0}", filterContext.Exception.Message)
                : "A critical error has occurred.";

            filterContext.Result = new JsonResult { Data = new Result(false, new LogMessage(MessageType.Error, message)) };
            filterContext.ExceptionHandled = true;
        }

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public bool AllowMultiple
        {
            get { return false; }
        }
    }
}
