using Microsoft.Extensions.Options;
using ReddisSample1.Models;
using StackExchange.Redis;

namespace ReddisSample1.Utilities
{
    public class ConnectionHelper
    {
         public ConnectionHelper(IOptions<ReddisDbSettings> reddisDbSettings)
        {
            ConnectionHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() => {
                return ConnectionMultiplexer.Connect(Environment.GetEnvironmentVariable("RedisConnection", EnvironmentVariableTarget.Process));
            });
        }
        private static Lazy<ConnectionMultiplexer> lazyConnection;
        public  ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
