using System.Text.Json.Serialization;

namespace Common.Domain.Models
{
    public class HealthCheckBase
    {
        [JsonPropertyName("status")] public string Status { get; set; }

        [JsonPropertyName("timestamp")] public DateTime Timestamp { get; set; }

        [JsonPropertyName("details")] public Dictionary<string, object> Details { get; set; } = new();
    }
}