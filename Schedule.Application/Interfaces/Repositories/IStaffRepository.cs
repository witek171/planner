using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface IStaffRepository
{
	Task<List<Staff>> GetAllAsync(Guid companyId);

	Task<Staff?> GetByIdAsync(
		Guid staffId,
		Guid companyId);

	Task<Guid> CreateAsync(Staff staff);
	Task<bool> PutAsync(Staff staff);

	Task<bool> DeleteByIdAsync(
		Guid companyId,
		Guid staffId);

	Task<bool> HasRelatedRecordsAsync(
		Guid staffId,
		Guid companyId);
}