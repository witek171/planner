using Schedule.Domain.Models;

namespace Schedule.Infrastructure.Repositories;

public interface IStaffProfileRepository
{
    Task<IEnumerable<StaffProfile>> GetAllAsync();
    Task<StaffProfile?> GetByIdAsync(Guid id);
    Task AddAsync(StaffProfile staff);
    Task UpdateAsync(StaffProfile staff);
    Task DeleteAsync(Guid id);
}
