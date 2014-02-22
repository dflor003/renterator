using Ninject.Modules;
using Renterator.DataAccess.Infrastructure;
using Renterator.DataAccess.Model;

namespace Renterator.DataAccess
{
    public class DataAccessNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataAccessor>().To<RenteratorDataAccessor>();
        }
    }
}
