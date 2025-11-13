using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class DatabaseHealthStatusResponse
{
	public DatabaseHealthStatusResponse(
		string connectionString,
		TimeSpan responseTime,
		string databaseName,
		string status,
		DateTime timestamp,
		Dictionary<string, object> details)
	{
		ConnectionString = connectionString;
		ResponseTime = responseTime;
		DatabaseName = databaseName;
		Status = status;
		Timestamp = timestamp;
		Details = details;
	}

	public DatabaseHealthStatusResponse()
	{
	}

	[Required] public string ConnectionString { get; }
	[Required] public TimeSpan ResponseTime { get; }
	[Required] public string DatabaseName { get; }
	[Required] public string Status { get; }
	[Required] public DateTime Timestamp { get; }
	[Required] public Dictionary<string, object> Details { get; }
}