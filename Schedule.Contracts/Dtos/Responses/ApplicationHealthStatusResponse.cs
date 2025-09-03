using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class ApplicationHealthStatusResponse
{
	public ApplicationHealthStatusResponse(
		string version,
		string environment,
		TimeSpan uptime,
		long memoryUsage,
		string status,
		DateTime timestamp,
		Dictionary<string, object> details)
	{
		Version = version;
		Environment = environment;
		Uptime = uptime;
		MemoryUsage = memoryUsage;
		Status = status;
		Timestamp = timestamp;
		Details = details;
	}

	[Required] public string Version { get; }
	[Required] public string Environment { get; }
	[Required] public TimeSpan Uptime { get; }
	[Required] public long MemoryUsage { get; }
	[Required] public string Status { get; }
	[Required] public DateTime Timestamp { get; }
	[Required] public Dictionary<string, object> Details { get; }
}