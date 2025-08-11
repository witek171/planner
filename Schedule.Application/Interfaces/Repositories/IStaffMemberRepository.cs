using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface IStaffMemberRepository
{
	Task<List<StaffMember>> GetAllAsync(Guid companyId);

	Task<StaffMember?> GetByIdAsync(
		Guid staffMemberId,
		Guid companyId);

	Task<Guid> CreateAsync(StaffMember staffMember);
	Task<bool> PutAsync(StaffMember staffMember);

	Task<bool> DeleteByIdAsync(
		Guid companyId,
		Guid staffMemberId);

	Task<bool> HasRelatedRecordsAsync(
		Guid staffMemberId,
		Guid companyId);

	Task<bool> EmailExistsAsync(
		Guid companyId,
		string email);

	Task<bool> PhoneExistsAsync(
		Guid companyId,
		string phone);
}