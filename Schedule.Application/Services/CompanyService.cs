using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models;

namespace Schedule.Application.Services;

public class CompanyService : ICompanyService
{
	private readonly ICompanyRepository _repository;

	public CompanyService(ICompanyRepository repository)
	{
		_repository = repository;
	}

	public async Task<Guid> CreateAsync(Company company)
	{
		company.Normalize();
		return await _repository.CreateAsync(company);
	}

	public async Task PutAsync(Company company)
	{
		company.Normalize();
		await _repository.PutAsync(company);
	}

	public async Task DeleteByIdAsync(Guid companyId)
	{
		await _repository.DeleteByIdAsync(companyId);
	}

	public async Task<Company?> GetByIdAsync(Guid companyId)
	{
		return await _repository.GetByIdAsync(companyId);
	}

	public async Task MarkAsParentNodeAsync(Company company)
	{
		company.MarkAsParentNode();
		await _repository.UpdateIsParentNodeFlagAsync(company);
	}

	public async Task UnmarkAsParentNodeAsync(Company company)
	{
		bool isUsedAsParent = await _repository.ExistsAsParentAsync(company.Id);
		if (isUsedAsParent)
			throw new InvalidOperationException(
				$"Company {company.Id} cannot be unmarked as parent node, " +
				$"because it is used as parent node in hierarchy");

		company.UnmarkAsParentNode();
		await _repository.UpdateIsParentNodeFlagAsync(company);
	}

	public async Task MarkAsReceptionAsync(Company company)
	{
		company.MarkAsReception();
		await _repository.UpdateIsReceptionFlagAsync(company);
	}

	public async Task UnmarkAsReceptionAsync(Company company)
	{
		company.UnmarkAsReception();
		await _repository.UpdateIsReceptionFlagAsync(company);
	}
}