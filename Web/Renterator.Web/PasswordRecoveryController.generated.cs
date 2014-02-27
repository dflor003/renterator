// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Web.Mvc;
using T4MVC;
namespace Renterator.Web.Controllers
{
    public partial class PasswordRecoveryController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected PasswordRecoveryController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult PasswordReset()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PasswordReset);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SendForgotPasswordEmail()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SendForgotPasswordEmail);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult ResetPassword()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.ResetPassword);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public PasswordRecoveryController Actions { get { return MVC.PasswordRecovery; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "PasswordRecovery";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "PasswordRecovery";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string PasswordReset = "PasswordReset";
            public readonly string SendForgotPasswordEmail = "SendForgotPasswordEmail";
            public readonly string ResetPassword = "ResetPassword";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string PasswordReset = "PasswordReset";
            public const string SendForgotPasswordEmail = "SendForgotPasswordEmail";
            public const string ResetPassword = "ResetPassword";
        }


        static readonly ActionParamsClass_PasswordReset s_params_PasswordReset = new ActionParamsClass_PasswordReset();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_PasswordReset PasswordResetParams { get { return s_params_PasswordReset; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_PasswordReset
        {
            public readonly string token = "token";
        }
        static readonly ActionParamsClass_SendForgotPasswordEmail s_params_SendForgotPasswordEmail = new ActionParamsClass_SendForgotPasswordEmail();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SendForgotPasswordEmail SendForgotPasswordEmailParams { get { return s_params_SendForgotPasswordEmail; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SendForgotPasswordEmail
        {
            public readonly string email = "email";
        }
        static readonly ActionParamsClass_ResetPassword s_params_ResetPassword = new ActionParamsClass_ResetPassword();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ResetPassword ResetPasswordParams { get { return s_params_ResetPassword; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ResetPassword
        {
            public readonly string token = "token";
            public readonly string email = "email";
            public readonly string newPassword = "newPassword";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string ForgotPassword = "ForgotPassword";
                public readonly string Layout = "Layout";
                public readonly string PasswordReset = "PasswordReset";
            }
            public readonly string ForgotPassword = "~/Views/PasswordRecovery/ForgotPassword.cshtml";
            public readonly string Layout = "~/Views/PasswordRecovery/Layout.cshtml";
            public readonly string PasswordReset = "~/Views/PasswordRecovery/PasswordReset.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_PasswordRecoveryController : Renterator.Web.Controllers.PasswordRecoveryController
    {
        public T4MVC_PasswordRecoveryController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void PasswordResetOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string token);

        [NonAction]
        public override System.Web.Mvc.ActionResult PasswordReset(string token)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PasswordReset);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "token", token);
            PasswordResetOverride(callInfo, token);
            return callInfo;
        }

        [NonAction]
        partial void SendForgotPasswordEmailOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string email);

        [NonAction]
        public override System.Web.Mvc.JsonResult SendForgotPasswordEmail(string email)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SendForgotPasswordEmail);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "email", email);
            SendForgotPasswordEmailOverride(callInfo, email);
            return callInfo;
        }

        [NonAction]
        partial void ResetPasswordOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string token, string email, string newPassword);

        [NonAction]
        public override System.Web.Mvc.JsonResult ResetPassword(string token, string email, string newPassword)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.ResetPassword);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "token", token);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "email", email);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "newPassword", newPassword);
            ResetPasswordOverride(callInfo, token, email, newPassword);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
