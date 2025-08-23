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
        Console.WriteLine($"Connected to Redis at: {settings.ConnectionString}");
        Console.WriteLine($"Using database: {settings.Database}");
        Console.WriteLine($"Connection timeout: {settings.ConnectTimeout}ms");
        Console.WriteLine($"Sync timeout: {settings.SyncTimeout}ms");
        Console.WriteLine();

        // Test default Redis configuration
        Console.WriteLine("Testing Redis connection with configuration from appsettings.json...");

        // Create an instance of Program to access the ConnectionRedis

        // Upload a string to Redis
        //var uploadString = new UploadString("test");
        //uploadString.Execute();

        // Upload a hash to Redis
        var uploadHash = new UploadHash("test2", program._connectionRedis);
        uploadHash.Execute();
    }
}