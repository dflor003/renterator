using Renterator.Common.Caching;
using Renterator.Services.Dto;

namespace Renterator.Services.Interfaces
{
    public interface IUserSessionService
    {
        UserContext GetLoggedInUser(string token);
    }

    internal class UserSessionService : IUserSessionService
    {
        private readonly ICache cache;

        public UserSessionService(ICache cache)
        {
            this.cache = cache;
        }

        public UserContext GetLoggedInUser(string token)
        {
            UserContext context;
            return !cache.TryGet(token, out context) ? null : context;
        }
    }
}
