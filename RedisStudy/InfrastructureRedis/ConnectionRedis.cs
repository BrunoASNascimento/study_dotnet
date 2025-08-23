using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace RedisStudy.InfrastructureRedis
{
    public class ConnectionRedis
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;
        private readonly RedisSettings _redisSettings;

        public ConnectionRedis(IConfiguration? configuration = null)
        {
            // Load configuration
            _redisSettings = LoadRedisSettings(configuration);

            // Connect to Redis using the unified connection string
            _redis = ConnectionMultiplexer.Connect(_redisSettings.RedisConnectionString ?? "localhost:6379");
            _db = _redis.GetDatabase();
        }

        private RedisSettings LoadRedisSettings(IConfiguration? configuration)
        {
            if (configuration == null)
            {
                // Build configuration from appsettings.json
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                configuration = builder.Build();
            }

            var redisSettings = new RedisSettings();
            redisSettings.RedisConnectionString = configuration["RedisConnectionString"] ?? "localhost:6379";

            return redisSettings;
        }

        public IDatabase GetDatabase()
        {
            return _db;
        }

        public RedisSettings GetSettings()
        {
            return _redisSettings;
        }

        public void Dispose()
        {
            _redis?.Dispose();
        }
    }
}
