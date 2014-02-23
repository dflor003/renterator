using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Renterator.Services.Infrastructure;

namespace Renterator.Web.Helpers
{
    public class HandleAjaxErrorAttribute : System.Web.Mvc.IExceptionFilter, System.Web.Http.Filters.IExceptionFilter
    {
        private const string ErrorDescription = "One or more errors ocurred";
        private static readonly Task TaskCompleted = Task.FromResult<object>(null);

        public bool AllowMultiple
        {
            get { return false; }
        }

        public void OnException(ExceptionContext filterContext)
        {

            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                return;
            }

            // Get the status code and log messages
            Tuple<HttpStatusCode, LogMessage[]> result = HandleException(filterContext.Exception);
            HttpStatusCode statusCode = result.Item1;
            LogMessage[] messages = result.Item2;

            // Setup the mvc result
            filterContext.Result = new JsonResult { Data = new { Messages = messages } };
            filterContext.ExceptionHandled = true;

            // Setup response
            HttpResponseBase response = filterContext.HttpContext.Response;
            response.StatusCode = (int)statusCode;
            response.StatusDescription = ErrorDescription;
        }

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            // Get the status code and log messages
            Tuple<HttpStatusCode, LogMessage[]> result = HandleException(actionExecutedContext.Exception);
            HttpStatusCode statusCode = result.Item1;
            LogMessage[] messages = result.Item2;

            // Generate error
            HttpResponseMessage errorResponse = actionExecutedContext.Request.CreateResponse(statusCode, new { Messages = messages });
            actionExecutedContext.Response = errorResponse;

            return TaskCompleted;
        }

        private static Tuple<HttpStatusCode, LogMessage[]> HandleException(Exception exception)
        {
            // Handle validation
            ValidationException validationException = exception as ValidationException;
            if (validationException != null)
            {
                return Tuple.Create(HttpStatusCode.BadRequest, validationException.Messages);
            }

            // Handle all other exceptions
            return Tuple.Create(HttpStatusCode.InternalServerError, new[] { new LogMessage(MessageType.Error, exception.Message) });
        }
    }
}
