using System.Web.Optimization;
using Renterator.Web.Helpers;

namespace Renterator.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Bootstrap
            StyleBundle bootstrapBundle = new StyleBundle("~/bundles/css/bootstrap");
            bootstrapBundle.Include("~/Content/less/lib/bootstrap.less");
            bootstrapBundle.Transforms.Add(new LessTransform());
            bootstrapBundle.Transforms.Add(new CssMinify());
            bundles.Add(bootstrapBundle);

            // Other css libs
            bundles.Add(new StyleBundle("~/bundles/css/lib").Include(
                "~/Content/css/lib/ladda/ladda.css",
                "~/Scripts/lib/select2/select2.css",
                "~/Scripts/lib/select2/select2-bootstrap.css"));

            // Less files
            var siteLess = new StyleBundle("~/bundles/css/site").Include(
                    "~/Content/less/site.less");
            siteLess.Transforms.Add(new LessTransform());
            siteLess.Transforms.Add(new CssMinify());
            bundles.Add(siteLess);

            // Library js
            bundles.Add(new ScriptBundle("~/bundles/scripts/lib").Include(
                "~/Scripts/lib/jquery/jquery-{version}.js",
                "~/Scripts/lib/blockui/jquery.blockui.js",
                "~/Scripts/lib/jquery-deparam/jquery-deparam.js",
                "~/Scripts/lib/bootstrap/bootstrap.js",
                "~/Scripts/lib/knockout/knockout-{version}.debug.js",
                "~/Scripts/lib/knockout/knockout.validation.js",
                "~/Scripts/lib/knockout/koExternalTemplateEngine_all.js",
                "~/Scripts/lib/linqjs/linq.js",
                "~/Scripts/lib/ladda/spin.js",
                "~/Scripts/lib/ladda/ladda.js",
                "~/Scripts/lib/select2/select2.js"));

            // Other js
            bundles.Add(new ScriptBundle("~/bundles/scripts/src").Include(
                // Common files

                // View models
                ));
        }
    }
}
