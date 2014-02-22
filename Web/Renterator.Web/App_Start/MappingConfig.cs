using Renterator.Services;

namespace Renterator.Web
{
    public class MappingConfig
    {
        public static void RegisterMappings()
        {
            MappingHelper.InitializeMappings();
        }
    }
}