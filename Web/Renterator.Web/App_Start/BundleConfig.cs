using System.Web.Optimization;

namespace Renterator.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Bootstrap
            bundles.Add(new StyleBundle("~/bundles/css/bootstrap").Include(
                "~/Content/less/lib/bootstrap/bootstrap.css"));

            // Other css libs
            bundles.Add(new StyleBundle("~/bundles/css/lib").Include(
                "~/Content/css/lib/ladda/ladda.css",
                "~/Scripts/lib/select2/select2.css",
                "~/Scripts/lib/select2/select2-bootstrap.css",
                "~/Content/css/lib/moment-datepicker/datepicker.css"));

            // Less files
            bundles.Add(new StyleBundle("~/bundles/css/site").Include(
                "~/Content/less/src/site.css"));

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
                "~/Scripts/lib/select2/select2.js",
                "~/Scripts/lib/moment/moment.js",
                "~/Scripts/lib/moment-datepicker/moment-datepicker.js",
                "~/Scripts/lib/moment-datepicker/moment-datepicker-ko.js"));

            // Other js
            bundles.Add(new ScriptBundle("~/bundles/scripts/src").Include(
                // Common files
                "~/Scripts/src/common/utils.js",
                "~/Scripts/src/common/message-log.js",
                "~/Scripts/src/common/service-helper.js",
                "~/Scripts/src/common/dialog-helper.js",
                "~/Scripts/src/common/binding-handlers.js",

                // View models
                "~/Scripts/src/viewmodels/login-form.js",
                "~/Scripts/src/viewmodels/current-balance.js",
                "~/Scripts/src/viewmodels/bills-view.js"
                ));
        }
    }
}
