using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Services;

public interface ICompanyService
{
	Task<Guid> CreateAsync(Company company);
	Task PutAsync(Company company);
	Task DeleteByIdAsync(Guid companyId);
	Task<Company?> GetByIdAsync(Guid companyId);
	Task MarkAsReceptionAsync(Company company);
	Task UnmarkAsReceptionAsync(Company company);

	Task AddRelationAsync(
		Guid childId,
		Guid parentId);

	Task RemoveRelationsAsync(Guid companyId);
	Task<List<Company>> GetAllRelationsAsync(Guid companyId);
}