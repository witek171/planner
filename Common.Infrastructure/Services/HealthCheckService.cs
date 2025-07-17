using System.Diagnostics;
using Common.Application.Services;
using Common.Application.Utils;
using Common.Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Common.Infrastructure.Services
{
    public class HealthCheckService : IHealthCheckService
    {
        private readonly IHealthCheckUtils _healthCheckUtils;
        private readonly ILogger<HealthCheckService> _logger;
        private readonly DateTime _startTime;

        public HealthCheckService(
            IHealthCheckUtils healthCheckUtils,
            ILogger<HealthCheckService> logger
        )
        {
            _healthCheckUtils = healthCheckUtils;
            _logger = logger;
            _startTime = DateTime.UtcNow;
        }

        public ApplicationHealthStatus GetApplicationStatus()
        {
            try
            {
                _logger.LogInformation("Checking application health");
                var process = Process.GetCurrentProcess();
                var memoryUsage = process.WorkingSet64;
                var uptime = DateTime.UtcNow - _startTime;

                return new ApplicationHealthStatus
                {
                    Status = "Healthy",
                    Timestamp = DateTime.UtcNow,
                    Version = _healthCheckUtils.GetAssemblyVersion(),
                    Environment = Environment
                        .GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown",
                    Uptime = uptime,
                    MemoryUsage = memoryUsage,
                    Details = new Dictionary<string, object>
                    {
                        ["machine_name"] = Environment.MachineName,
                        ["processor_count"] = Environment.ProcessorCount,
                        ["os_version"] = Environment.OSVersion.ToString(),
                        ["working_directory"] = Environment.CurrentDirectory,
                        ["assembly_location"] = System.Reflection.Assembly
                            .GetExecutingAssembly().Location ?? "Unknown"
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking application health");
                return new ApplicationHealthStatus
                {
                    Status = "Unhealthy",
                    Timestamp = DateTime.UtcNow,
                    Details = new Dictionary<string, object>
                    {
                        ["error"] = ex.Message,
                        ["error_type"] = ex.GetType().Name
                    }
                };
            }
        }

        public async Task<DatabaseHealthStatus> GetDatabaseStatusAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            var connectionString = _healthCheckUtils.GetConnectionString();

            try
            {
                _logger.LogInformation("Checking database health");

                if (string.IsNullOrEmpty(connectionString))
                    throw new InvalidOperationException("Connection string not configured");

                await using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                // test command
                await using var command = new SqlCommand("SELECT 1", connection);
                await command.ExecuteScalarAsync();

                stopwatch.Stop();

                var databaseName = connection.Database ?? "Unknown";
                var serverVersion = connection.ServerVersion ?? "Unknown";

                _logger.LogInformation("Database connection successful: {DatabaseName}", databaseName);

                return new DatabaseHealthStatus
                {
                    Status = "Healthy",
                    Timestamp = DateTime.UtcNow,
                    ConnectionString = _healthCheckUtils.MaskConnectionString(connectionString),
                    ResponseTime = stopwatch.Elapsed,
                    DatabaseName = databaseName,
                    Details = new Dictionary<string, object>
                    {
                        ["server_version"] = serverVersion,
                        ["connection_timeout"] = connection.ConnectionTimeout,
                        ["state"] = connection.State.ToString()
                    }
                };
            }
            catch (SqlException sqlEx)
            {
                stopwatch.Stop();
                _logger.LogError(sqlEx, "SQL error while checking database health");

                return new DatabaseHealthStatus
                {
                    Status = "Unhealthy",
                    Timestamp = DateTime.UtcNow,
                    ConnectionString = _healthCheckUtils.MaskConnectionString(connectionString),
                    ResponseTime = stopwatch.Elapsed,
                    Details = new Dictionary<string, object>
                    {
                        ["error"] = sqlEx.Message,
                        ["error_type"] = sqlEx.GetType().Name,
                        ["sql_error_number"] = sqlEx.Number
                    }
                };
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "General error while checking database health");

                return new DatabaseHealthStatus
                {
                    Status = "Unhealthy",
                    Timestamp = DateTime.UtcNow,
                    ConnectionString = _healthCheckUtils.MaskConnectionString(connectionString),
                    ResponseTime = stopwatch.Elapsed,
                    Details = new Dictionary<string, object>
                    {
                        ["error"] = ex.Message,
                        ["error_type"] = ex.GetType().Name
                    }
                };
            }
        }
    }
}