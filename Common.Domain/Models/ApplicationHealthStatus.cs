using System.Text.Json.Serialization;

namespace Common.Domain.Models
{
    public class ApplicationHealthStatus : HealthCheckBase
    {
        [JsonPropertyName("version")] public string Version { get; set; }

        [JsonPropertyName("environment")] public string Environment { get; set; }

        [JsonPropertyName("uptime")] public TimeSpan Uptime { get; set; }

        [JsonPropertyName("memory_usage")] public long MemoryUsage { get; set; }
    }
}