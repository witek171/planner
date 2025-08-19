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

	[HttpPut("{companyId:guid}/markAsParentNode")]
	public async Task<ActionResult> MarkAsParentNode(Guid companyId)
	{
		Company? company = await _companyService
			.GetByIdAsync(companyId);
		if (company == null) return NotFound();

		company.MarkAsParentNode();
		await _companyService.UpdateIsParentNodeFlagAsync(company);
		return NoContent();
	}

	[HttpPut("{companyId:guid}/unmarkAsParentNode")]
	public async Task<ActionResult> UnmarkAsParentNode(Guid companyId)
	{
		Company? company = await _companyService
			.GetByIdAsync(companyId);
		if (company == null) return NotFound();

		company.UnmarkAsParentNode();
		await _companyService.UpdateIsParentNodeFlagAsync(company);
		return NoContent();
	}

	[HttpPut("{companyId:guid}/markAsReception")]
	public async Task<ActionResult> MarkAsReception(Guid companyId)
	{
		Company? company = await _companyService
			.GetByIdAsync(companyId);
		if (company == null) return NotFound();

		company.MarkAsReception();
		await _companyService.UpdateIsReceptionFlagAsync(company);
		return NoContent();
	}

	[HttpPut("{companyId:guid}/unmarkAsReception")]
	public async Task<ActionResult> UnmarkAsReception(Guid companyId)
	{
		Company? company = await _companyService
			.GetByIdAsync(companyId);
		if (company == null) return NotFound();

		company.UnmarkAsReception();
		await _companyService.UpdateIsReceptionFlagAsync(company);
		return NoContent();
	}
	
	// [HttpPost("{parentId:guid}/children/{childId:guid}")]
	// public async Task<ActionResult> AddChildCompany(
	// 	Guid parentId,
	// 	Guid childId)
	// {
	// 	var success = await _companyService.AddChildCompanyAsync(parentId, childId);
	// 	if (!success)
	// 		return BadRequest();
	//
	// 	return Ok();
	// }
	//
	// [HttpGet]
	// public async Task<ActionResult<CompanyResponse>> GetAll(
	// 	[FromQuery] Guid companyId
	// )
	// {
	// 	// CompanyResponse response = _mapper.Map<CompanyResponse>(company);
	// 	return Ok(response);
	// }
}