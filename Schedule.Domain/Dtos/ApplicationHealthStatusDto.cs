namespace Schedule.Domain.Dtos;

public sealed record ApplicationHealthStatusDto(
	string Version,
	string Environment,
	TimeSpan Uptime,
	long MemoryUsage,
	string Status,
	DateTime Timestamp,
	Dictionary<string, object> Details
)
{
}