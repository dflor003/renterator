using System;
using System.Web;
using Renterator.Common;
using Renterator.Services.Interfaces;

namespace Renterator.Services.AppServices.Common
{
    public class HttpRuntimeHelper : IHttpRuntimeHelper
    {
        private readonly HttpContextBase httpContext;

        public HttpRuntimeHelper()
            : this(new HttpContextWrapper(HttpContext.Current))
        {
        }

        public HttpRuntimeHelper(HttpContextBase httpContext)
        {
            this.httpContext = Utils.NullArgumentCheck("httpContext", httpContext);
        }

        public string MakeAbsolutePath(string path)
        {
            // Error checks
            Utils.NullArgumentCheck("path", path);
            Utils.NullCheck("Could not get the HttpRequest from context", httpContext.Request);

            // Get the prefix (i.e. http://myhost:80) part of the url
            Uri requestUri = Utils.NullCheck("Could not get current url from request", httpContext.Request.Url);
            string originalUrl = Utils.NullCheck("Could not retrieve original url", requestUri.OriginalString);
            string pathAndQuery = Utils.NullCheck("Could not retrieve path and query", requestUri.PathAndQuery);
            string urlPrefix = originalUrl.Replace(pathAndQuery, string.Empty);

            // Get the path we need to append (i.e. /MyController/MyAction?whatever=whatever)
            string absolutePath = VirtualPathUtility.ToAbsolute(path);

            return string.Concat(urlPrefix, absolutePath);
        }
    }
}
