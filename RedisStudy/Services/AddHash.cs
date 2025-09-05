using MessagePack;
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
            TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");
            // Get database connection
            var db = _connectionRedis.GetDatabase();

            // Generate a unique field name
            string fieldName = "field_" + Guid.NewGuid().ToString("N")[..6];
            DateTime saoPauloNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo"));
            DateTime utcSpecified = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

            // Create the data to store
            var fieldValues = new Dictionary<string, object>
            {
                { fieldName, Guid.NewGuid().ToString() },
                { "datetimeutc", DateTime.UtcNow },
                { "datetimelocal", DateTime.Now },
                { "datetimeutcSpecified",utcSpecified },
                { "datetimeutcconvertedlocal", saoPauloNow }
            };

            // Serialize the data using MessagePack
            byte[]? serializedData = Serialize(fieldValues);

            if (serializedData == null && fieldValues == null)
            {
                Console.WriteLine("Failed to serialize data");
                return;
            }
            else
            {
                // Store in Redis hash
                db.HashSet(_key, fieldName, serializedData);

                // Log the operation
                Console.WriteLine($"Stored in hash '{_key}' field '{fieldName}' = MessagePack serialized data ({serializedData.Length} bytes)");
                Console.WriteLine($"Original data: {string.Join(", ", fieldValues.Select(kv => $"{kv.Key}={kv.Value}"))}");
            }
        }

        private static byte[]? Serialize(object? obj)
        {
            //var options = MessagePackSerializerOptions.Standard.WithResolver(
            //CompositeResolver.Create(
            //    NativeDateTimeResolver.Instance,
            //    ContractlessStandardResolver.Instance));
            return obj == null ? null : MessagePackSerializer.Serialize(obj, MessagePack.Resolvers.ContractlessStandardResolver.Options);
            //return obj == null ? null : MessagePackSerializer.Serialize(obj, options);
        }
    }
}
