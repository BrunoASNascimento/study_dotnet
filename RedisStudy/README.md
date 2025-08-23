# Redis Study - Configuration Example

This project demonstrates how to configure Redis connection using `appsettings.json` in a .NET 8 application.

## Configuration

The Redis connection settings are defined in `appsettings.json`:

```json
{
  "Redis": {
    "ConnectionString": "localhost:6379",
    "Database": 0,
    "Password": "",
    "AbortOnConnectFail": false,
    "ConnectTimeout": 5000,
    "SyncTimeout": 5000
  }
}
```

### Configuration Options

- **ConnectionString**: Redis server address and port (e.g., "localhost:6379")
- **Database**: Redis database number (0-15 typically)
- **Password**: Redis password (leave empty if no password required)
- **AbortOnConnectFail**: Whether to abort connection on initial connection failure
- **ConnectTimeout**: Connection timeout in milliseconds
- **SyncTimeout**: Synchronous operation timeout in milliseconds

## Usage

The `ConnectionRedis` class automatically reads configuration from `appsettings.json`:

```csharp
// Creates connection using appsettings.json
var connection = new ConnectionRedis();

// Get database instance
var db = connection.GetDatabase();

// Use Redis operations
db.StringSet("key", "value");
```

## Dependencies

- StackExchange.Redis
- Microsoft.Extensions.Configuration
- Microsoft.Extensions.Configuration.Json
- Microsoft.Extensions.Configuration.Binder

## Running the Application

1. Ensure Redis is running on localhost:6379 (or update appsettings.json)
2. Run the application: `dotnet run`
3. The application will test the Redis connection and display configuration details

## Environment-Specific Configuration

You can create environment-specific configuration files:

- `appsettings.Development.json`
- `appsettings.Production.json`
- `appsettings.Staging.json`

These will automatically override the base `appsettings.json` values based on the `DOTNET_ENVIRONMENT` or `ASPNETCORE_ENVIRONMENT` variable.