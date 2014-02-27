﻿// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
public static partial class MVC
{
    public static Renterator.Web.Controllers.BalancesController Balances = new Renterator.Web.Controllers.T4MVC_BalancesController();
    public static Renterator.Web.Controllers.HomeController Home = new Renterator.Web.Controllers.T4MVC_HomeController();
    public static Renterator.Web.Controllers.JasmineController Jasmine = new Renterator.Web.Controllers.T4MVC_JasmineController();
    public static Renterator.Web.Controllers.PasswordRecoveryController PasswordRecovery = new Renterator.Web.Controllers.T4MVC_PasswordRecoveryController();
    public static T4MVC.SharedController Shared = new T4MVC.SharedController();
}

namespace T4MVC
{
}

namespace T4MVC
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class Dummy
    {
        private Dummy() { }
        public static Dummy Instance = new Dummy();
    }
}

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal partial class T4MVC_System_Web_Mvc_ActionResult : System.Web.Mvc.ActionResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_ActionResult(string area, string controller, string action, string protocol = null): base()
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
     
    public override void ExecuteResult(System.Web.Mvc.ControllerContext context) { }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}
[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal partial class T4MVC_System_Web_Mvc_JsonResult : System.Web.Mvc.JsonResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_JsonResult(string area, string controller, string action, string protocol = null): base()
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}



namespace Links
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static class Scripts {
        private const string URLPATH = "~/Scripts";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class lib {
            private const string URLPATH = "~/Scripts/lib";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class blockui {
                private const string URLPATH = "~/Scripts/lib/blockui";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string jquery_blockUI_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.blockUI.min.js") ? Url("jquery.blockUI.min.js") : Url("jquery.blockUI.js");
                public static readonly string jquery_blockUI_min_js = Url("jquery.blockUI.min.js");
            }
        
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class bootstrap {
                private const string URLPATH = "~/Scripts/lib/bootstrap";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string bootstrap_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/bootstrap.min.js") ? Url("bootstrap.min.js") : Url("bootstrap.js");
                public static readonly string bootstrap_min_js = Url("bootstrap.min.js");
            }
        
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class jasmine {
                private const string URLPATH = "~/Scripts/lib/jasmine";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string jasmine_html_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jasmine-html.min.js") ? Url("jasmine-html.min.js") : Url("jasmine-html.js");
                public static readonly string jasmine_rowtests_1_0_1_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jasmine-rowtests-1.0.1.min.js") ? Url("jasmine-rowtests-1.0.1.min.js") : Url("jasmine-rowtests-1.0.1.js");
                public static readonly string jasmine_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jasmine.min.css") ? Url("jasmine.min.css") : Url("jasmine.css");
                     
                public static readonly string jasmine_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jasmine.min.js") ? Url("jasmine.min.js") : Url("jasmine.js");
                public static readonly string jasmine_favicon_png = Url("jasmine_favicon.png");
            }
        
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class jquery {
                private const string URLPATH = "~/Scripts/lib/jquery";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string jquery_2_1_0_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery-2.1.0.min.js") ? Url("jquery-2.1.0.min.js") : Url("jquery-2.1.0.js");
                public static readonly string jquery_2_1_0_min_js = Url("jquery-2.1.0.min.js");
                public static readonly string jquery_2_1_0_min_map = Url("jquery-2.1.0.min.map");
            }
        
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class jquery_deparam {
                private const string URLPATH = "~/Scripts/lib/jquery-deparam";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string jquery_deparam_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery-deparam.min.js") ? Url("jquery-deparam.min.js") : Url("jquery-deparam.js");
                public static readonly string jquery_deparam_min_js = Url("jquery-deparam.min.js");
            }
        
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class knockout {
                private const string URLPATH = "~/Scripts/lib/knockout";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string knockout_3_0_0_debug_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/knockout-3.0.0.debug.min.js") ? Url("knockout-3.0.0.debug.min.js") : Url("knockout-3.0.0.debug.js");
                public static readonly string knockout_3_0_0_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/knockout-3.0.0.min.js") ? Url("knockout-3.0.0.min.js") : Url("knockout-3.0.0.js");
                public static readonly string knockout_validation_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/knockout.validation.min.js") ? Url("knockout.validation.min.js") : Url("knockout.validation.js");
                public static readonly string knockout_validation_min_js = Url("knockout.validation.min.js");
                public static readonly string koExternalTemplateEngine_all_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/koExternalTemplateEngine_all.min.js") ? Url("koExternalTemplateEngine_all.min.js") : Url("koExternalTemplateEngine_all.js");
                public static readonly string koExternalTemplateEngine_all_min_js = Url("koExternalTemplateEngine_all.min.js");
            }
        
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class ladda {
                private const string URLPATH = "~/Scripts/lib/ladda";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string ladda_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/ladda.min.js") ? Url("ladda.min.js") : Url("ladda.js");
                public static readonly string ladda_min_js = Url("ladda.min.js");
                public static readonly string spin_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/spin.min.js") ? Url("spin.min.js") : Url("spin.js");
                public static readonly string spin_min_js = Url("spin.min.js");
            }
        
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class linqjs {
                private const string URLPATH = "~/Scripts/lib/linqjs";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string linq_vsdoc_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/linq-vsdoc.min.js") ? Url("linq-vsdoc.min.js") : Url("linq-vsdoc.js");
                public static readonly string linq_jquery_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/linq.jquery.min.js") ? Url("linq.jquery.min.js") : Url("linq.jquery.js");
                public static readonly string linq_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/linq.min.js") ? Url("linq.min.js") : Url("linq.js");
                public static readonly string linq_min_js = Url("linq.min.js");
            }
        
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class modernizr {
                private const string URLPATH = "~/Scripts/lib/modernizr";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string modernizr_2_6_2_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/modernizr-2.6.2.min.js") ? Url("modernizr-2.6.2.min.js") : Url("modernizr-2.6.2.js");
            }
        
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class moment {
                private const string URLPATH = "~/Scripts/lib/moment";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string moment_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/moment.min.js") ? Url("moment.min.js") : Url("moment.js");
                public static readonly string moment_min_js = Url("moment.min.js");
            }
        
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class select2 {
                private const string URLPATH = "~/Scripts/lib/select2";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string select2_bootstrap_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/select2-bootstrap.min.css") ? Url("select2-bootstrap.min.css") : Url("select2-bootstrap.css");
                     
                public static readonly string select2_spinner_gif = Url("select2-spinner.gif");
                public static readonly string select2_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/select2.min.css") ? Url("select2.min.css") : Url("select2.css");
                     
                public static readonly string select2_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/select2.min.js") ? Url("select2.min.js") : Url("select2.js");
                public static readonly string select2_min_js = Url("select2.min.js");
                public static readonly string select2_png = Url("select2.png");
                public static readonly string select2x2_png = Url("select2x2.png");
            }
        
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class src {
            private const string URLPATH = "~/Scripts/src";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class common {
                private const string URLPATH = "~/Scripts/src/common";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string message_log_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/message-log.min.js") ? Url("message-log.min.js") : Url("message-log.js");
                public static readonly string service_helper_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/service-helper.min.js") ? Url("service-helper.min.js") : Url("service-helper.js");
                public static readonly string utils_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/utils.min.js") ? Url("utils.min.js") : Url("utils.js");
            }
        
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class viewmodels {
                private const string URLPATH = "~/Scripts/src/viewmodels";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string current_balance_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/current-balance.min.js") ? Url("current-balance.min.js") : Url("current-balance.js");
                public static readonly string login_form_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/login-form.min.js") ? Url("login-form.min.js") : Url("login-form.js");
            }
        
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class templates {
            private const string URLPATH = "~/Scripts/templates";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string MessageLog_tmpl_html = Url("MessageLog.tmpl.html");
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class tests {
            private const string URLPATH = "~/Scripts/tests";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string TestSetup_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/TestSetup.min.js") ? Url("TestSetup.min.js") : Url("TestSetup.js");
        }
    
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static partial class Bundles
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static partial class Scripts {}
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static partial class Styles {}
    }
}

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal static class T4MVCHelpers {
    // You can change the ProcessVirtualPath method to modify the path that gets returned to the client.
    // e.g. you can prepend a domain, or append a query string:
    //      return "http://localhost" + path + "?foo=bar";
    private static string ProcessVirtualPathDefault(string virtualPath) {
        // The path that comes in starts with ~/ and must first be made absolute
        string path = VirtualPathUtility.ToAbsolute(virtualPath);
        
        // Add your own modifications here before returning the path
        return path;
    }

    // Calling ProcessVirtualPath through delegate to allow it to be replaced for unit testing
    public static Func<string, string> ProcessVirtualPath = ProcessVirtualPathDefault;

    // Calling T4Extension.TimestampString through delegate to allow it to be replaced for unit testing and other purposes
    public static Func<string, string> TimestampString = System.Web.Mvc.T4Extensions.TimestampString;

    // Logic to determine if the app is running in production or dev environment
    public static bool IsProduction() { 
        return (HttpContext.Current != null && !HttpContext.Current.IsDebuggingEnabled); 
    }
}





#endregion T4MVC
#pragma warning restore 1591


