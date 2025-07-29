using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Interfaces.Services;
using Schedule.Contracts.Dtos;
using Schedule.Domain.Models;

namespace PlannerNet.Controllers;

[ApiController]
[Route("api/[controller]")]
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

	[HttpPost("{companyId:guid}")]
	public async Task<ActionResult> Create(
		Guid companyId,
		[FromBody] ParticipantCreateRequest request
	)
	{
		request.CompanyId = companyId;

		Participant participant = _mapper.Map<Participant>(request);

		await _participantService.CreateAsync(participant);
		return Created();
	}
}