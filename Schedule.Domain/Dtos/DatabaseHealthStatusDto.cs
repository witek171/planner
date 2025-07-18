namespace Schedule.Domain.Dtos;

public class DatabaseHealthStatusDto : HealthStatusDto
{
	public DatabaseHealthStatusDto(Models.DatabaseHealthStatus health) : 
		base(health.Status, health.Timestamp, health.Details)
	{
		ConnectionString = health.ConnectionString;
		ResponseTime = health.ResponseTime;
		DatabaseName = health.DatabaseName;
	}

	public string ConnectionString { get; set; }
	public TimeSpan ResponseTime { get; set; }
	public string DatabaseName { get; set; }
}