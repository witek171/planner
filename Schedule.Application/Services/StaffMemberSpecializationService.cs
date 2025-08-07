using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models;

namespace Schedule.Application.Services;

public class StaffMemberSpecializationService : IStaffMemberSpecializationService
{
	private readonly IStaffMemberSpecializationRepository _repository;

	public StaffMemberSpecializationService(IStaffMemberSpecializationRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<StaffMemberSpecialization>> GetByStaffMemberIdAsync(Guid staffMemberId)
	{
		return await _repository.GetByStaffMemberIdAsync(staffMemberId);
	}

	public async Task<Guid> CreateAsync(StaffMemberSpecialization specialization)
	{
		return await _repository.CreateAsync(specialization);
	}

	public async Task DeleteAsync(Guid id)
	{
		await _repository.DeleteByIdAsync(id);
	}
}
