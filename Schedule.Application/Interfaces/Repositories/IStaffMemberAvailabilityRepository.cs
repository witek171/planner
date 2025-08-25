using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface IStaffMemberAvailabilityRepository
{
	Task<List<StaffMemberAvailability>> GetByStaffMemberIdAsync(
		Guid companyId,
		Guid staffMemberId);

	Task<Guid> CreateAsync(StaffMemberAvailability availability);

	Task<bool> DeleteByIdAsync(
		Guid staffMemberAvailabilityId,
		Guid companyId);
}