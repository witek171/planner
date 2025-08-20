using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface ICompanyRepository
{
	Task<Guid> CreateAsync(Company company);
	Task<bool> PutAsync(Company company);
	Task<bool> DeleteByIdAsync(Guid companyId);
	Task<Company?> GetByIdAsync(Guid companyId);
	Task<bool> ExistsAsParentAsync(Guid companyId);
	Task<List<Guid>> GetParentIdsAsync(Guid childId);

	Task<bool> AddRelationAsync(
		Guid childId,
		Guid parentId);

	Task<bool> RemoveRelationAsync(Guid companyId);
	Task<bool> RemoveAllRelationsAsync(Guid companyId);
	Task<List<Company>> GetAllRelationsAsync(Guid companyId);
	Task<bool> UpdateIsParentNodeFlagAsync(Company company);
	Task<bool> UpdateIsReceptionFlagAsync(Company company);
}