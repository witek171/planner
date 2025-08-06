using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface IStaffSpecializationRepository
{
	Task<List<StaffSpecialization>> GetByStaffIdAsync(Guid staffId);
	Task<Guid> CreateAsync(StaffSpecialization specialization);
	Task<bool> DeleteByIdAsync(
		Guid companyId,
		Guid id);
}
