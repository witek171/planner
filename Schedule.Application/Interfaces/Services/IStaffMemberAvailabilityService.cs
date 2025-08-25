using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Services;

public interface IStaffMemberAvailabilityService
{
	Task<List<StaffMemberAvailability>> GetByStaffMemberIdAsync(
		Guid companyId,
		Guid staffMemberId);

	Task<Guid> CreateAsync(StaffMemberAvailability availability);

	Task DeleteAsync(
		Guid companyId,
		Guid id);
}