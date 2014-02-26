using System;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Ninject;
using Renterator.Services.Dto;
using Renterator.Services.Interfaces;

namespace Renterator.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        [Inject]
        public IUserSessionService UserSessionService { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            FilterConfig.RegisterGlobalFilters(GlobalConfiguration.Configuration.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MappingConfig.RegisterMappings();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs evt)
        {
            // Get context for token, redirect if no match
            IUserSessionService sessionService = UserSessionService;
            string token = GetToken();
            UserContext userContext = token == null ? null : sessionService.GetLoggedInUser(token);

            if (userContext == null)
            {
                ClearToken();
                return;
            }

            Context.User = userContext;
            Thread.CurrentPrincipal = userContext;
        }

        private void ClearToken()
        {
            Context.User = null;
            Thread.CurrentPrincipal = null;

            FormsAuthentication.SignOut();
            this.Context.Request.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, string.Empty) { Expires = DateTime.Now.AddDays(-1) });
        }

        private string GetToken()
        {
            HttpCookie token = this.Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            return token == null || string.IsNullOrWhiteSpace(token.Value) ? null : token.Value;
        }
    }
}
