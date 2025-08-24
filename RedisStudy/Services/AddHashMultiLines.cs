using System.Collections.Concurrent;
using Newtonsoft.Json;
using RedisStudy.InfrastructureRedis;
using StackExchange.Redis;

namespace RedisStudy.Services
{
    public class AddHashMultiLines
    {
        private readonly string _key;
        private readonly ConnectionRedis _connectionRedis;

        public AddHashMultiLines(string key, ConnectionRedis connectionRedis)
        {
            _key = key ?? throw new ArgumentNullException(nameof(key));
            _connectionRedis = connectionRedis ?? throw new ArgumentNullException(nameof(connectionRedis));
        }

        public class MyObjectTest
        {

            public string fieldValue { get; init; }

        }

        public void Execute()
        {
            // Get database connection
            var db = _connectionRedis.GetDatabase();

            ConcurrentDictionary<string, MyObjectTest> allFieldValues = new ConcurrentDictionary<string, MyObjectTest>();
            //ParallelOptions options = new ParallelOptions();
            //options.MaxDegreeOfParallelism = 4;
            Parallel.For(0, 10_000_000, i =>
            {
                // Create the data to store
                var fieldValues = new MyObjectTest()
                {
                    fieldValue = Guid.NewGuid().ToString()
                };
                allFieldValues.TryAdd($"data-{fieldValues.fieldValue[..6]}", fieldValues);
            });

            // Convert to HashEntry array
            var hashEntries = allFieldValues
                .Select(itm => new HashEntry(itm.Key, JsonConvert.SerializeObject(itm.Value)))
                .ToArray();

            // Send in chunks 
            const int chunkSize = 100_000;
            for (int i = 0; i < hashEntries.Length; i += chunkSize)
            {
                var chunk = hashEntries.Skip(i).Take(chunkSize).ToArray();
                db.HashSet(_key, chunk);


            }
        }
    }
}