using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Interfaces.Services;
using Schedule.Contracts.Dtos.Requests;
using Schedule.Contracts.Dtos.Responses;
using Schedule.Domain.Models;

namespace PlannerNet.Controllers;

[ApiController]
[Route("api/[controller]/{companyId:guid}")]
public class StaffMemberController : ControllerBase
{
	private readonly IStaffMemberService _staffMemberService;
	private readonly IStaffMemberSpecializationService _staffMemberSpecializationService;
	private readonly IStaffMemberAvailabilityService _staffMemberAvailabilityService;
	private readonly IEventScheduleStaffMemberService _eventScheduleStaffMemberService;
	private readonly IMapper _mapper;

	public StaffMemberController(
		IStaffMemberService staffMemberService,
		IStaffMemberSpecializationService staffMemberSpecializationService,
		IStaffMemberAvailabilityService staffMemberAvailabilityService,
		IEventScheduleStaffMemberService eventScheduleStaffMemberService,
		IMapper mapper)
	{
		_staffMemberService = staffMemberService;
		_staffMemberSpecializationService = staffMemberSpecializationService;
		_staffMemberAvailabilityService = staffMemberAvailabilityService;
		_eventScheduleStaffMemberService = eventScheduleStaffMemberService;
		_mapper = mapper;
	}

	[HttpGet("all")]
	public async Task<ActionResult<List<StaffMemberResponse>>> GetAll(Guid companyId)
	{
		List<StaffMember> staffMemberList = await _staffMemberService.GetAllAsync(companyId);

		List<StaffMemberResponse> response = _mapper
			.Map<List<StaffMemberResponse>>(staffMemberList);
		return Ok(response);
	}

	[HttpGet("byId")]
	public async Task<ActionResult<StaffMemberResponse>> GetById(
		[FromQuery] Guid staffMemberId,
		Guid companyId)
	{
		StaffMember? staffMember = await _staffMemberService
			.GetByIdAsync(staffMemberId, companyId);

		StaffMemberResponse response = _mapper.Map<StaffMemberResponse>(staffMember);
		return Ok(response);
	}

	[HttpPost]
	public async Task<ActionResult<Guid>> Create(
		Guid companyId,
		[FromBody] CreateStaffMemberRequest request)
	{
		StaffMember staffMember = _mapper.Map<StaffMember>(request);
		staffMember.SetCompanyId(companyId);

		Guid staffMemberId = await _staffMemberService.CreateAsync(staffMember);
		return CreatedAtAction(nameof(Create), staffMemberId);
	}

	[HttpPut("{staffMemberId:guid}")]
	public async Task<ActionResult> Put(
		Guid staffMemberId,
		Guid companyId,
		[FromBody] UpdateStaffMemberRequest request)
	{
		StaffMember? staffMember = await _staffMemberService
			.GetByIdAsync(staffMemberId, companyId);

		_mapper.Map(request, staffMember);

		await _staffMemberService.PutAsync(staffMember!);
		return NoContent();
	}

	[HttpDelete("{staffMemberId:guid}")]
	public async Task<ActionResult> Delete(
		Guid companyId,
		Guid staffMemberId)
	{
		await _staffMemberService.DeleteAsync(staffMemberId, companyId);
		return NoContent();
	}

	[HttpGet("specializations")]
	public async Task<ActionResult<List<SpecializationResponse>>> GetStaffMemberSpecializations(
		[FromQuery] Guid staffMemberId,
		Guid companyId)
	{
		List<Specialization> specializations = await _staffMemberSpecializationService
			.GetStaffMemberSpecializationsAsync(staffMemberId, companyId);

		List<SpecializationResponse> responses = _mapper
			.Map<List<SpecializationResponse>>(specializations);
		return Ok(responses);
	}

	[HttpGet("specializations/all")]
	// zwracanie wszystkich pracownikow z ich specjalizacjami,
	// jakie dane pracownikow zwracamy?(imie, nazwisko, id, rola)
	
	// czy robic put gdzie bede w staffMemberSpecializations
	// edytowal specializationId dla pracownika?
	
	[HttpPost("specializations")]
	public async Task<ActionResult<Guid>> CreateStaffMemberSpecialization(
		Guid companyId,
		[FromBody] CreateStaffMemberSpecializationRequest request)
	{
		StaffMemberSpecialization? staffMemberSpecialization = _mapper
			.Map<StaffMemberSpecialization>(request);

		staffMemberSpecialization.SetCompanyId(companyId);

		Guid id = await _staffMemberSpecializationService
			.CreateAsync(companyId, staffMemberSpecialization);
		return CreatedAtAction(nameof(Create), id);
	}

	[HttpDelete("specializations/{staffMemberSpecializationId:guid}")]
	public async Task<ActionResult> DeleteStaffMemberSpecialization(
		Guid companyId,
		Guid staffMemberSpecializationId)
	{
		await _staffMemberSpecializationService
			.DeleteAsync(companyId, staffMemberSpecializationId);
		return NoContent();
	}

	// od tad jeszcze nie do konca poprawione
	[HttpGet("{staffMemberId}/availability")]
	public async Task<ActionResult<List<StaffMemberAvailabilityResponse>>> GetAvailabilityByStaffMemberId(
		Guid staffMemberId)
	{
		List<StaffMemberAvailability> list =
			await _staffMemberAvailabilityService.GetByStaffMemberIdAsync(staffMemberId);

		List<StaffMemberAvailabilityResponse> responses = _mapper.Map<List<StaffMemberAvailabilityResponse>>(list);
		return Ok(responses);
	}

	[HttpGet("availability/{id}")]
	public async Task<ActionResult<StaffMemberAvailabilityResponse>> GetAvailabilityById(Guid id)
	{
		StaffMemberAvailability? availability = await _staffMemberAvailabilityService.GetByIdAsync(id);

		StaffMemberAvailabilityResponse response = _mapper.Map<StaffMemberAvailabilityResponse>(availability);
		return Ok(response);
	}

	[HttpPost("{staffMemberId}/availability")]
	public async Task<ActionResult<Guid>> CreateAvailability(
		Guid staffMemberId,
		[FromBody] CreateStaffMemberAvailabilityRequest request)
	{
		if (staffMemberId != request.StaffMemberId)
			return BadRequest("StaffMemberId in route does not match body.");

		StaffMemberAvailability? entity = _mapper.Map<StaffMemberAvailability>(request);
		Guid id = await _staffMemberAvailabilityService.CreateAsync(entity);
		return Ok(id);
	}

	[HttpPut("availability/{id}")]
	public async Task<ActionResult> UpdateAvailability(
		Guid id,
		[FromBody] UpdateStaffMemberAvailabilityRequest request)
	{
		StaffMemberAvailability? existing = await _staffMemberAvailabilityService.GetByIdAsync(id);
		if (existing == null)
			return NotFound();

		existing.Date = request.Date;
		existing.StartTime = request.StartTime;
		existing.EndTime = request.EndTime;
		existing.IsAvailable = request.IsAvailable;

		await _staffMemberAvailabilityService.UpdateAsync(existing);
		return NoContent();
	}

	[HttpDelete("availability/{id}")]
	public async Task<ActionResult> DeleteAvailability(Guid id)
	{
		await _staffMemberAvailabilityService.DeleteAsync(id);
		return NoContent();
	}

	[HttpGet("eventschedules/{eventId}/staffMember")]
	public async Task<ActionResult<List<EventScheduleStaffMemberResponse>>> GetStaffMemberAssignedToEvent(Guid eventId)
	{
		List<EventScheduleStaffMember> list = await _eventScheduleStaffMemberService.GetByEventIdAsync(eventId);
		return Ok(_mapper.Map<List<EventScheduleStaffMemberResponse>>(list));
	}

	[HttpPost("eventschedules/{eventId}/staffMember")]
	public async Task<ActionResult<Guid>> AssignStaffMemberToEvent(Guid eventId,
		[FromBody] CreateEventScheduleStaffMemberRequest request)
	{
		if (eventId != request.EventScheduleId)
			return BadRequest("EventScheduleId in route does not match body.");

		EventScheduleStaffMember? entity = _mapper.Map<EventScheduleStaffMember>(request);
		Guid id = await _eventScheduleStaffMemberService.CreateAsync(entity);
		return Ok(id);
	}

	[HttpDelete("eventschedules/staffMember/{id}")]
	public async Task<ActionResult> UnassignStaffMemberFromEvent(Guid id)
	{
		await _eventScheduleStaffMemberService.DeleteAsync(id);
		return NoContent();
	}
}