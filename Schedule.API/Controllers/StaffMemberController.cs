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
	private readonly IStaffMemberSpecializationService _specializationService;
	private readonly IStaffMemberAvailabilityService _availabilityService;
	private readonly IEventScheduleStaffMemberService _eventScheduleStaffMemberService;
	private readonly IMapper _mapper;

	public StaffMemberController(
		IStaffMemberService staffMemberService,
		IStaffMemberSpecializationService specializationService,
		IStaffMemberAvailabilityService availabilityService,
		IEventScheduleStaffMemberService eventScheduleStaffMemberService,
		IMapper mapper)
	{
		_staffMemberService = staffMemberService;
		_specializationService = specializationService;
		_availabilityService = availabilityService;
		_eventScheduleStaffMemberService = eventScheduleStaffMemberService;
		_mapper = mapper;
	}

	[HttpGet("all")]
	public async Task<ActionResult<List<StaffMemberResponse>>> GetAll(Guid companyId)
	{
		List<StaffMember> staffMemberList = await _staffMemberService.GetAllAsync(companyId);

		List<StaffMemberResponse> response = _mapper.Map<List<StaffMemberResponse>>(staffMemberList);
		return Ok(response);
	}

	[HttpGet("byId")]
	public async Task<ActionResult<StaffMemberResponse>> GetById(
		Guid staffMemberId,
		Guid companyId)
	{
		StaffMember? staffMember = await _staffMemberService.GetByIdAsync(staffMemberId, companyId);

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
		StaffMember? staffMember = await _staffMemberService.GetByIdAsync(staffMemberId, companyId);
		// StaffMember staffMember = _mapper.Map<StaffMember>(request);

		await _staffMemberService.PutAsync(staffMember);
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

	[HttpGet("{staffMemberId}/specializations")]
	public async Task<ActionResult<List<StaffMemberSpecializationResponse>>> GetSpecializations(Guid staffMemberId)
	{
		List<StaffMemberSpecialization> list = await _specializationService.GetByStaffMemberIdAsync(staffMemberId);
		
		List<StaffMemberSpecializationResponse> responses = _mapper.Map<List<StaffMemberSpecializationResponse>>(list);
		return Ok(responses);
	}

	[HttpPost("{staffMemberId}/specializations")]
	public async Task<ActionResult<Guid>> AddSpecialization(Guid staffMemberId,
		[FromBody] CreateStaffMemberSpecializationRequest request)
	{
		if (staffMemberId != request.StaffMemberId)
			return BadRequest("StaffMemberId in route does not match body.");

		StaffMemberSpecialization? specialization = _mapper.Map<StaffMemberSpecialization>(request);
		Guid id = await _specializationService.CreateAsync(specialization);
		return Ok(id);
	}

	[HttpDelete("specializations/{id}")]
	public async Task<ActionResult> DeleteSpecialization(Guid id)
	{
		await _specializationService.DeleteAsync(id);
		return NoContent();
	}

	[HttpGet("{staffMemberId}/availability")]
	public async Task<ActionResult<List<StaffMemberAvailabilityResponse>>> GetAvailabilityByStaffMemberId(Guid staffMemberId)
	{
		List<StaffMemberAvailability> list = await _availabilityService.GetByStaffMemberIdAsync(staffMemberId);
		
		List<StaffMemberAvailabilityResponse> responses = _mapper.Map<List<StaffMemberAvailabilityResponse>>(list);
		return Ok(responses);
	}

	[HttpGet("availability/{id}")]
	public async Task<ActionResult<StaffMemberAvailabilityResponse>> GetAvailabilityById(Guid id)
	{
		StaffMemberAvailability? availability = await _availabilityService.GetByIdAsync(id);

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
		Guid id = await _availabilityService.CreateAsync(entity);
		return Ok(id);
	}

	[HttpPut("availability/{id}")]
	public async Task<ActionResult> UpdateAvailability(
		Guid id,
		[FromBody] UpdateStaffMemberAvailabilityRequest request)
	{
		StaffMemberAvailability? existing = await _availabilityService.GetByIdAsync(id);
		if (existing == null)
			return NotFound();

		existing.Date = request.Date;
		existing.StartTime = request.StartTime;
		existing.EndTime = request.EndTime;
		existing.IsAvailable = request.IsAvailable;

		await _availabilityService.UpdateAsync(existing);
		return NoContent();
	}

	[HttpDelete("availability/{id}")]
	public async Task<ActionResult> DeleteAvailability(Guid id)
	{
		await _availabilityService.DeleteAsync(id);
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