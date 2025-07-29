using Schedule.Domain.Models.StaffRelated;

namespace Schedule.Application.Interfaces.Repositories;

public interface IStaffAvailabilityRepository
{
	Task<List<StaffAvailability>> GetByStaffIdAsync(Guid staffId);
	Task<StaffAvailability?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(StaffAvailability availability);
	Task UpdateAsync(StaffAvailability availability);
	Task DeleteAsync(Guid id);
}
