using RedisStudy.InfrastructureRedis;
using RedisStudy.Services;

class Program
{
    private readonly ConnectionRedis _connectionRedis;

    public Program()
    {
        _connectionRedis = new ConnectionRedis();
    }

    static void Main(string[] args)
    {
        var program = new Program();

        Console.WriteLine("=== Redis Configuration Test ===");

        // Display Redis configuration info
        var settings = program._connectionRedis.GetSettings();
        Console.WriteLine($"Connected to Redis with connection string: {settings.RedisConnectionString}");
        Console.WriteLine();



        // Upload a string to Redis
        var uploadString = new AddString("test", program._connectionRedis);
        uploadString.Execute();

        // Upload a hash to Redis
        var uploadHash = new AddHash("test2", program._connectionRedis);
        uploadHash.Execute();

        // Upload multiple lines to a hash in Redis
        var uploadHashMultiLines = new AddHashMultiLines("test3", program._connectionRedis);
        uploadHashMultiLines.Execute();
    }
}