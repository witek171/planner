using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Services;

public interface IStaffMemberSpecializationService
{
	Task<List<Specialization>> GetStaffMemberSpecializationsAsync(
		Guid staffMemberId,
		Guid companyId);

	Task<Guid> CreateAsync(
		Guid companyId,
		StaffMemberSpecialization specialization);

	Task DeleteAsync(
		Guid id,
		Guid companyId);
}