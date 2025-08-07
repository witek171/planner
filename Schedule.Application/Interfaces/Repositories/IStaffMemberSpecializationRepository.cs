using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface IStaffMemberSpecializationRepository
{
	Task<Guid> CreateAsync(
		Guid companyId,
		StaffMemberSpecialization specialization);

	Task<bool> DeleteByIdAsync(
		Guid companyId,
		Guid id);

	Task<List<Specialization>> GetStaffMemberSpecializationsAsync(
		Guid staffMemberId,
		Guid companyId);
}