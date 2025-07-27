namespace Schedule.Domain.Models;

public class ApplicationHealthStatus : HealthStatusBase
{
	public ApplicationHealthStatus(
		string version,
		string environment,
		TimeSpan uptime,
		long memoryUsage,
		string status,
		DateTime timestamp,
		Dictionary<string, object> details
	) : base(status, timestamp, details)
	{
		Version = version;
		Environment = environment;
		Uptime = uptime;
		MemoryUsage = memoryUsage;
	}

	public string Version { get; }
	public string Environment { get; }
	public TimeSpan Uptime { get; }
	public long MemoryUsage { get; }
}