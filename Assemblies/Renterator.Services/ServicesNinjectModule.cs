using Ninject.Modules;
using Renterator.Common.Caching;
using Renterator.Services.AppServices.Business;
using Renterator.Services.AppServices.Common;
using Renterator.Services.AppServices.Security;
using Renterator.Services.Interfaces;

namespace Renterator.Services
{
    public class ServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAuthenticationService>().To<AuthenticationService>();
            Bind<IPasswordRecoveryService>().To<PasswordRecoveryService>();
            Bind<IEmailService>().To<EmailService>();
            Bind<IHttpRuntimeHelper>().To<HttpRuntimeHelper>();
            Bind<ICache>().To<Cache>().InSingletonScope();
            Bind<IRoleService>().To<RoleService>();
            Bind<IUserSessionService>().To<UserSessionService>();
            Bind<IBillManagementService>().To<BillManagementService>();
        }
    }
}
