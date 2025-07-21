using Microsoft.Extensions.Logging;

namespace Schedule.Application.Interfaces.Utils;

public interface IHealthCheckUtils
{
	string GetConnectionString();
	string GetAssemblyVersion();
	string MaskConnectionString(string connectionString);
	TimeSpan GetApplicationUptime();
	long GetMemoryUsage();

	void AddDetailsOrLogError(
		Dictionary<string, object> details,
		ref string status,
		string key,
		Func<object> func,
		ILogger logger
	);
}