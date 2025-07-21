namespace Schedule.Contracts.Dtos;

public record ApplicationHealthStatusDto(
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