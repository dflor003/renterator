using System;
using Renterator.Common.Caching;

namespace Renterator.Services.Infrastructure
{
    internal static class CacheKeys
    {
        public static Func<Guid, string> ForgotPasswordGuid = guid => Cache.Key("ForgotPassword", guid);
    }
}
