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

            // Create configuration options
            var configOptions = new ConfigurationOptions
            {
                EndPoints = { _redisSettings.ConnectionString },
                AbortOnConnectFail = _redisSettings.AbortOnConnectFail,
                ConnectTimeout = _redisSettings.ConnectTimeout,
                SyncTimeout = _redisSettings.SyncTimeout
            };

            // Add password if provided
            if (!string.IsNullOrEmpty(_redisSettings.Password))
            {
                configOptions.Password = _redisSettings.Password;
            }

            // Connect to Redis
            _redis = ConnectionMultiplexer.Connect(configOptions);
            _db = _redis.GetDatabase(_redisSettings.Database);
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
            var redisSection = configuration.GetSection("Redis");

            redisSettings.ConnectionString = redisSection["ConnectionString"] ?? "localhost:6379";
            redisSettings.Database = int.Parse(redisSection["Database"] ?? "0");
            redisSettings.Password = redisSection["Password"] ?? "";
            redisSettings.AbortOnConnectFail = bool.Parse(redisSection["AbortOnConnectFail"] ?? "false");
            redisSettings.ConnectTimeout = int.Parse(redisSection["ConnectTimeout"] ?? "5000");
            redisSettings.SyncTimeout = int.Parse(redisSection["SyncTimeout"] ?? "5000");

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
