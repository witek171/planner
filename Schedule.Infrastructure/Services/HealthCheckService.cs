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
		string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown";

		try
		{
			_logger.LogInformation("Checking application health");
			
			string version = _healthCheckUtils.GetAssemblyVersion();
			TimeSpan uptime = _healthCheckUtils.GetApplicationUptime();
			long memoryUsage = _healthCheckUtils.GetMemoryUsage();

			Dictionary<string, object> details = new Dictionary<string, object>
			{
				["machineName"] = Environment.MachineName,
				["processorCount"] = Environment.ProcessorCount,
				["osVersion"] = Environment.OSVersion.ToString(),
				["workingDirectory"] = Environment.CurrentDirectory,
				["assemblyLocation"] = System.Reflection.Assembly
					.GetExecutingAssembly().Location ?? "Unknown",
				["dotnetVersion"] = Environment.Version.ToString(),
				["is64BitProcess"] = Environment.Is64BitProcess,
				["totalMemory"] = GC.GetTotalMemory(false)
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
			
			string version = _healthCheckUtils.GetAssemblyVersion();
			TimeSpan uptime = _healthCheckUtils.GetApplicationUptime();
			long memoryUsage = _healthCheckUtils.GetMemoryUsage();

			Dictionary<string, object> errorDetails = new Dictionary<string, object>
			{
				["error"] = ex.Message,
				["errorType"] = ex.GetType().Name
			};

			return new ApplicationHealthStatus(
				version,
				environment,
				uptime,
				memoryUsage,
				"Unhealthy",
				DateTime.UtcNow,
				errorDetails
			);
		}
	}

	public async Task<DatabaseHealthStatus> GetDatabaseStatusAsync()
	{
		Stopwatch stopwatch = Stopwatch.StartNew();
		string connectionString = _healthCheckUtils.GetConnectionString();

		try
		{
			_logger.LogInformation("Checking database health");
			
			string maskedConnectionString = _healthCheckUtils.MaskConnectionString(connectionString);

			if (string.IsNullOrWhiteSpace(connectionString))
				throw new ArgumentException("Connection string cannot be null or empty", nameof(connectionString));

			TimeSpan databaseTimeout = TimeSpan.FromSeconds(10);
			using CancellationTokenSource timeoutCts = new CancellationTokenSource(databaseTimeout);

			await using SqlConnection connection = new SqlConnection(connectionString);
			await connection.OpenAsync(timeoutCts.Token);

			await using SqlCommand testCommand = new SqlCommand("SELECT 1", connection);
			await testCommand.ExecuteScalarAsync(timeoutCts.Token);

			string databaseName = connection.Database ?? "Unknown";
			string serverVersion = connection.ServerVersion ?? "Unknown";

			var details = new Dictionary<string, object>
			{
				["serverVersion"] = serverVersion,
				["connectionTimeout"] = connection.ConnectionTimeout,
				["state"] = connection.State.ToString(),
				["serverProcessId"] = connection.ServerProcessId.ToString(),
				["clientConnectionId"] = connection.ClientConnectionId.ToString(),
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
			
			string maskedConnectionString = _healthCheckUtils.MaskConnectionString(connectionString);

			Dictionary<string, object> errorDetails = new Dictionary<string, object>
			{
				["error"] = sqlEx.Message,
				["errorType"] = sqlEx.GetType().Name,
				["sqlErrorNumber"] = sqlEx.Number,
				["severity"] = sqlEx.Class,
				["state"] = sqlEx.State
			};

			return new DatabaseHealthStatus(
				maskedConnectionString,
				stopwatch.Elapsed,
				"Unknown",
				"Unhealthy",
				DateTime.UtcNow,
				errorDetails
			);
		}
		catch (Exception ex)
		{
			stopwatch.Stop();
			_logger.LogError(ex, "General error while checking database health");
			
			string maskedConnectionString = _healthCheckUtils.MaskConnectionString(connectionString);

			Dictionary<string, object> errorDetails = new Dictionary<string, object>
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
				errorDetails
			);
		}
	}
}