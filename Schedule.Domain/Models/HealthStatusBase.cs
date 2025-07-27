namespace Schedule.Domain.Models;

public class HealthStatusBase
{
	public HealthStatusBase(
		string status,
		DateTime timestamp,
		Dictionary<string, object> details
	)
	{
		Status = status;
		Timestamp = timestamp;
		Details = details;
	}

	public string Status { get; }
	public DateTime Timestamp { get; }
	public Dictionary<string, object> Details { get; }
}