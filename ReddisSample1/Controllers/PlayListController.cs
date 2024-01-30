using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ReddisSample1.Models;
using ReddisSample1.Services;
using ReddisSample1.Utilities;

namespace ReddisSample1.Controllers
{
    public class PlayListController : Controller
    {
        private readonly MongoDBService _mongoDBService;
        private readonly ICacheService _cacheService;
        private static object _lock = new object();
        private readonly UpdateRedisCache _updateRedisCache;

        public PlayListController(MongoDBService mongoDBService,ICacheService cacheService, UpdateRedisCache updateRedisCache)
        {
            _mongoDBService = mongoDBService;
            _cacheService = cacheService;
            _updateRedisCache = updateRedisCache;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<List<Playlist>> Get() {
            var cacheData = _cacheService.GetData<List<Playlist>>("Get");
            if (cacheData != null)
            {
                return cacheData;
            }
            lock (_lock)
            {
                var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
                cacheData = _mongoDBService.GetAsync().Result;
                _cacheService.SetData<List<Playlist>>("Get", cacheData, expirationTime);
            }

            return cacheData;
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody] Playlist playlist) {
            await _mongoDBService.CreateAsync(playlist);
            //_updateRedisCache.UpdateKey("Get");
            _cacheService.RemoveData("Get");
            return new RedirectResult("https://localhost:44367/Get");

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AddToPlaylist(string id, [FromBody] string movieId) {
            await _mongoDBService.AddToPlaylistAsync(id, movieId);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) {
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }

    }
}
