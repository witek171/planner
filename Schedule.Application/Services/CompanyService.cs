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

	public async Task AddRelationAsync(
		Guid childId,
		Guid parentId)
	{
		if (await _repository.ExistsAsChildAsync(childId))
			throw new InvalidOperationException(
				$"Company {childId} already has a parent company");

		if (await _repository.RelationExistAsync(childId, parentId))
			throw new InvalidOperationException(
				$"Relation between companies {childId} and {parentId} already exists");

		await _repository.AddRelationAsync(childId, parentId);
		Company parent = (await _repository.GetByIdAsync(parentId))!;

		if (!parent.IsParentNode)
		{
			parent.MarkAsParentNode();
			await _repository.UpdateIsParentNodeFlagAsync(parent);
		}
	}

	public async Task RemoveRelationsAsync(Guid companyId)
	{
		if (
			!await _repository.ExistsAsChildAsync(companyId) &&
			!await _repository.ExistsAsParentAsync(companyId))
			throw new InvalidOperationException(
				$"Company {companyId} has no relations");

		(bool hasChildren, Guid? parentId) = await _repository
			.RemoveRelationsAsync(companyId);

		if (!hasChildren && parentId is Guid id)
			await UnmarkCompanyAsParentIfNeededAsync(id);

		await UnmarkCompanyAsParentIfNeededAsync(companyId);
	}

	public async Task<List<Company>> GetAllRelationsAsync(Guid companyId)
	{
		List<Company> companies = await _repository.GetAllRelationsAsync(companyId);
		return companies;
	}

	private async Task UnmarkCompanyAsParentIfNeededAsync(Guid companyId)
	{
		Company company = (await _repository.GetByIdAsync(companyId))!;
		if (!await _repository.ExistsAsParentAsync(companyId) && company.IsParentNode)
		{
			company.UnmarkAsParentNode();
			await _repository.UpdateIsParentNodeFlagAsync(company);
		}
	}
}