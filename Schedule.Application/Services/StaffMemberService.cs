using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models;

namespace Schedule.Application.Services;

public class StaffMemberService : IStaffMemberService
{
	private readonly IStaffMemberRepository _staffMemberRepository;

	public StaffMemberService(IStaffMemberRepository staffMemberRepository)
	{
		_staffMemberRepository = staffMemberRepository;
	}

	public async Task<List<StaffMember>> GetAllAsync(Guid companyId)
		=> await _staffMemberRepository.GetAllAsync(companyId);

	public async Task<StaffMember?> GetByIdAsync(
		Guid id,
		Guid companyId)
		=> await _staffMemberRepository.GetByIdAsync(id, companyId);

	private async Task ValidateEmailAndPhoneAsync(StaffMember staffMember, Guid companyId)
	{
		Guid staffMemberId = staffMember.Id;
		string email = staffMember.Email;
		string phone = staffMember.Phone;

		if (await _staffMemberRepository.EmailExistsForOtherAsync(companyId, staffMemberId, email))
			throw new ArgumentException(
				$"Email {email} already exists for company {companyId}");

		if (await _staffMemberRepository.PhoneExistsForOtherAsync(companyId, staffMemberId, phone))
			throw new ArgumentException(
				$"Phone {phone} already exists for company {companyId}");
	}

	public async Task<Guid> CreateAsync(StaffMember staffMember, Guid companyId)
	{
		staffMember.Normalize();
		await ValidateEmailAndPhoneAsync(staffMember, companyId);
		staffMember.AddCompany(companyId);
		return await _staffMemberRepository.CreateAsync(staffMember);
	}

	public async Task PutAsync(StaffMember staffMember, Guid companyId)
	{
		staffMember.Normalize();
		await ValidateEmailAndPhoneAsync(staffMember, companyId);
		await _staffMemberRepository.PutAsync(staffMember);
	}

	public async Task DeleteAsync(
		Guid id,
		Guid companyId)
	{
		if (await _staffMemberRepository.HasRelatedRecordsAsync(id, companyId))
		{
			StaffMember? staffMember = await _staffMemberRepository.GetByIdAsync(id, companyId);
			if (staffMember == null)
				throw new InvalidOperationException(
					$"Staff member {id} is already marked as deleted for company {companyId}");

			staffMember.SoftDelete();
			await _staffMemberRepository.UpdateSoftDeleteAsync(staffMember);
		}
		else
			await _staffMemberRepository.DeleteByIdAsync(id, companyId);
	}

	public async Task<bool> AssignToCompanyAsync(Guid staffMemberId, Guid companyId)
	{
		return await _staffMemberRepository.AssignToCompanyAsync(staffMemberId, companyId);
	}

	public async Task<bool> UnassignFromCompanyAsync(Guid staffMemberId, Guid companyId)
	{
		return await _staffMemberRepository.UnassignFromCompanyAsync(staffMemberId, companyId);
	}

	public async Task<List<StaffMemberCompany>> GetAssignedCompanyAsync(Guid staffMemberId)
	{
		return await _staffMemberRepository.GetAssignedCompanyAsync(staffMemberId);
	}
}