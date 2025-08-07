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
			StaffMember staffMemberMember = (await _repository.GetByIdAsync(id, companyId))!;
			staffMemberMember.SoftDelete();
		}
		else
		{
			await _repository.DeleteByIdAsync(id, companyId);
		}
	}
}