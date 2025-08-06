using Schedule.Domain.Models.StaffRelated;

namespace Schedule.Application.Interfaces.Services;

public interface IStaffService
{
	Task<List<Staff>> GetAllAsync();
	Task<Staff?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(Staff staff);
	Task UpdateAsync(Staff staff);
	Task DeleteAsync(Guid id, Guid companyId);
}
