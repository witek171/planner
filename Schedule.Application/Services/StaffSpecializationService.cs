using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models;

namespace Schedule.Application.Services;

public class StaffSpecializationService : IStaffSpecializationService
{
	private readonly IStaffSpecializationRepository _repository;

	public StaffSpecializationService(IStaffSpecializationRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<StaffSpecialization>> GetByStaffIdAsync(Guid staffId)
	{
		return await _repository.GetByStaffIdAsync(staffId);
	}

	public async Task<Guid> CreateAsync(StaffSpecialization specialization)
	{
		return await _repository.CreateAsync(specialization);
	}

	public async Task DeleteAsync(Guid id)
	{
		await _repository.DeleteByIdAsync(id);
	}
}
