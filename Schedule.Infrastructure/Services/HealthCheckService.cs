using System.Diagnostics;
using System.Reflection;
using System.Security;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Services;
using Schedule.Application.Interfaces.Utils;
using Schedule.Domain.Models;

namespace Schedule.Infrastructure.Services;

public class HealthCheckService : IHealthCheckService
{
	private readonly IHealthCheckUtils _healthCheckUtils;
	private readonly ILogger<HealthCheckService> _logger;

	public HealthCheckService(
		IHealthCheckUtils healthCheckUtils,
		ILogger<HealthCheckService> logger
	)
	{
		_healthCheckUtils = healthCheckUtils;
		_logger = logger;
	}

	public ApplicationHealthStatus GetApplicationStatus()
	{
		_logger.LogInformation("Checking application health");

		string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown";
		string status = "Healthy";
		Dictionary<string, object> details = new();
		string version = _healthCheckUtils.GetAssemblyVersion();
		TimeSpan uptime = _healthCheckUtils.GetApplicationUptime();
		long memoryUsage = _healthCheckUtils.GetMemoryUsage();

		try
		{
			details["machineName"] = Environment.MachineName;
			details["processorCount"] = Environment.ProcessorCount;
			details["osVersion"] = Environment.OSVersion.ToString();
			details["workingDirectory"] = Environment.CurrentDirectory;
			details["assemblyLocation"] = Assembly
				.GetExecutingAssembly().Location ?? "Unknown";
			details["dotnetVersion"] = Environment.Version.ToString();
			details["is64BitProcess"] = Environment.Is64BitProcess;
			details["totalMemory"] = GC.GetTotalMemory(false);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error while checking application health");

			if (ex is SecurityException or UnauthorizedAccessException or OutOfMemoryException)
			{
				status = "Unhealthy";
				_logger.LogCritical(ex, "Critical system error detected");
			}
			else
				status = "Degraded";

			details["error"] = ex.Message;
			details["errorType"] = ex.GetType().Name;
		}

		if (version == "Unknown" || uptime == TimeSpan.Zero || memoryUsage == 0)
		{
			status = status == "Healthy" ? "Degraded" : status;
			details["dataQuality"] = "Some metrics unavailable";
		}

		return new ApplicationHealthStatus(
			version,
			environment,
			uptime,
			memoryUsage,
			status,
			DateTime.UtcNow,
			details
		);
	}

	public async Task<DatabaseHealthStatus> GetDatabaseStatusAsync()
	{
		_logger.LogInformation("Checking database health");

		Stopwatch stopwatch = Stopwatch.StartNew();
		string maskedConnectionString = string.Empty;
		string databaseName = "Unknown";
		string status = "Healthy";
		Dictionary<string, object> details = new();

		try
		{
			string connectionString = _healthCheckUtils.GetConnectionString();
			maskedConnectionString = _healthCheckUtils.MaskConnectionString(connectionString);

			if (string.IsNullOrWhiteSpace(connectionString))
			{
				status = "Unhealthy";
				details["error"] = "Connection string is null or empty";
				details["errorType"] = "ConfigurationError";
			}
			else
			{
				const int timeoutSeconds = 10;
				TimeSpan databaseTimeout = TimeSpan.FromSeconds(timeoutSeconds);
				using CancellationTokenSource timeoutCts = new(databaseTimeout);

				await using SqlConnection connection = new(connectionString);
				await connection.OpenAsync(timeoutCts.Token);

				await using SqlCommand testCommand = new("SELECT 1", connection);
				await testCommand.ExecuteScalarAsync(timeoutCts.Token);

				databaseName = connection.Database ?? "Unknown";
				string serverVersion = connection.ServerVersion ?? "Unknown";

				details["serverVersion"] = serverVersion;
				details["connectionTimeout"] = connection.ConnectionTimeout;
				details["state"] = connection.State.ToString();
				details["serverProcessId"] = connection.ServerProcessId.ToString();
				details["clientConnectionId"] = connection.ClientConnectionId.ToString();
			}
		}
		catch (SqlException sqlEx)
		{
			_logger.LogError(sqlEx, "SQL error while checking database health");

			status = "Unhealthy";
			details["error"] = sqlEx.Message;
			details["errorType"] = sqlEx.GetType().Name;
			details["sqlErrorNumber"] = sqlEx.Number;
			details["severity"] = sqlEx.Class;
			details["state"] = sqlEx.State;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "General error while checking database health");

			status = "Unhealthy";
			details["error"] = ex.Message;
			details["errorType"] = ex.GetType().Name;
		}
		finally
		{
			stopwatch.Stop();
		}

		if (databaseName == "Unknown" || stopwatch.Elapsed == TimeSpan.Zero)
		{
			status = status == "Healthy" ? "Degraded" : status;
			details["dataQuality"] = "Some metrics unavailable";
		}

		return new DatabaseHealthStatus(
			maskedConnectionString,
			stopwatch.Elapsed,
			databaseName,
			status,
			DateTime.UtcNow,
			details
		);
	}
}