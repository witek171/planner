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
// potrzebny try catch skoro createAsyns rzuca wyjatki
		await _participantService.CreateAsync(participant);
// co tu zwracac?
		return Created($"api/participant/{companyId}/{participant.Id}",
			new { message = "participant created" });
	}

	[HttpPatch("{participantId:guid}")]
	public async Task<ActionResult> Update(
		Guid companyId, Guid participantId, [FromBody] ParticipantUpdateRequest request)
	{
		// obecnie podawane dane nie sa formatowane (mozna podac email duzymi literami i whitespacey)
		Participant? existing = await _participantService.GetByIdAsync(participantId, companyId);
		// if (existing == null)
		// 	return NotFound(new { message = "participant not exist" });

		_mapper.Map(request, existing);
// try catch
		await _participantService.PatchAsync(existing);

		return NoContent();
	}

	[HttpDelete("{email}")]
	public async Task<ActionResult> Delete(
		Guid companyId,
		string email
	)
	{
		// bool deleted = 
			await _participantService.DeleteByEmailAsync(email, companyId);

		// if (!deleted)
		// 	return NotFound();

		return NoContent();
	}

	[HttpGet]
	public async Task<ActionResult> GetByEmail(
		[FromQuery] string email,
		Guid companyId
	)
	{
		Participant? participant = await _participantService.GetByEmailAsync(email, companyId);
		if (participant == null)
			return NotFound();

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