using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class ApplicationHealthStatusResponse
{
	[Required] public string Version { get; set; }
	[Required] public string Environment { get; set; }
	[Required] public TimeSpan Uptime { get; set; }
	[Required] public long MemoryUsage { get; set; }
	[Required] public string Status { get; set; }
	[Required] public DateTime Timestamp { get; set; }
	[Required] public Dictionary<string, object> Details { get; set; }

	public ApplicationHealthStatusResponse(
		string version,
		string environment,
		TimeSpan uptime,
		long memoryUsage,
		string status,
		DateTime timestamp,
		Dictionary<string, object> details
	)
	{
		Version = version;
		Environment = environment;
		Uptime = uptime;
		MemoryUsage = memoryUsage;
		Status = status;
		Timestamp = timestamp;
		Details = details;
	}
}