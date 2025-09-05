using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Services;

public interface IStaffMemberService
{
	Task<List<StaffMember>> GetAllAsync(Guid companyId);

	Task<StaffMember?> GetByIdAsync(
		Guid id,
		Guid companyId);

	Task<Guid> CreateAsync(StaffMember staffMember);
	Task PutAsync(StaffMember staffMember);

	Task DeleteAsync(
		Guid id,
		Guid companyId);
}