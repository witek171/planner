using System.Diagnostics;
using System.Reflection;
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

		_healthCheckUtils.AddDetailsOrLogError(details, ref status, "machineName",
			() => Environment.MachineName, _logger);
		_healthCheckUtils.AddDetailsOrLogError(details, ref status, "processorCount",
			() => Environment.ProcessorCount, _logger);
		_healthCheckUtils.AddDetailsOrLogError(details, ref status, "osVersion",
			() => Environment.OSVersion.ToString(), _logger);
		_healthCheckUtils.AddDetailsOrLogError(details, ref status, "workingDirectory",
			() => Environment.CurrentDirectory, _logger);
		_healthCheckUtils.AddDetailsOrLogError(details, ref status, "assemblyLocation",
			() => Assembly.GetExecutingAssembly().Location ?? "Unknown", _logger);
		_healthCheckUtils.AddDetailsOrLogError(details, ref status, "dotnetVersion",
			() => Environment.Version.ToString(), _logger);
		_healthCheckUtils.AddDetailsOrLogError(details, ref status, "is64BitProcess",
			() => Environment.Is64BitProcess, _logger);
		_healthCheckUtils.AddDetailsOrLogError(details, ref status, "totalMemory",
			() => GC.GetTotalMemory(false), _logger);

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