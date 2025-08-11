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

		Guid companyId = staffMember.CompanyId;
		string email = staffMember.Email;
		string phone = staffMember.Phone;
		if (await _repository.EmailExistsAsync(companyId, email))
		{
			throw new InvalidOperationException(
				$"Email {email} already exists for this company");
		}

		if (await _repository.PhoneExistsAsync(companyId, phone))
		{
			throw new InvalidOperationException(
				$"Phone {phone} already exists for this company");
		}

		return await _repository.CreateAsync(staffMember);
	}

	public async Task PutAsync(StaffMember staffMember)
	{
		staffMember.Normalize();
		await _repository.PutAsync(staffMember);
	}

	public async Task DeleteAsync(
		Guid id,
		Guid companyId)
	{
		if (await _repository.HasRelatedRecordsAsync(id, companyId))
		{
			StaffMember staffMember = (await _repository.GetByIdAsync(id, companyId))!;
			staffMember.SoftDelete();
		}
		else
		{
			await _repository.DeleteByIdAsync(id, companyId);
		}
	}
}