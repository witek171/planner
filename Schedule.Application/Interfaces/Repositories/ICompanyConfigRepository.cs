using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface ICompanyConfigRepository
{
	Task CreateAsync(Guid companyId);
	Task<bool> UpdateBreakTimesAsync(CompanyConfig companyConfig);
	Task<CompanyConfig?> GetByIdAsync(Guid companyId);
}