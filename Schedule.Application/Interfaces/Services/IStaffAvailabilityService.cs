using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Services;

public interface IStaffAvailabilityService
{
	Task<List<StaffAvailability>> GetByStaffIdAsync(Guid staffId);
	Task<StaffAvailability?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(StaffAvailability availability);
	Task UpdateAsync(StaffAvailability availability);
	Task DeleteAsync(Guid id);
}
