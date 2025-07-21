using System.Diagnostics;
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
		const string configKey = "DefaultConnection";
		const string envKey = "ConnectionStrings__DefaultConnection";

		string? connectionString = _configuration.GetConnectionString(configKey)
		                           ?? Environment.GetEnvironmentVariable(envKey);

		if (string.IsNullOrWhiteSpace(connectionString))
		{
			const string errorMessage = $"Database connection string not found. " +
			                            $"Check configuration key '{configKey}' or environment variable '{envKey}'";

			_logger.LogError("Configuration error: {ErrorMessage}", errorMessage);
			return string.Empty;
		}

		_logger.LogDebug("Connection string loaded successfully from configuration");
		return connectionString;
	}

	public string MaskConnectionString(string connectionString)
	{
		if (string.IsNullOrWhiteSpace(connectionString))
			return "Not configured";

		try
		{
			SqlConnectionStringBuilder builder = new(connectionString);

			if (!string.IsNullOrEmpty(builder.Password))
				builder.Password = "***";

			if (!string.IsNullOrEmpty(builder.UserID))
				builder.UserID = "***";

			return builder.ConnectionString;
		}
		catch (ArgumentException ex)
		{
			_logger.LogWarning(ex, "Invalid connection string format during masking");
			return "Invalid connection string format";
		}
		catch (Exception ex)
		{
			_logger.LogWarning(ex, "Cannot mask connection string");
			return "Invalid connection string format";
		}
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

	public TimeSpan GetApplicationUptime()
	{
		try
		{
			using Process process = Process.GetCurrentProcess();
			return DateTime.UtcNow - process.StartTime.ToUniversalTime();
		}
		catch (Exception ex)
		{
			_logger.LogWarning(ex, "Unable to calculate application uptime");
			return TimeSpan.Zero;
		}
	}

	public long GetMemoryUsage()
	{
		try
		{
			using Process process = Process.GetCurrentProcess();
			return process.WorkingSet64;
		}
		catch (Exception ex)
		{
			_logger.LogWarning(ex, "Unable to get memory usage");
			return 0;
		}
	}
}