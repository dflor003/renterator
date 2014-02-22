using Ninject.Modules;
using Renterator.Common.Caching;
using Renterator.Services.AppServices.Common;
using Renterator.Services.AppServices.Security;

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
        }
    }
}
