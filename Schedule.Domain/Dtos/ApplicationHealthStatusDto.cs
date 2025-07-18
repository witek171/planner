namespace Schedule.Domain.Dtos;

public class ApplicationHealthStatusDto : HealthStatusDto
{
	public ApplicationHealthStatusDto(Models.ApplicationHealthStatus health)
		: base(health.Status, health.Timestamp, health.Details)
	{
		Version = health.Version;
		Environment = health.Environment;
		Uptime = health.Uptime;
		MemoryUsage = health.MemoryUsage;
	}

	public string Version { get; set; }
	public string Environment { get; set; }
	public TimeSpan Uptime { get; set; }
	public long MemoryUsage { get; set; }
}