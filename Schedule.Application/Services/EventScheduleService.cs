using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models;

namespace Schedule.Application.Services;

public class EventScheduleService : IEventScheduleService
{
	private readonly IEventScheduleRepository _eventScheduleRepository;
	private readonly IEventScheduleStaffMemberRepository _eventScheduleStaffMemberRepository;
	private readonly IReservationRepository _reservationRepository;
	private readonly IEventTypeRepository _eventTypeRepository;

	public EventScheduleService(IEventScheduleRepository eventScheduleRepository,
		IEventScheduleStaffMemberRepository eventScheduleStaffMemberRepository,
		IReservationRepository reservationRepository,
		IEventTypeRepository eventTypeRepository)
	{
		_eventScheduleRepository = eventScheduleRepository;
		_eventScheduleStaffMemberRepository = eventScheduleStaffMemberRepository;
		_reservationRepository = reservationRepository;
		_eventTypeRepository = eventTypeRepository;
	}

	public async Task<List<EventSchedule>> GetByStaffMemberIdAsync(
		Guid companyId,
		Guid staffMemberId)
		=> await _eventScheduleRepository.GetByStaffMemberIdAsync(companyId, staffMemberId);

	public async Task<List<EventSchedule>> GetAllAsync(Guid companyId)
		=> await _eventScheduleRepository.GetAllAsync(companyId);

	public async Task<EventSchedule?> GetByIdAsync(
		Guid id,
		Guid companyId)
		=> await _eventScheduleRepository.GetByIdAsync(id, companyId);

	public async Task<Guid> CreateAsync(EventSchedule eventSchedule)
	{
		await ValidateEventTypeAsync(eventSchedule);
		eventSchedule.Normalize();
		// eventSchedule.SetAsActive(); obecnia baza danych domyslnie ustawia status na active
		return await _eventScheduleRepository.CreateAsync(eventSchedule);
	}

	public async Task UpdateAsync(EventSchedule eventSchedule)
	{
		await ValidateEventTypeAsync(eventSchedule);
		eventSchedule.Normalize();
		await _eventScheduleRepository.UpdateAsync(eventSchedule);
	}

	public async Task DeleteAsync(
		Guid id,
		Guid companyId)
	{
		if (await _eventScheduleRepository.HasRelatedRecordsAsync(id, companyId))
		{
			List<Guid> ids = await _eventScheduleStaffMemberRepository
				.GetEventScheduleStaffIdsByEventScheduleIdAsync(id, companyId);
			foreach (Guid eventScheduleStaffId in ids)
			{
				await _eventScheduleStaffMemberRepository
					.DeleteByIdAsync(eventScheduleStaffId, companyId);
				// wyslanie powiadomienia o odwolaniu zajec do pracownika
			}

			// List<Guid> reservationIds = await _reservationRepository
			// 	.GetByEventScheduleIdAsync(id, companyId);
			// foreach (Guid reservationId in reservationIds)
			// {
			// 	Reservation? reservation = await _reservationRepository
			// 		.GetByIdAsync(reservationId, companyId);
			// 	reservation?.SoftDelete();
			// 	// await _reservationRepository.UpdateStatusAsync(reservation);
			// 	// wyslanie powiadomienia o odwolaniu zajec do uczestnika
			// }

			EventSchedule eventSchedule = (await _eventScheduleRepository.GetByIdAsync(id, companyId))!;
			eventSchedule.SoftDelete();
			await _eventScheduleRepository.UpdateStatusAsync(eventSchedule);
		}
		else
			await _eventScheduleRepository.DeleteAsync(id, companyId);
	}

	private async Task ValidateEventTypeAsync(EventSchedule eventSchedule)
	{
		Guid eventTypeId = eventSchedule.EventTypeId;
		Guid companyId = eventSchedule.CompanyId;

		EventType? eventType = await _eventTypeRepository
			.GetByIdAsync(eventTypeId, companyId);
		if (eventType == null)
			throw new InvalidOperationException(
				$"Event type {eventTypeId} not found");
	}
}