using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Interfaces.Services;
using Schedule.Contracts.Dtos.StaffRelated.EventScheduleStaff.Requests;
using Schedule.Contracts.Dtos.StaffRelated.EventScheduleStaff.Responses;
using Schedule.Contracts.Dtos.StaffRelated.Staff.Requests;
using Schedule.Contracts.Dtos.StaffRelated.Staff.Responses;
using Schedule.Contracts.Dtos.StaffRelated.StaffAvailability.Requests;
using Schedule.Contracts.Dtos.StaffRelated.StaffAvailability.Responses;
using Schedule.Contracts.Dtos.StaffRelated.StaffSpecializations.Requests;
using Schedule.Contracts.Dtos.StaffRelated.StaffSpecializations.Responses;
using Schedule.Domain.Models.StaffRelated;

namespace PlannerNet.Controllers;

[ApiController]
[Route("api/[controller]")]
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

	[HttpGet]
	public async Task<ActionResult<List<StaffResponse>>> GetAll()
	{
		List<Staff> staffList = await _staffService.GetAllAsync();
		return Ok(_mapper.Map<List<StaffResponse>>(staffList));
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<StaffResponse>> GetById(Guid id)
	{
		Staff? staff = await _staffService.GetByIdAsync(id);
		if (staff == null)
			return NotFound();

		return Ok(_mapper.Map<StaffResponse>(staff));
	}

	[HttpPost]
	public async Task<ActionResult<Guid>> Create([FromBody] CreateStaffRequest request)
	{
		Staff? staff = _mapper.Map<Staff>(request);
		Guid id = await _staffService.CreateAsync(staff);
		return Ok(id);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> Update(Guid id, [FromBody] UpdateStaffRequest request)
	{
		Staff? existing = await _staffService.GetByIdAsync(id);
		if (existing == null)
			return NotFound();

		existing.Role = request.Role;
		existing.FirstName = request.FirstName;
		existing.LastName = request.LastName;
		existing.Phone = request.Phone;

		await _staffService.UpdateAsync(existing);
		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		await _staffService.DeleteAsync(id);
		return NoContent();
	}

	[HttpGet("{staffId}/specializations")]
	public async Task<ActionResult<List<StaffSpecializationResponse>>> GetSpecializations(Guid staffId)
	{
		List<StaffSpecialization> list = await _specializationService.GetByStaffIdAsync(staffId);
		return Ok(_mapper.Map<List<StaffSpecializationResponse>>(list));
	}

	[HttpPost("{staffId}/specializations")]
	public async Task<ActionResult<Guid>> AddSpecialization(Guid staffId, [FromBody] CreateStaffSpecializationRequest request)
	{
		if (staffId != request.StaffId)
			return BadRequest("StaffId in route does not match body.");

		StaffSpecialization? specialization = _mapper.Map<StaffSpecialization>(request);
		Guid id = await _specializationService.CreateAsync(specialization);
		return Ok(id);
	}

	[HttpDelete("specializations/{id}")]
	public async Task<IActionResult> DeleteSpecialization(Guid id)
	{
		await _specializationService.DeleteAsync(id);
		return NoContent();
	}

	[HttpGet("{staffId}/availability")]
	public async Task<ActionResult<List<StaffAvailabilityResponse>>> GetAvailabilityByStaffId(Guid staffId)
	{
		List<StaffAvailability> list = await _availabilityService.GetByStaffIdAsync(staffId);
		return Ok(_mapper.Map<List<StaffAvailabilityResponse>>(list));
	}

	[HttpGet("availability/{id}")]
	public async Task<ActionResult<StaffAvailabilityResponse>> GetAvailabilityById(Guid id)
	{
		StaffAvailability? availability = await _availabilityService.GetByIdAsync(id);
		if (availability == null)
			return NotFound();

		return Ok(_mapper.Map<StaffAvailabilityResponse>(availability));
	}

	[HttpPost("{staffId}/availability")]
	public async Task<ActionResult<Guid>> CreateAvailability(Guid staffId, [FromBody] CreateStaffAvailabilityRequest request)
	{
		if (staffId != request.StaffId)
			return BadRequest("StaffId in route does not match body.");

		StaffAvailability? entity = _mapper.Map<StaffAvailability>(request);
		Guid id = await _availabilityService.CreateAsync(entity);
		return Ok(id);
	}

	[HttpPut("availability/{id}")]
	public async Task<IActionResult> UpdateAvailability(Guid id, [FromBody] UpdateStaffAvailabilityRequest request)
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
	public async Task<IActionResult> DeleteAvailability(Guid id)
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
	public async Task<ActionResult<Guid>> AssignStaffToEvent(Guid eventId, [FromBody] CreateEventScheduleStaffRequest request)
	{
		if (eventId != request.EventScheduleId)
			return BadRequest("EventScheduleId in route does not match body.");

		EventScheduleStaff? entity = _mapper.Map<EventScheduleStaff>(request);
		Guid id = await _eventScheduleStaffService.CreateAsync(entity);
		return Ok(id);
	}

	[HttpDelete("eventschedules/staff/{id}")]
	public async Task<IActionResult> UnassignStaffFromEvent(Guid id)
	{
		await _eventScheduleStaffService.DeleteAsync(id);
		return NoContent();
	}
}
