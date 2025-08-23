namespace RedisStudy.InfrastructureRedis
{
    public class RedisSettings
    {
        public string? ConnectionString { get; set; }
        public int Database { get; set; } = 0;
        public string Password { get; set; } = "";
        public bool AbortOnConnectFail { get; set; } = false;
        public int ConnectTimeout { get; set; } = 5000;
        public int SyncTimeout { get; set; } = 5000;
    }
}