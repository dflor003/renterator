using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Renterator.Common;
using Renterator.Services.AppServices.Security;

namespace Renterator.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class TenantSecuredAttribute : AuthorizeAttribute
    {
        private const string WebsiteCodeKey = "websiteCode";

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // Call into base
            base.OnAuthorization(filterContext);

            // Get user and website info needed to authenticate
            string userEmail, websiteCode;
            GetUserAndWebsiteCode(filterContext, out userEmail, out websiteCode);

            // Make sure values are populated
            if (string.IsNullOrWhiteSpace(userEmail) || string.IsNullOrWhiteSpace(websiteCode))
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
            
            // Authenticate to make sure that you have access to this service
            Utils.NullCheck("LoginService not set", AuthenticationService);
            ////if (!AuthenticationService.DoesUserHaveAccessToSite(userEmail, websiteCode))
            ////{
                filterContext.Result = new HttpUnauthorizedResult();
            ////}
        }

        public static void GetUserAndWebsiteCode(ControllerContext filterContext, out string userEmail, out string websiteCode)
        {
            // Error checks
            Utils.NullArgumentCheck("filterContext", filterContext);

            // Get tenant id from route
            Utils.NullCheck("Must have route data", filterContext.RouteData);
            Utils.NullCheck("Must have route values", filterContext.RouteData.Values);
            RouteData routeData = filterContext.RouteData;
            websiteCode = routeData.Values[WebsiteCodeKey] as string;

            // Get user from context
            Utils.NullCheck("No http context present", filterContext.HttpContext);
            Utils.NullCheck("No user found in context", filterContext.HttpContext.User);
            Utils.NullCheck("No user identity found", filterContext.HttpContext.User.Identity);
            HttpContextBase httpContext = filterContext.HttpContext;
            IIdentity user = httpContext.User.Identity;
            userEmail = !user.IsAuthenticated || string.IsNullOrWhiteSpace(user.Name) ? null : user.Name;
        }
    }
}
