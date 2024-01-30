using Microsoft.AspNetCore.Mvc;
using ReddisSample1.Services;

namespace ReddisSample1.Utilities
{
    public class UpdateRedisCache:ControllerBase
    {
        private readonly ICacheService _cacheService;
        public UpdateRedisCache(ICacheService cacheService)
        {
            _cacheService = cacheService;        }
        public RedirectResult UpdateKey(string key)
        {
            try
            {
                _cacheService.RemoveData(key);
                return new RedirectResult("https://localhost:44367/Get");
            }
            catch { return null; }
        }
    }
}
