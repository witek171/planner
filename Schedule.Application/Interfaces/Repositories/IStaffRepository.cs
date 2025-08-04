using Schedule.Domain.Models.StaffRelated;

namespace Schedule.Application.Interfaces.Repositories;

public interface IStaffRepository
{
	Task<List<Staff>> GetAllAsync();
	Task<Staff?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(Staff staff);
	Task UpdateAsync(Staff staff);
	Task DeleteAsync(Guid id);
}