using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Interfaces.Services;
using Schedule.Contracts.Dtos.Requests;
using Schedule.Contracts.Dtos.Responses;
using Schedule.Domain.Models;

namespace Schedule.API.Controllers;

[ApiController]
[Route("api/[controller]/{companyId:guid}")]
public class SpecializationController : ControllerBase
{
	private readonly ISpecializationService _service;
	private readonly IMapper _mapper;

	public SpecializationController(ISpecializationService service, IMapper mapper)
	{
		_service = service;
		_mapper = mapper;
	}

	[HttpGet]
	public async Task<ActionResult<List<SpecializationResponse>>> GetAll(Guid companyId)
	{
		List<Specialization> result = await _service.GetAllAsync(companyId);
		List<SpecializationResponse> response = _mapper.Map<List<SpecializationResponse>>(result);
		return Ok(response);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<SpecializationResponse>> GetById(Guid id, Guid companyId)
	{
		Specialization? specialization = await _service.GetByIdAsync(id, companyId);
		if (specialization == null)
			return NotFound();

		SpecializationResponse? response = _mapper.Map<SpecializationResponse>(specialization);
		return Ok(response);
	}

	[HttpPost]
	public async Task<ActionResult<Guid>> Create(Guid companyId, [FromBody] CreateSpecializationRequest request)
	{
		Specialization? specialization = _mapper.Map<Specialization>(request);
		specialization.SetCompanyId(companyId);
		Guid id = await _service.CreateAsync(specialization);
		return CreatedAtAction(nameof(Create), id);
	}

	[HttpPut("{id:guid}")]
	public async Task<ActionResult> Update(Guid id, Guid companyId, [FromBody] UpdateSpecializationRequest request)
	{
		Specialization? specialization = await _service.GetByIdAsync(id, companyId);
		if (specialization == null) 
			return NotFound();

		_mapper.Map(request, specialization);
		Boolean success = await _service.UpdateAsync(specialization);
		if (!success) 
			return NotFound();

		return NoContent();
	}

	[HttpDelete("{id:guid}")]
	public async Task<ActionResult> Delete(Guid id, Guid companyId)
	{
		Boolean success = await _service.DeleteAsync(id, companyId);
		if (!success) 
			return NotFound();
		return NoContent();
	}
}
