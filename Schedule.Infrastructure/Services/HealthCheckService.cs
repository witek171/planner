using System.Diagnostics;
using Schedule.Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Services;
using Schedule.Application.Interfaces.Utils;

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
		string version = _healthCheckUtils.GetAssemblyVersion();
		string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown";
		Process? process = null;
		TimeSpan uptime = TimeSpan.Zero;
		try
		{
			process = Process.GetCurrentProcess();
			uptime = DateTime.UtcNow - process.StartTime.ToUniversalTime();
		}
		catch (Exception uptimeEx)
		{
			_logger.LogWarning(uptimeEx, "Unable to calculate application uptime");
		}

		try
		{
			_logger.LogInformation("Checking application health");
			long memoryUsage = process?.WorkingSet64 ?? 0;

			Dictionary<string, object> details = new Dictionary<string, object>
			{
				["machineName"] = Environment.MachineName,
				["processorCount"] = Environment.ProcessorCount,
				["osVersion"] = Environment.OSVersion.ToString(),
				["workingDirectory"] = Environment.CurrentDirectory,
				["assemblyLocation"] = System.Reflection.Assembly
					.GetExecutingAssembly().Location ?? "Unknown"
			};

			return new ApplicationHealthStatus(
				version,
				environment,
				uptime,
				memoryUsage,
				"Healthy",
				DateTime.UtcNow,
				details
			);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error while checking application health");

			Dictionary<string, object> details = new Dictionary<string, object>
			{
				["error"] = ex.Message,
				["errorType"] = ex.GetType().Name
			};

			return new ApplicationHealthStatus(
				version,
				environment,
				uptime,
				0,
				"Unhealthy",
				DateTime.UtcNow,
				details
			);
		}
	}

	public async Task<DatabaseHealthStatus> GetDatabaseStatusAsync()
	{
		Stopwatch stopwatch = Stopwatch.StartNew();
		string connectionString = _healthCheckUtils.GetConnectionString();
		string maskedConnectionString = _healthCheckUtils.MaskConnectionString(connectionString);

		try
		{
			_logger.LogInformation("Checking database health");

			if (string.IsNullOrEmpty(connectionString))
				throw new InvalidOperationException("Connection string not configured");

			await using SqlConnection connection = new SqlConnection(connectionString);
			await connection.OpenAsync();

			await using SqlCommand testCommand = new SqlCommand("SELECT 1", connection);
			await testCommand.ExecuteScalarAsync();

			stopwatch.Stop();

			string databaseName = connection.Database ?? "Unknown";
			string serverVersion = connection.ServerVersion ?? "Unknown";

			_logger.LogInformation("Database connection successful: {DatabaseName}", databaseName);

			Dictionary<string, object> details = new Dictionary<string, object>
			{
				["serverVersion"] = serverVersion,
				["connectionTimeout"] = connection.ConnectionTimeout,
				["state"] = connection.State.ToString()
			};

			return new DatabaseHealthStatus(
				maskedConnectionString,
				stopwatch.Elapsed,
				databaseName,
				"Healthy",
				DateTime.UtcNow,
				details
			);
		}
		catch (SqlException sqlEx)
		{
			stopwatch.Stop();
			_logger.LogError(sqlEx, "SQL error while checking database health");

			Dictionary<string, object> details = new Dictionary<string, object>
			{
				["error"] = sqlEx.Message,
				["errorType"] = sqlEx.GetType().Name,
				["sqlErrorNumber"] = sqlEx.Number
			};

			return new DatabaseHealthStatus(
				maskedConnectionString,
				stopwatch.Elapsed,
				"Unknown",
				"Unhealthy",
				DateTime.UtcNow,
				details
			);
		}
		catch (Exception ex)
		{
			stopwatch.Stop();
			_logger.LogError(ex, "General error while checking database health");

			Dictionary<string, object> details = new Dictionary<string, object>
			{
				["error"] = ex.Message,
				["errorType"] = ex.GetType().Name
			};

			return new DatabaseHealthStatus(
				maskedConnectionString,
				stopwatch.Elapsed,
				"Unknown",
				"Unhealthy",
				DateTime.UtcNow,
				details
			);
		}
	}
}