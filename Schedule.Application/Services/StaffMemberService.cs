using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models;

namespace Schedule.Application.Services;

public class StaffMemberService : IStaffMemberService
{
	private readonly IStaffMemberRepository _repository;

	public StaffMemberService(IStaffMemberRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<StaffMember>> GetAllAsync(Guid companyId)
	{
		return await _repository.GetAllAsync(companyId);
	}

	public async Task<StaffMember?> GetByIdAsync(
		Guid id,
		Guid companyId)
	{
		return await _repository.GetByIdAsync(id, companyId);
	}

	public async Task<Guid> CreateAsync(StaffMember staffMember)
	{
		staffMember.Normalize();
		await ValidateEmailAndPhoneAsync(staffMember);

		return await _repository.CreateAsync(staffMember);
	}

	public async Task PutAsync(StaffMember staffMember)
	{
		staffMember.Normalize();
		await ValidateEmailAndPhoneAsync(staffMember);
		await _repository.PutAsync(staffMember);
	}

	public async Task DeleteAsync(
		Guid id,
		Guid companyId)
	{
		if (await _repository.HasRelatedRecordsAsync(id, companyId))
		{
			StaffMember? staffMember = await _repository.GetByIdAsync(id, companyId);
			if (staffMember == null)
				throw new InvalidOperationException(
					$"Staff member {id} is already marked as deleted for company {companyId}");

			staffMember.SoftDelete();
			await _repository.UpdateSoftDeleteAsync(staffMember);
		}
		else
		{
			await _repository.DeleteByIdAsync(id, companyId);
		}
	}

	private async Task ValidateEmailAndPhoneAsync(StaffMember staffMember)
	{
		Guid companyId = staffMember.CompanyId;
		Guid staffMemberId = staffMember.Id;
		string email = staffMember.Email;
		string phone = staffMember.Phone;

		if (await _repository.EmailExistsForOtherAsync(companyId, staffMemberId, email))
			throw new ArgumentException(
				$"Email {email} already exists for company {companyId}");

		if (await _repository.PhoneExistsForOtherAsync(companyId, staffMemberId, phone))
			throw new ArgumentException(
				$"Phone {phone} already exists for company {companyId}");
	}
}