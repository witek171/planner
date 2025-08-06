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

	public async Task<List<Staff>> GetAllAsync()
	{
		return await _repository.GetAllAsync();
	}

	public async Task<Staff?> GetByIdAsync(Guid id)
	{
		return await _repository.GetByIdAsync(id);
	}

	public async Task<Guid> CreateAsync(Staff staff)
	{
		return await _repository.CreateAsync(staff);
	}

	public async Task UpdateAsync(Staff staff)
	{
		await _repository.PutAsync(staff);
	}

	public async Task DeleteAsync(Guid id, Guid companyId)
	{
		await _repository.DeleteByIdAsync(id);
	}
}
