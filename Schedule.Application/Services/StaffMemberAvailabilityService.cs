using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models;

namespace Schedule.Application.Services;

public class StaffMemberAvailabilityService : IStaffMemberAvailabilityService
{
	private readonly IStaffMemberAvailabilityRepository _repository;

	public StaffMemberAvailabilityService(IStaffMemberAvailabilityRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<StaffMemberAvailability>> GetByStaffMemberIdAsync(Guid staffMemberId)
	{
		return await _repository.GetByStaffMemberIdAsync(staffMemberId);
	}

	public async Task<StaffMemberAvailability?> GetByIdAsync(Guid id)
	{
		return await _repository.GetByIdAsync(id);
	}

	public async Task<Guid> CreateAsync(StaffMemberAvailability availability)
	{
		return await _repository.CreateAsync(availability);
	}

	public async Task UpdateAsync(StaffMemberAvailability availability)
	{
		await _repository.PutAsync(availability);
	}

	public async Task DeleteAsync(Guid id)
	{
		await _repository.DeleteByIdAsync(id);
	}
}
