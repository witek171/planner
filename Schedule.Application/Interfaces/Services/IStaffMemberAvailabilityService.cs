using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Services;

public interface IStaffMemberAvailabilityService
{
	Task<List<StaffMemberAvailability>> GetByStaffMemberIdAsync(Guid staffMemberId);
	Task<StaffMemberAvailability?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(StaffMemberAvailability availability);
	Task UpdateAsync(StaffMemberAvailability availability);
	Task DeleteAsync(Guid id);
}
