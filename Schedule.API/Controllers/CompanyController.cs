using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Interfaces.Services;
using Schedule.Contracts.Dtos.Requests;
using Schedule.Contracts.Dtos.Responses;
using Schedule.Domain.Models;

namespace PlannerNet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
	private readonly ICompanyService _companyService;
	private readonly IMapper _mapper;

	public CompanyController(
		ICompanyService companyService,
		IMapper mapper)
	{
		_companyService = companyService;
		_mapper = mapper;
	}

	[HttpPost]
	public async Task<ActionResult<Guid>> Create(
		[FromBody] CreateCompanyRequest request
	)
	{
		Company company = _mapper.Map<Company>(request);

		Guid companyId = await _companyService.CreateAsync(company);
		return CreatedAtAction(nameof(Create), companyId);
	}

	[HttpPut("{companyId:guid}")]
	public async Task<ActionResult> Put(
		Guid companyId,
		[FromBody] UpdateCompanyRequest request)
	{
		Company? company = await _companyService
			.GetByIdAsync(companyId);
		if (company == null) return NotFound();

		_mapper.Map(request, company);

		await _companyService.PutAsync(company);
		return NoContent();
	}

	[HttpDelete("{companyId:guid}")]
	public async Task<ActionResult> DeleteById(Guid companyId)
	{
		await _companyService.DeleteByIdAsync(companyId);
		return NoContent();
	}

	[HttpGet("byId")]
	public async Task<ActionResult<CompanyResponse>> GetById(
		[FromQuery] Guid companyId
	)
	{
		Company? company = await _companyService
			.GetByIdAsync(companyId);
		if (company == null) return NotFound();

		CompanyResponse response = _mapper.Map<CompanyResponse>(company);
		return Ok(response);
	}

	[HttpPut("{companyId:guid}/markAsReception")]
	public async Task<ActionResult> MarkAsReception(Guid companyId)
	{
		Company? company = await _companyService
			.GetByIdAsync(companyId);
		if (company == null) return NotFound();

		await _companyService.MarkAsReceptionAsync(company);
		return NoContent();
	}

	[HttpPut("{companyId:guid}/unmarkAsReception")]
	public async Task<ActionResult> UnmarkAsReception(Guid companyId)
	{
		Company? company = await _companyService
			.GetByIdAsync(companyId);
		if (company == null) return NotFound();

		await _companyService.UnmarkAsReceptionAsync(company);
		return NoContent();
	}

	[HttpPost("{companyId:guid}/addTo/{parentCompanyId:guid}")]
	public async Task<ActionResult> AddToParent(
		Guid companyId,
		Guid parentCompanyId)
	{
		Company? company = await _companyService.GetByIdAsync(companyId);
		Company? parentCompany = await _companyService.GetByIdAsync(parentCompanyId);
		if (company == null || parentCompany == null) return NotFound();

		await _companyService.AddRelationAsync(companyId, parentCompanyId);
		return Ok();
	}

	[HttpDelete("{companyId:guid}/parent/{parentId:guid}")]
	public async Task<ActionResult> DetachFromParent(
		Guid companyId,
		Guid parentCompanyId)
	{
		Company? company = await _companyService.GetByIdAsync(companyId);
		Company? parentCompany = await _companyService.GetByIdAsync(parentCompanyId);
		if (company == null || parentCompany == null) return NotFound();

		await _companyService.RemoveRelationAsync(companyId, parentCompanyId);
		return NoContent();
	}

	[HttpDelete("{companyId:guid}/parents")]
	public async Task<ActionResult> DetachFromAllParents(Guid companyId)
	{
		Company? company = await _companyService.GetByIdAsync(companyId);
		if (company == null) return NotFound();

		await _companyService.RemoveAllRelationsAsync(companyId);
		return NoContent();
	}

	[HttpGet("{companyId:guid}/relations")]
	public async Task<ActionResult> GetRelations(Guid companyId)
	{
		Company? company = await _companyService.GetByIdAsync(companyId);
		if (company == null) return NotFound();

		List<Company> companies = await _companyService
			.GetAllRelationsAsync(companyId);

		List<CompanyResponse> responses = _mapper
			.Map<List<CompanyResponse>>(companies);
		return Ok(responses);
	}
}