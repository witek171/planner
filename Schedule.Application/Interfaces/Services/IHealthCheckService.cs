using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Services;

public interface IHealthCheckService
{
	ApplicationHealthStatus GetApplicationStatus();
	Task<DatabaseHealthStatus> GetDatabaseStatusAsync();
}