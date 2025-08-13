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

	public async Task<List<StaffMemberAvailability>> GetByStaffMemberIdAsync(
		Guid companyId,
		Guid staffMemberId)
	{
		return await _repository.GetByStaffMemberIdAsync(companyId, staffMemberId);
	}

	public async Task<StaffMemberAvailability?> GetByIdAsync(
		Guid companyId,
		Guid id)
	{
		return await _repository.GetByIdAsync(companyId, id);
	}

	public async Task<Guid> CreateAsync(StaffMemberAvailability availability)
	{
		return await _repository.CreateAsync(availability);
	}

	public async Task DeleteAsync(
		Guid companyId,
		Guid id)
	{
			await _repository.DeleteByIdAsync(companyId, id);
	}
}