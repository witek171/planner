using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models;

namespace Schedule.Application.Services;

public class StaffService : IStaffService
{
	private readonly IStaffRepository _repository;

	public StaffService(IStaffRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<Staff>> GetAllAsync(Guid companyId)
	{
		return await _repository.GetAllAsync(companyId);
	}

	public async Task<Staff?> GetByIdAsync(
		Guid id,
		Guid companyId)
	{
		return await _repository.GetByIdAsync(id, companyId);
	}

	public async Task<Guid> CreateAsync(Staff staff)
	{
		staff.Normalize();
		return await _repository.CreateAsync(staff);
	}

	public async Task UpdateAsync(Staff staff)
	{
		staff.Normalize();
		await _repository.PutAsync(staff);
	}

	public async Task DeleteAsync(
		Guid id,
		Guid companyId)
	{
		if (await _repository.HasRelatedRecordsAsync(id, companyId))
		{
			Staff staffMember = (await _repository.GetByIdAsync(id, companyId))!;
			staffMember.SoftDelete();
		}
		else
		{
			await _repository.DeleteByIdAsync(id, companyId);
		}
	}
}