namespace Schedule.Domain.Models;

public class DatabaseHealthStatus : HealthStatusBase
{
	public DatabaseHealthStatus(
		string connectionString,
		TimeSpan responseTime,
		string databaseName,
		string status,
		DateTime timestamp,
		Dictionary<string, object> details) : base(status, timestamp, details)
	{
		ConnectionString = connectionString;
		ResponseTime = responseTime;
		DatabaseName = databaseName;
	}

	public string ConnectionString { get; }
	public TimeSpan ResponseTime { get; }
	public string DatabaseName { get; }
}