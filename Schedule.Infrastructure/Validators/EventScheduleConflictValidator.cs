using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Validators;
using Schedule.Domain.Models;

namespace Schedule.Infrastructure.Validators;

using Itenso.TimePeriod;

public class EventScheduleConflictValidator : IEventScheduleConflictValidator
{
	private readonly IEventScheduleRepository _eventScheduleRepository;
	private readonly IReservationRepository _reservationRepository;
	private readonly IStaffMemberAvailabilityRepository _staffMemberAvailabilityRepository;
	private readonly ICompanyConfigRepository _companyConfigRepository;

	public EventScheduleConflictValidator(
		IEventScheduleRepository eventScheduleRepository,
		IReservationRepository reservationRepository,
		IStaffMemberAvailabilityRepository staffMemberAvailabilityRepository,
		ICompanyConfigRepository companyConfigRepository)
	{
		_eventScheduleRepository = eventScheduleRepository;
		_reservationRepository = reservationRepository;
		_staffMemberAvailabilityRepository = staffMemberAvailabilityRepository;
		_companyConfigRepository = companyConfigRepository;
	}

	public async Task<bool> CanAssignStaffMemberAsync(
		Guid companyId,
		Guid staffMemberId,
		DateTime start,
		DateTime end)
	{
		TimeRange eventScheduleRange = new(start, end);
		List<StaffMemberAvailability> availabilities = await _staffMemberAvailabilityRepository
			.GetByStaffMemberIdAsync(companyId, staffMemberId);

		bool isAvailable = false;
		foreach (StaffMemberAvailability availability in availabilities)
		{
			DateTime startTime = availability.StartTime;
			DateTime endTime = availability.EndTime;
			TimeRange availabilityRange = new(startTime, endTime);
			if (availabilityRange.HasInside(eventScheduleRange))
			{
				isAvailable = true;
				break;
			}
		}

		if (!isAvailable)
			return false;

		CompanyConfig companyConfig = (await _companyConfigRepository.GetByIdAsync(companyId))!;
		int breakTime = companyConfig.BreakTimeStaff;

		TimeRange bufferedEventScheduleRange = new(start.AddMinutes(-breakTime), end.AddMinutes(breakTime));
		List<EventSchedule> eventSchedules = await _eventScheduleRepository
			.GetByStaffMemberIdAsync(companyId, staffMemberId);
		foreach (EventSchedule eventSchedule in eventSchedules)
		{
			DateTime startTime = eventSchedule.StartTime;
			DateTime endTime = eventSchedule.EndTime;
			TimeRange existingRange = new(startTime, endTime);
			if (bufferedEventScheduleRange.IntersectsWith(existingRange))
				return false;
		}

		return true;
	}

	public async Task<bool> CanAssignParticipantAsync(
		Guid companyId,
		Guid participantId,
		DateTime start,
		DateTime end)
	{
		CompanyConfig companyConfig = (await _companyConfigRepository.GetByIdAsync(companyId))!;
		int breakTime = companyConfig.BreakTimeParticipants;

		TimeRange bufferedEventScheduleRange = new(start.AddMinutes(-breakTime), end.AddMinutes(breakTime));
		List<Reservation> reservations = await _reservationRepository
			.GetByParticipantIdAsync(companyId, participantId);

		foreach (Reservation reservation in reservations)
		{
			DateTime startTime = reservation.EventSchedule.StartTime;
			DateTime endTime = reservation.EventSchedule.EndTime;
			TimeRange existingRange = new(startTime, endTime);
			if (bufferedEventScheduleRange.IntersectsWith(existingRange))
				return false;
		}

		return true;
	}
}