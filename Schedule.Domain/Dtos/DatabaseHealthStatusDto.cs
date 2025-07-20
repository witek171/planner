namespace Schedule.Domain.Dtos;

public record DatabaseHealthStatusDto(
	string ConnectionString,
	TimeSpan ResponseTime,
	string DatabaseName,
	string Status,
	DateTime Timestamp,
	Dictionary<string, object> Details
)
{
}