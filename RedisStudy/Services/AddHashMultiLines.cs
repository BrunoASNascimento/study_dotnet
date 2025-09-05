using System.Collections.Concurrent;
using System.Diagnostics;
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

        public async Task ExecuteAsync()
        {
            // Get database connection
            var db = _connectionRedis.GetDatabase();

            ConcurrentDictionary<string, MyObjectTest> allFieldValues = new ConcurrentDictionary<string, MyObjectTest>();
            ConcurrentDictionary<string, MyObjectTest> allFieldValues2 = new ConcurrentDictionary<string, MyObjectTest>();
            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = 100;


            Parallel.For(0, 1_000_000, i =>
            {
                // Create the data to store
                var fieldValues = new MyObjectTest()
                {
                    fieldValue = Guid.NewGuid().ToString()
                };
                allFieldValues.TryAdd($"data-1-{fieldValues.fieldValue}", fieldValues);
                allFieldValues2.TryAdd($"data-2-{fieldValues.fieldValue}", fieldValues);
            });



            // Convert to HashEntry array
            var hashEntries = allFieldValues
                .Select(itm => new HashEntry(itm.Key, JsonConvert.SerializeObject(itm.Value)))
                .ToArray();

            // Convert to HashEntry array
            var hashEntries2 = allFieldValues2
                .Select(itm => new HashEntry(itm.Key, JsonConvert.SerializeObject(itm.Value)))
                .ToArray();

            // Send in chunks 
            const int chunkSize = 10_000;
            List<Task> tasks = new List<Task>();

            var runTime = Stopwatch.StartNew();
            for (int i = 0; i < hashEntries.Length; i += chunkSize)
            {
                int currentIndex = i;
                tasks.Add(Task.Run(async () =>
                {
                    var chunk = hashEntries.Skip(currentIndex).Take(chunkSize).ToArray();
                    await db.HashSetAsync(_key, chunk);
                }));

            }
            await Task.WhenAll(tasks);

            var getRunTime = runTime.ElapsedMilliseconds;
            //Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Time taken to upload multiple lines Task: {getRunTime} ms");


            var runTimeFor = Stopwatch.StartNew();
            for (int i = 0; i < hashEntries2.Length; i += chunkSize)
            {
                var chunk = hashEntries2.Skip(i).Take(chunkSize).ToArray();
                db.HashSet(_key, chunk);

            }
            var getRunTime2 = runTimeFor.ElapsedMilliseconds;
            Console.WriteLine($"Time taken to upload multiple lines For: {getRunTime2} ms");

            var percentage = ((double)(getRunTime2 - getRunTime) / getRunTime) * 100;
            Console.WriteLine($"Improvement of {percentage:F2}%");

        }
    }
}