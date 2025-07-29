using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models.StaffRelated;

namespace Schedule.Application.Services;

public class StaffAvailabilityService : IStaffAvailabilityService
{
	private readonly IStaffAvailabilityRepository _repository;

	public StaffAvailabilityService(IStaffAvailabilityRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<StaffAvailability>> GetByStaffIdAsync(Guid staffId)
	{
		return await _repository.GetByStaffIdAsync(staffId);
	}

	public async Task<StaffAvailability?> GetByIdAsync(Guid id)
	{
		return await _repository.GetByIdAsync(id);
	}

	public async Task<Guid> CreateAsync(StaffAvailability availability)
	{
		return await _repository.CreateAsync(availability);
	}

	public async Task UpdateAsync(StaffAvailability availability)
	{
		await _repository.UpdateAsync(availability);
	}

	public async Task DeleteAsync(Guid id)
	{
		await _repository.DeleteAsync(id);
	}
}
