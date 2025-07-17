using System.Text.Json.Serialization;

namespace Common.Domain.Models
{
    public class DatabaseHealthStatus : HealthCheckBase
    {
        [JsonPropertyName("connection_string")]
        public string ConnectionString { get; set; }

        [JsonPropertyName("response_time")] public TimeSpan ResponseTime { get; set; }

        [JsonPropertyName("database_name")] public string DatabaseName { get; set; }
    }
}