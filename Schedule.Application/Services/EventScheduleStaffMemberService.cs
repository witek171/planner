using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Application.Interfaces.Validators;
using Schedule.Domain.Models;

namespace Schedule.Application.Services;

public class EventScheduleStaffMemberService : IEventScheduleStaffMemberService
{
	private readonly IEventScheduleStaffMemberRepository _eventScheduleStaffMemberRepository;
	private readonly IEventScheduleConflictValidator _eventScheduleConflictValidator;
	private readonly IEventScheduleRepository _eventScheduleRepository;
	private readonly IStaffMemberRepository _staffMemberRepository;

	public EventScheduleStaffMemberService(
		IEventScheduleStaffMemberRepository eventScheduleStaffMemberRepository,
		IEventScheduleConflictValidator eventScheduleConflictValidator,
		IEventScheduleRepository eventScheduleRepository,
		IStaffMemberRepository staffMemberRepository)
	{
		_eventScheduleStaffMemberRepository = eventScheduleStaffMemberRepository;
		_eventScheduleConflictValidator = eventScheduleConflictValidator;
		_eventScheduleRepository = eventScheduleRepository;
		_staffMemberRepository = staffMemberRepository;
	}

	public async Task<List<EventScheduleStaffMember>> GetByStaffMemberIdAsync(
		Guid companyId,
		Guid staffMemberId)
		=> await _eventScheduleStaffMemberRepository
			.GetByStaffMemberIdAsync(companyId, staffMemberId);

	public async Task<Guid> CreateAsync(EventScheduleStaffMember eventScheduleStaffMember)
	{
		await ValidateEventScheduleAsync(eventScheduleStaffMember);
		await ValidateStaffMemberAsync(eventScheduleStaffMember);
		Guid companyId = eventScheduleStaffMember.CompanyId;
		Guid staffMemberId = eventScheduleStaffMember.StaffMemberId;
		Guid eventScheduleId = eventScheduleStaffMember.EventScheduleId;
		EventSchedule eventSchedule = (await _eventScheduleRepository
			.GetByIdAsync(eventScheduleId, companyId))!;
		DateTime startTime = eventSchedule.StartTime;
		DateTime endTime = eventSchedule.EndTime;

		if (!await _eventScheduleConflictValidator
				.CanAssignStaffMemberAsync(companyId, staffMemberId, startTime, endTime))
			throw new InvalidOperationException(
				$"Staff member {staffMemberId} has a time conflict");

		return await _eventScheduleStaffMemberRepository.CreateAsync(eventScheduleStaffMember);
	}

	public async Task DeleteAsync(
		Guid companyId,
		Guid id)
		=> await _eventScheduleStaffMemberRepository.DeleteByIdAsync(companyId, id);

	public async Task<bool> ExistsByIdAsync(
		Guid companyId,
		Guid id)
		=> await _eventScheduleStaffMemberRepository.ExistsByIdAsync(companyId, id);

	private async Task ValidateEventScheduleAsync(EventScheduleStaffMember eventScheduleStaffMember)
	{
		Guid eventScheduleId = eventScheduleStaffMember.EventScheduleId;
		Guid companyId = eventScheduleStaffMember.CompanyId;

		EventSchedule? eventSchedule = await _eventScheduleRepository
			.GetByIdAsync(eventScheduleId, companyId);
		if (eventSchedule == null)
			throw new InvalidOperationException(
				$"Event Schedule {eventScheduleId} not found");
	}

	private async Task ValidateStaffMemberAsync(EventScheduleStaffMember eventScheduleStaffMember)
	{
		Guid staffMemberId = eventScheduleStaffMember.StaffMemberId;
		Guid companyId = eventScheduleStaffMember.CompanyId;

		StaffMember? eventType = await _staffMemberRepository
			.GetByIdAsync(staffMemberId, companyId);
		if (eventType == null)
			throw new InvalidOperationException(
				$"Staff member {staffMemberId} not found");
	}
}