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

	public async Task<Guid> CreateAsync(
		Guid companyId,
		StaffMemberSpecialization staffMemberSpecialization)
	{
		if (
			await _repository.ExistsAsync(
				staffMemberSpecialization.StaffMemberId,
				staffMemberSpecialization.SpecializationId)
		)
		{
			// zamiast poprawnego id specjalizacji jest zwracany guid empty
			throw new InvalidOperationException(
				$"Staff member {staffMemberSpecialization.StaffMemberId}" +
				$" already has specialization {staffMemberSpecialization.Id} assigned");
		}

		return await _repository.CreateAsync(companyId, staffMemberSpecialization);
	}

	public async Task DeleteAsync(
		Guid id,
		Guid companyId)
	{
		await _repository.DeleteByIdAsync(id, companyId);
	}
}