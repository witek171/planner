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
		List<StaffMember> staff = await _staffMemberService.GetAllAsync(companyId);
		List<StaffMemberResponse> response = _mapper.Map<List<StaffMemberResponse>>(staff);
		return Ok(response);
	}

	[HttpGet("byId")]
	public async Task<ActionResult<StaffMemberResponse>> GetById(
		[FromQuery] Guid staffMemberId,
		Guid companyId)
	{
		StaffMember? staffMember = await _staffMemberService
			.GetByIdAsync(staffMemberId, companyId);
		if (staffMember == null)
			return NotFound();

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
		if (staffMember == null) return NotFound();

		_mapper.Map(request, staffMember);
		await _staffMemberService.PutAsync(staffMember!);
		return NoContent();
	}

	[HttpDelete("{staffMemberId:guid}")]
	public async Task<ActionResult> Delete(
		Guid staffMemberId,
		Guid companyId)
	{
		await _staffMemberService.DeleteAsync(staffMemberId, companyId);
		return NoContent();
	}

	[HttpPost("specialization")]
	public async Task<ActionResult<Guid>> CreateStaffMemberSpecialization(
		Guid companyId,
		[FromBody] CreateStaffMemberSpecializationRequest request)
	{
		StaffMemberSpecialization? staffMemberSpecialization = _mapper
			.Map<StaffMemberSpecialization>(request);

		staffMemberSpecialization.SetCompanyId(companyId);

		Guid id = await _staffMemberSpecializationService
			.CreateAsync(companyId, staffMemberSpecialization);
		return CreatedAtAction(nameof(CreateStaffMemberSpecialization), id);
	}

	[HttpDelete("specialization/{staffMemberSpecializationId:guid}")]
	public async Task<ActionResult> DeleteStaffMemberSpecialization(
		Guid companyId,
		Guid staffMemberSpecializationId)
	{
		await _staffMemberSpecializationService
			.DeleteAsync(companyId, staffMemberSpecializationId);
		return NoContent();
	}

	[HttpGet("availability/byStaffMemberId")]
	public async Task<ActionResult<List<StaffMemberAvailabilityResponse>>> GetAvailabilityByStaffMemberId(
		Guid companyId,
		[FromQuery] Guid staffMemberId)
	{
		StaffMember? staffMember = await _staffMemberService
			.GetByIdAsync(staffMemberId, companyId);
		if (staffMember == null) return NotFound();

		List<StaffMemberAvailability> list =
			await _staffMemberAvailabilityService
				.GetByStaffMemberIdAsync(companyId, staffMemberId);

		List<StaffMemberAvailabilityResponse> responses = _mapper
			.Map<List<StaffMemberAvailabilityResponse>>(list);
		return Ok(responses);
	}

	[HttpGet("availability/byId")]
	public async Task<ActionResult<StaffMemberAvailabilityResponse>> GetAvailabilityById(
		Guid companyId,
		[FromQuery] Guid id)
	{
		StaffMemberAvailability? availability = await _staffMemberAvailabilityService
			.GetByIdAsync(companyId, id);
		if (availability == null) return NotFound();

		StaffMember staffMember = (await _staffMemberService
			.GetByIdAsync(availability.StaffMemberId, companyId))!;
		StaffMemberResponse staffMemberResponse = _mapper
			.Map<StaffMemberResponse>(staffMember);

		StaffMemberAvailabilityResponse baseResponse = _mapper
			.Map<StaffMemberAvailabilityResponse>(availability);

		StaffMemberAvailabilityResponse response = new(
			baseResponse.Date,
			baseResponse.StartTime,
			baseResponse.EndTime,
			staffMemberResponse
		);

		return Ok(response);
	}

	[HttpPost("availability/{staffMemberId:guid}")]
	public async Task<ActionResult<Guid>> CreateAvailability(
		Guid companyId,
		Guid staffMemberId,
		[FromBody] CreateStaffMemberAvailabilityRequest request)
	{
		StaffMember? staffMember = await _staffMemberService
			.GetByIdAsync(staffMemberId, companyId);
		if (staffMember == null) return NotFound();

		StaffMemberAvailability? availability = _mapper.Map<StaffMemberAvailability>(request);
		availability.SetCompanyId(companyId);
		availability.SetStaffMemberId(staffMemberId);

		Guid id = await _staffMemberAvailabilityService.CreateAsync(availability);
		return CreatedAtAction(nameof(CreateAvailability), id);
	}

	[HttpDelete("availability/{availabilityId:guid}")]
	public async Task<ActionResult> DeleteAvailability(
		Guid companyId,
		Guid availabilityId)
	{
		await _staffMemberAvailabilityService.DeleteAsync(companyId, availabilityId);
		return NoContent();
	}

	// trzeba by zrobic encje eventSchedule i eventScheduleRespone ktore bylo by tu zwracane
	[HttpGet("eventschedule")]
	public async Task<ActionResult<List<EventScheduleStaffMemberResponse>>> GetStaffMemberEventSchedules(
		Guid companyId,
		[FromQuery] Guid staffMemberId)
	{
		List<EventScheduleStaffMember> events = await _eventScheduleStaffMemberService
			.GetByStaffMemberIdAsync(companyId, staffMemberId);

		List<EventScheduleStaffMemberResponse> responses = _mapper
			.Map<List<EventScheduleStaffMemberResponse>>(events);
		return Ok(responses);
	}

	// nie dziala, raczej problem z mapowaniem
	[HttpPost("eventschedule")]
	public async Task<ActionResult<Guid>> AssignStaffMemberToEvent(
		Guid companyId,
		[FromBody] CreateEventScheduleStaffMemberRequest request)
	{
		EventScheduleStaffMember? eventScheduleStaffMember = _mapper
			.Map<EventScheduleStaffMember>(request);
		eventScheduleStaffMember.SetCompanyId(companyId);

		Guid id = await _eventScheduleStaffMemberService
			.CreateAsync(eventScheduleStaffMember);
		return Ok(id);
	}

	[HttpDelete("eventschedule/{eventScheduleStaffMemberId:guid}")]
	public async Task<ActionResult> UnassignStaffMemberFromEvent(
		Guid companyId,
		Guid eventScheduleStaffMemberId)
	{
		await _eventScheduleStaffMemberService.DeleteAsync(companyId, eventScheduleStaffMemberId);
		return NoContent();
	}
}