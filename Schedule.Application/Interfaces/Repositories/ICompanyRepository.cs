using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface ICompanyRepository
{
	Task<Guid> CreateAsync(Company company);
	Task<bool> PutAsync(Company company);
	Task<bool> DeleteByIdAsync(Guid companyId);
	Task<Company?> GetByIdAsync(Guid companyId);
	Task<bool> UpdateIsParentNodeFlagAsync(Company company);
	Task<bool> UpdateIsReceptionFlagAsync(Company company);
	Task<bool> ExistsAsParentAsync(Guid companyId);
}