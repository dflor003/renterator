using System.Web.Optimization;

namespace Renterator.Web
{
    public static class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css/lib").Include(
                "~/Css/bootstrap/bootstrap.css",
                "~/Css/bootstrap/bootstrap-responsive.css",
                "~/Css/ladda/ladda.css",
                "~/Scripts/lib/select2/select2.css",
                "~/Scripts/lib/select2/select2-bootstrap.css"));

            var fmmAdminLess = new StyleBundle("~/bundles/css/fmm-admin").Include(
                    "~/Css/fmm/fmm.main.less",
                    "~/Css/fmm/fmm.admin.less");
            fmmAdminLess.Transforms.Add(new LessTransform());
            fmmAdminLess.Transforms.Add(new CssMinify());
            bundles.Add(fmmAdminLess);

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            var fmmMarketingLess = new StyleBundle("~/bundles/css/fmm-marketing").Include(
                    "~/Css/fmm/fmm.main.less",
                    "~/Css/fmm/fmm.marketing.less");
            fmmMarketingLess.Transforms.Add(new LessTransform());
            fmmMarketingLess.Transforms.Add(new CssMinify());
            bundles.Add(fmmMarketingLess);

            // Library js
            bundles.Add(new ScriptBundle("~/bundles/scripts/lib").Include(
                "~/Scripts/lib/jquery/jquery-{version}.js",
                "~/Scripts/lib/jqueryui/jquery-ui-{version}.js",
                "~/Scripts/lib/blockui/jquery.blockui.js",
                "~/Scripts/lib/jquery-deparam/jquery-deparam.js",
                "~/Scripts/lib/modernizr/modernizr-{version}.js",
                "~/Scripts/lib/bootstrap/bootstrap.js",
                "~/Scripts/lib/knockout/knockout-{version}.debug.js",
                "~/Scripts/lib/knockout/knockout.validation.js",
                "~/Scripts/lib/knockout/koExternalTemplateEngine_all.js",
                "~/Scripts/lib/linqjs/linq.js",
                "~/Scripts/lib/ladda/spin.js",
                "~/Scripts/lib/ladda/ladda.js",
                "~/Scripts/lib/select2/select2.js"));

            // Fmm js
            bundles.Add(new ScriptBundle("~/bundles/scripts/fmm").Include(
                // Common files
                "~/Scripts/fmm/common/Utils.js",
                "~/Scripts/fmm/common/ServiceHelper.js",
                "~/Scripts/fmm/common/BindingHandlers.js",
                "~/Scripts/fmm/common/MessageLog.js",
                "~/Scripts/fmm/common/BaseViewModel.js",

                // View models
                "~/Scripts/fmm/viewmodel/security/Login.js",
                "~/Scripts/fmm/viewmodel/security/AccountSetup.js",
                "~/Scripts/fmm/viewmodel/security/ForgotPassword.js",
                "~/Scripts/fmm/viewmodel/security/PasswordReset.js"));
        }
    }
}
