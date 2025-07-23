using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos;

public class DatabaseHealthStatusResponse
{
	[Required] public string ConnectionString { get; set; }
	[Required] public TimeSpan ResponseTime { get; set; }
	[Required] public string DatabaseName { get; set; }
	[Required] public string Status { get; set; }
	[Required] public DateTime Timestamp { get; set; }
	[Required] public Dictionary<string, object> Detail { get; set; }

	public DatabaseHealthStatusResponse(
		string connectionString,
		TimeSpan responseTime,
		string databaseName,
		string status,
		DateTime timestamp,
		Dictionary<string, object> detail
	)
	{
		ConnectionString = connectionString;
		ResponseTime = responseTime;
		DatabaseName = databaseName;
		Status = status;
		Timestamp = timestamp;
		Detail = detail;
	}
}