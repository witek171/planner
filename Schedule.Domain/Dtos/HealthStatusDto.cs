namespace Schedule.Domain.Dtos;

public class HealthStatusDto
{
	public HealthStatusDto(
		string status,
		DateTime timestamp,
		Dictionary<string, object> details
	)
	{
		Status = status;
		Timestamp = timestamp;
		Details = details;
	}

	public string Status { get; set; }
	public DateTime Timestamp { get; set; }
	public Dictionary<string, object> Details { get; set; }
}