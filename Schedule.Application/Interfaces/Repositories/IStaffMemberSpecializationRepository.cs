using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface IStaffMemberSpecializationRepository
{
	Task<List<StaffMemberSpecialization>> GetByStaffMemberIdAsync(Guid staffMemberId);
	Task<Guid> CreateAsync(StaffMemberSpecialization specialization);
	Task<bool> DeleteByIdAsync(
		Guid companyId,
		Guid id);
}
