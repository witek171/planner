using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Services;

public interface ICompanyService
{
	Task<Guid> CreateAsync(Company company);
	Task PutAsync(Company company);
	Task DeleteByIdAsync(Guid companyId);
	Task<Company?> GetByIdAsync(Guid companyId);
	Task MarkAsParentNodeAsync(Company company);
	Task UnmarkAsParentNodeAsync(Company company);
	Task MarkAsReceptionAsync(Company company);
	Task UnmarkAsReceptionAsync(Company company);
}