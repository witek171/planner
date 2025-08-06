using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface IStaffRepository
{
	Task<List<Staff>> GetAllAsync();
	Task<Staff?> GetByIdAsync(Guid staffId);
	Task<Guid> CreateAsync(Staff staff);
	Task<bool> PutAsync(Staff staff);
	Task<bool> DeleteByIdAsync(
		Guid companyId,
		Guid staffId);
}