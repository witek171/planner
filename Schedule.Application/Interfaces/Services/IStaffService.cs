using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Services;

public interface IStaffService
{
	Task<List<Staff>> GetAllAsync(Guid companyId);
	Task<Staff?> GetByIdAsync(
		Guid id,
		Guid companyId);
	Task<Guid> CreateAsync(Staff staff);
	Task UpdateAsync(Staff staff);
	Task DeleteAsync(
		Guid id,
		Guid companyId);
}
