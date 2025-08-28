using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models;

namespace Schedule.Application.Services;

public class CompanyService : ICompanyService
{
	private readonly ICompanyRepository _companyRepository;

	public CompanyService(ICompanyRepository companyRepository)
	{
		_companyRepository = companyRepository;
	}

	public async Task<Guid> CreateAsync(Company company)
	{
		company.Normalize();
		return await _companyRepository.CreateAsync(company);
	}

	public async Task PutAsync(Company company)
	{
		company.Normalize();
		await _companyRepository.PutAsync(company);
	}

	public async Task DeleteByIdAsync(Guid companyId) => await _companyRepository
		.DeleteByIdAsync(companyId);

	public async Task<Company?> GetByIdAsync(Guid companyId)
	{
		return await _companyRepository.GetByIdAsync(companyId);
	}

	public async Task MarkAsReceptionAsync(Company company)
	{
		company.MarkAsReception();
		await _companyRepository.UpdateIsReceptionFlagAsync(company);
	}

	public async Task UnmarkAsReceptionAsync(Company company)
	{
		company.UnmarkAsReception();
		await _companyRepository.UpdateIsReceptionFlagAsync(company);
	}

	public async Task AddRelationAsync(
		Guid childId,
		Guid parentId)
	{
		if (await _companyRepository.ExistsAsChildAsync(childId))
			throw new InvalidOperationException(
				$"Company {childId} already has a parent company");

		if (await _companyRepository.RelationExistAsync(childId, parentId))
			throw new InvalidOperationException(
				$"Relation between companies {childId} and {parentId} already exists");

		await _companyRepository.AddRelationAsync(childId, parentId);
		Company parent = (await _companyRepository.GetByIdAsync(parentId))!;

		if (!parent.IsParentNode)
		{
			parent.MarkAsParentNode();
			await _companyRepository.UpdateIsParentNodeFlagAsync(parent);
		}
	}

	public async Task RemoveRelationsAsync(Guid companyId)
	{
		if (!await _companyRepository.ExistsAsChildAsync(companyId) &&
			!await _companyRepository.ExistsAsParentAsync(companyId))
			throw new InvalidOperationException(
				$"Company {companyId} is not present in the hierarchy," +
				$" therefore it has no relations to remove");

		(bool hasChildren, Guid? parentId) = await _companyRepository
			.RemoveRelationsAsync(companyId);

		if (!hasChildren && parentId is Guid id)
			await UnmarkCompanyAsParentIfNeededAsync(id);

		await UnmarkCompanyAsParentIfNeededAsync(companyId);
	}

	public async Task<List<Company>> GetAllRelationsAsync(Guid companyId)
	{
		List<Company> companies = await _companyRepository.GetAllRelationsAsync(companyId);
		return companies;
	}

	private async Task UnmarkCompanyAsParentIfNeededAsync(Guid companyId)
	{
		Company company = (await _companyRepository.GetByIdAsync(companyId))!;
		if (!await _companyRepository.ExistsAsParentAsync(companyId) && company.IsParentNode)
		{
			company.UnmarkAsParentNode();
			await _companyRepository.UpdateIsParentNodeFlagAsync(company);
		}
	}
}