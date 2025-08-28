using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Services;

public interface ISpecializationService
{
	Task<List<Specialization>> GetAllAsync(Guid companyId);
	Task<Specialization?> GetByIdAsync(Guid id, Guid companyId);
	Task<Guid> CreateAsync(Specialization specialization);
	Task<bool> UpdateAsync(Specialization specialization);
	Task<bool> DeleteAsync(Guid id, Guid companyId);
}
