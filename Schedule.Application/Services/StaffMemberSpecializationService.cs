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

	public async Task<List<Specialization>> GetStaffMemberSpecializationsAsync(
		Guid staffMemberId,
		Guid companyId)
	{
		return await _repository.GetStaffMemberSpecializationsAsync(staffMemberId, companyId);
	}

	public async Task<Guid> CreateAsync(
		Guid companyId,
		StaffMemberSpecialization specialization)
	{
		// sprwadzic czy staffMember ma juz specjalizacje o danym id
		return await _repository.CreateAsync(companyId, specialization);
	}

	public async Task DeleteAsync(
		Guid id,
		Guid companyId)
	{
		await _repository.DeleteByIdAsync(id, companyId);
	}
}