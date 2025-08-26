using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models;

namespace Schedule.Application.Services;

public class SpecializationService : ISpecializationService
{
	private readonly ISpecializationRepository _repository;

	public SpecializationService(ISpecializationRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<Specialization>> GetAllAsync(Guid companyId)
		=> await _repository.GetAllAsync(companyId);

	public async Task<Specialization?> GetByIdAsync(Guid id, Guid companyId)
		=> await _repository.GetByIdAsync(id, companyId);

	public async Task<Guid> CreateAsync(Specialization specialization)
		=> await _repository.CreateAsync(specialization);

	public async Task<bool> UpdateAsync(Specialization specialization)
		=> await _repository.UpdateAsync(specialization);

	public async Task<bool> DeleteAsync(Guid id, Guid companyId)
		=> await _repository.DeleteAsync(id, companyId);
}
