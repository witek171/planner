using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface IStaffAvailabilityRepository
{
	Task<List<StaffAvailability>> GetByStaffIdAsync(Guid staffId);
	Task<StaffAvailability?> GetByIdAsync(Guid staffAvailabilityId);
	Task<Guid> CreateAsync(StaffAvailability availability);
	Task<bool> PutAsync(StaffAvailability availability);

	Task<bool> DeleteByIdAsync(
		Guid staffAvailabilityId,
		Guid companyId);
}