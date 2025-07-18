using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Utils;

namespace Schedule.Infrastructure.Utils;

public class HealthCheckUtils : IHealthCheckUtils
{
	private readonly IConfiguration _configuration;
	private readonly ILogger<HealthCheckUtils> _logger;

	public HealthCheckUtils(
		IConfiguration configuration,
		ILogger<HealthCheckUtils> logger
	)
	{
		_configuration = configuration;
		_logger = logger;
	}

	public string GetConnectionString()
	{
		string? connectionString = _configuration.GetConnectionString("DefaultConnection")
		                           ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

		if (string.IsNullOrEmpty(connectionString))
			_logger.LogWarning("Connection string not found in configuration or environment variables");

		return connectionString;
	}

	public string GetAssemblyVersion()
	{
		try
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			Version? version = assembly.GetName().Version;
			return version?.ToString() ?? "Unknown";
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error while retrieving assembly version");
			return "Unknown";
		}
	}

	public string MaskConnectionString(string connectionString)
	{
		if (string.IsNullOrEmpty(connectionString))
			return "Not configured";

		try
		{
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);

			if (!string.IsNullOrEmpty(builder.Password))
				builder.Password = "***";

			if (!string.IsNullOrEmpty(builder.UserID))
				builder.UserID = "***";

			return builder.ConnectionString;
		}
		catch (Exception ex)
		{
			_logger.LogWarning(ex, "Cannot mask connection string");
			return "Invalid connection string format";
		}
	}
}