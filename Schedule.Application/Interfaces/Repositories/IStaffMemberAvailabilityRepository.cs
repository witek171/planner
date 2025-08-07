using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface IStaffMemberAvailabilityRepository
{
	Task<List<StaffMemberAvailability>> GetByStaffMemberIdAsync(Guid staffMemberId);
	Task<StaffMemberAvailability?> GetByIdAsync(Guid staffMemberAvailabilityId);
	Task<Guid> CreateAsync(StaffMemberAvailability availability);
	Task<bool> PutAsync(StaffMemberAvailability availability);

	Task<bool> DeleteByIdAsync(
		Guid staffMemberAvailabilityId,
		Guid companyId);
}