using System.Text.Encodings.Web;
using System.Text.Json;
using RedisStudy.InfrastructureRedis;

namespace RedisStudy.Services
{
    public class UploadHash
    {
        private readonly string _key;
        private readonly ConnectionRedis _connectionRedis;

        public UploadHash(string key, ConnectionRedis connectionRedis)
        {
            _key = key ?? throw new ArgumentNullException(nameof(key));
            _connectionRedis = connectionRedis ?? throw new ArgumentNullException(nameof(connectionRedis));
        }

        public void Execute()
        {
            // Get database connection
            var db = _connectionRedis.GetDatabase();

            // Generate a unique field name
            string fieldName = GenerateFieldName();

            // Create the data to store
            var fieldValues = new Dictionary<string, string>
            {
                { fieldName, Guid.NewGuid().ToString() }
            };

            // Serialize the data
            string serializedData = SerializeFieldValues(fieldValues);

            // Store in Redis hash
            db.HashSet(_key, fieldName, serializedData);

            // Log the operation
            Console.WriteLine($"Stored in hash '{_key}' field '{fieldName}' = {serializedData}");
        }

        private static string GenerateFieldName()
        {
            return "field_" + Guid.NewGuid().ToString("N")[..6];
        }

        private static string SerializeFieldValues(Dictionary<string, string> fieldValues)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = false
            };

            return JsonSerializer.Serialize(fieldValues, options);
        }
    }
}
