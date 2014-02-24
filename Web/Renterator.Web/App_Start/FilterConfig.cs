using System.Web.Http.Filters;
using System.Web.Mvc;
using Renterator.Web.Helpers;

namespace Renterator.Web
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new HandleAjaxErrorAttribute());
            filters.Add(new AuthorizeAttribute());
        }

        public static void RegisterGlobalFilters(HttpFilterCollection filters)
        {
            filters.Add(new HandleAjaxErrorAttribute());
        }
    }
}
