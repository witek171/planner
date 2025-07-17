using Common.Domain.Models;

namespace Common.Application.Services
{
    public interface IHealthCheckService
    {
        ApplicationHealthStatus GetApplicationStatus();
        Task<DatabaseHealthStatus> GetDatabaseStatusAsync();
    }
}