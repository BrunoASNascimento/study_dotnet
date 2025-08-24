using System.Text.Encodings.Web;
using System.Text.Json;
using RedisStudy.InfrastructureRedis;

namespace RedisStudy.Services
{
    public class AddHash
    {
        private readonly string _key;
        private readonly ConnectionRedis _connectionRedis;

        public AddHash(string key, ConnectionRedis connectionRedis)
        {
            _key = key ?? throw new ArgumentNullException(nameof(key));
            _connectionRedis = connectionRedis ?? throw new ArgumentNullException(nameof(connectionRedis));
        }

        public void Execute()
        {
            // Get database connection
            var db = _connectionRedis.GetDatabase();

            // Generate a unique field name
            string fieldName = "field_" + Guid.NewGuid().ToString("N")[..6];

            // Create the data to store
            var fieldValues = new Dictionary<string, string>
            {
                { fieldName, Guid.NewGuid().ToString() }
            };

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = false
            };

            // Serialize the data
            string serializedData = JsonSerializer.Serialize(fieldValues, options);

            // Store in Redis hash
            db.HashSet(_key, fieldName, serializedData);

            // Log the operation
            Console.WriteLine($"Stored in hash '{_key}' field '{fieldName}' = {serializedData}");
        }



    }
}
