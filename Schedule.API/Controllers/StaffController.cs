using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Interfaces.Services;
using Schedule.Contracts.Dtos.Requests;
using Schedule.Contracts.Dtos.Responses;
using Schedule.Domain.Models;

namespace PlannerNet.Controllers;

[ApiController]
[Route("api/[controller]/{companyId:guid}")]
public class StaffController : ControllerBase
{
	private readonly IStaffService _staffService;
	private readonly IStaffSpecializationService _specializationService;
	private readonly IStaffAvailabilityService _availabilityService;
	private readonly IEventScheduleStaffService _eventScheduleStaffService;
	private readonly IMapper _mapper;

	public StaffController(
		IStaffService staffService,
		IStaffSpecializationService specializationService,
		IStaffAvailabilityService availabilityService,
		IEventScheduleStaffService eventScheduleStaffService,
		IMapper mapper)
	{
		_staffService = staffService;
		_specializationService = specializationService;
		_availabilityService = availabilityService;
		_eventScheduleStaffService = eventScheduleStaffService;
		_mapper = mapper;
	}

	[HttpGet("all")]
	public async Task<ActionResult<List<StaffResponse>>> GetAll()
	{
		List<Staff> staffList = await _staffService.GetAllAsync();

		List<StaffResponse> response = _mapper.Map<List<StaffResponse>>(staffList);
		return Ok(response);
	}

	[HttpGet("byId")]
	public async Task<ActionResult<StaffResponse>> GetById(Guid staffId)
	{
		Staff? staff = await _staffService.GetByIdAsync(staffId);

		StaffResponse response = _mapper.Map<StaffResponse>(staff);
		return Ok(response);
	}

	[HttpPost]
	public async Task<ActionResult<Guid>> Create(
		Guid companyId,
		[FromBody] CreateStaffRequest request)
	{
		Staff? staff = _mapper.Map<Staff>(request);
		staff.SetCompanyId(companyId);

		Guid staffId = await _staffService.CreateAsync(staff);
		return CreatedAtAction(nameof(Create), staffId);
	}

	[HttpPut("{staffId:guid}")]
	public async Task<ActionResult> Put(
		Guid staffId,
		[FromBody] UpdateStaffRequest request)
	{
		Staff? staff = await _staffService.GetByIdAsync(staffId);


		await _staffService.UpdateAsync(staff);
		return NoContent();
	}

	[HttpDelete("{staffId:guid}")]
	public async Task<ActionResult> Delete(
		Guid companyId,
		Guid staffId)
	{
		await _staffService.DeleteAsync(staffId, companyId);
		return NoContent();
	}

	[HttpGet("{staffId}/specializations")]
	public async Task<ActionResult<List<StaffSpecializationResponse>>> GetSpecializations(Guid staffId)
	{
		List<StaffSpecialization> list = await _specializationService.GetByStaffIdAsync(staffId);
		
		List<StaffSpecializationResponse> responses = _mapper.Map<List<StaffSpecializationResponse>>(list);
		return Ok(responses);
	}

	[HttpPost("{staffId}/specializations")]
	public async Task<ActionResult<Guid>> AddSpecialization(Guid staffId,
		[FromBody] CreateStaffSpecializationRequest request)
	{
		if (staffId != request.StaffId)
			return BadRequest("StaffId in route does not match body.");

		StaffSpecialization? specialization = _mapper.Map<StaffSpecialization>(request);
		Guid id = await _specializationService.CreateAsync(specialization);
		return Ok(id);
	}

	[HttpDelete("specializations/{id}")]
	public async Task<ActionResult> DeleteSpecialization(Guid id)
	{
		await _specializationService.DeleteAsync(id);
		return NoContent();
	}

	[HttpGet("{staffId}/availability")]
	public async Task<ActionResult<List<StaffAvailabilityResponse>>> GetAvailabilityByStaffId(Guid staffId)
	{
		List<StaffAvailability> list = await _availabilityService.GetByStaffIdAsync(staffId);
		
		List<StaffAvailabilityResponse> responses = _mapper.Map<List<StaffAvailabilityResponse>>(list);
		return Ok(responses);
	}

	[HttpGet("availability/{id}")]
	public async Task<ActionResult<StaffAvailabilityResponse>> GetAvailabilityById(Guid id)
	{
		StaffAvailability? availability = await _availabilityService.GetByIdAsync(id);

		StaffAvailabilityResponse response = _mapper.Map<StaffAvailabilityResponse>(availability);
		return Ok(response);
	}

	[HttpPost("{staffId}/availability")]
	public async Task<ActionResult<Guid>> CreateAvailability(
		Guid staffId,
		[FromBody] CreateStaffAvailabilityRequest request)
	{
		if (staffId != request.StaffId)
			return BadRequest("StaffId in route does not match body.");

		StaffAvailability? entity = _mapper.Map<StaffAvailability>(request);
		Guid id = await _availabilityService.CreateAsync(entity);
		return Ok(id);
	}

	[HttpPut("availability/{id}")]
	public async Task<ActionResult> UpdateAvailability(
		Guid id,
		[FromBody] UpdateStaffAvailabilityRequest request)
	{
		StaffAvailability? existing = await _availabilityService.GetByIdAsync(id);
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

	[HttpGet("eventschedules/{eventId}/staff")]
	public async Task<ActionResult<List<EventScheduleStaffResponse>>> GetStaffAssignedToEvent(Guid eventId)
	{
		List<EventScheduleStaff> list = await _eventScheduleStaffService.GetByEventIdAsync(eventId);
		return Ok(_mapper.Map<List<EventScheduleStaffResponse>>(list));
	}

	[HttpPost("eventschedules/{eventId}/staff")]
	public async Task<ActionResult<Guid>> AssignStaffToEvent(Guid eventId,
		[FromBody] CreateEventScheduleStaffRequest request)
	{
		if (eventId != request.EventScheduleId)
			return BadRequest("EventScheduleId in route does not match body.");

		EventScheduleStaff? entity = _mapper.Map<EventScheduleStaff>(request);
		Guid id = await _eventScheduleStaffService.CreateAsync(entity);
		return Ok(id);
	}

	[HttpDelete("eventschedules/staff/{id}")]
	public async Task<ActionResult> UnassignStaffFromEvent(Guid id)
	{
		await _eventScheduleStaffService.DeleteAsync(id);
		return NoContent();
	}
}