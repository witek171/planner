using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Interfaces.Services;
using Schedule.Contracts.Dtos;
using Schedule.Domain.Models;

namespace PlannerNet.Controllers;

[ApiController]
[Route("api/[controller]/{companyId:guid}")]
public class ParticipantController : ControllerBase
{
	private readonly IParticipantService _participantService;
	private readonly IMapper _mapper;

	public ParticipantController(
		IParticipantService participantService,
		IMapper mapper
	)
	{
		_participantService = participantService;
		_mapper = mapper;
	}

	[HttpPost]
	public async Task<ActionResult> Create(
		Guid companyId,
		[FromBody] ParticipantCreateRequest request
	)
	{
		request.CompanyId = companyId;
		Participant participant = _mapper.Map<Participant>(request);

		await _participantService.CreateAsync(participant);
		return CreatedAtAction(nameof(Create), participant.Id);
	}

	[HttpPatch("{participantId:guid}")]
	public async Task<ActionResult> Update(
		Guid companyId, Guid participantId, [FromBody] ParticipantUpdateRequest request)
	{
		// obecnie podawane dane nie sa formatowane (mozna podac email duzymi literami i whitespacey)
		Participant? existing = await _participantService.GetByIdAsync(participantId, companyId);

		_mapper.Map(request, existing);

		await _participantService.PatchAsync(existing);
		return NoContent();
	}

	[HttpDelete("{email}")]
	public async Task<ActionResult> Delete(
		Guid companyId,
		Guid id
	)
	{
		await _participantService.DeleteByIdAsync(id, companyId);
		return NoContent();
	}

	[HttpGet]
	public async Task<ActionResult> GetByEmail(
		[FromQuery] string email,
		Guid companyId
	)
	{
		Participant? participant = await _participantService.GetByEmailAsync(email, companyId);

		ParticipantResponse response = _mapper.Map<ParticipantResponse>(participant);
		return Ok(response);
	}

	[HttpGet("all")]
	public async Task<ActionResult> GetAll(Guid companyId)
	{
		List<Participant> participants = await _participantService.GetAllAsync(companyId);

		List<ParticipantResponse> response = _mapper.Map<List<ParticipantResponse>>(participants);
		return Ok(response);
	}
}