﻿
using Newtonsoft.Json;
using ReddisSample1.Utilities;
using StackExchange.Redis;
namespace ReddisSample1.Services
{
    public class CacheService : ICacheService
    {
        private IDatabase _db;
        private ConnectionHelper _connectionHelper;
        public CacheService(ConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
            ConfigureRedis();
        }
        private void ConfigureRedis()
        {
            _db = _connectionHelper.Connection.GetDatabase();
        }
        public T GetData<T>(string key)
        {
            var value = _db.StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }
        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
            return isSet;
        }
        public object RemoveData(string key)
        {
            bool _isKeyExist = _db.KeyExists(key);
            if (_isKeyExist == true)
            {
                return _db.KeyDelete(key);
            }
            return false;
        }
    }
}
