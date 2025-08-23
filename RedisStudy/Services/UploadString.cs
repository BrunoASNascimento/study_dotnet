using RedisStudy.InfrastructureRedis;

namespace RedisStudy.Services
{
    public class UploadString
    {
        private readonly string _key;
        private readonly ConnectionRedis _connectionRedis;

        public UploadString(string key, ConnectionRedis connectionRedis)
        {
            _key = key;
            _connectionRedis = connectionRedis;
        }

        public void Execute()
        {


            // Generate random data
            string randomValue = Guid.NewGuid().ToString();

            // Get database connection
            var db = _connectionRedis.GetDatabase();

            // Set key with the random value
            db.StringSet(_key, randomValue);

            Console.WriteLine($"Key '{_key}' set with value: {randomValue}");
        }
    }
}
