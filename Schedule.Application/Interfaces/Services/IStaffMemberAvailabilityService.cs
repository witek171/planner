using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Services;

public interface IStaffMemberAvailabilityService
{
	Task<List<StaffMemberAvailability>> GetByStaffMemberIdAsync(
		Guid companyId,
		Guid staffMemberId);

	Task<StaffMemberAvailability?> GetByIdAsync(
		Guid companyId,
		Guid id);

	Task<Guid> CreateAsync(StaffMemberAvailability availability);

	Task DeleteAsync(
		Guid companyId,
		Guid id);
}