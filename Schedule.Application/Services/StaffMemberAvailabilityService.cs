using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models;

namespace Schedule.Application.Services;

public class StaffMemberAvailabilityService : IStaffMemberAvailabilityService
{
	private readonly IStaffMemberAvailabilityRepository _staffMemberAvailabilityRepository;

	public StaffMemberAvailabilityService(IStaffMemberAvailabilityRepository staffMemberAvailabilityRepository)
	{
		_staffMemberAvailabilityRepository = staffMemberAvailabilityRepository;
	}

	public async Task<List<StaffMemberAvailability>> GetByStaffMemberIdAsync(
		Guid companyId,
		Guid staffMemberId)
		=> await _staffMemberAvailabilityRepository
			.GetByStaffMemberIdAsync(companyId, staffMemberId);

	public async Task<Guid> CreateAsync(StaffMemberAvailability availability)
	{
		availability.MarkAsAvailable();
		return await _staffMemberAvailabilityRepository.CreateAsync(availability);
	}

	public async Task DeleteAsync(
		Guid companyId,
		Guid id)
		=> await _staffMemberAvailabilityRepository.DeleteByIdAsync(companyId, id);

	public async Task<bool> ExistsByIdAsync(
		Guid companyId,
		Guid id)
		=> await _staffMemberAvailabilityRepository.ExistsByIdAsync(companyId, id);
}