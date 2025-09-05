using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models;

namespace Schedule.Application.Services;

public class EventScheduleService : IEventScheduleService
{
	private readonly IEventScheduleRepository _eventScheduleRepository;

	public EventScheduleService(IEventScheduleRepository eventScheduleRepository)
	{
		_eventScheduleRepository = eventScheduleRepository;
	}

	public async Task<List<EventSchedule>> GetByStaffMemberIdAsync(
		Guid companyId,
		Guid staffMemberId)
	{
		return await _eventScheduleRepository.GetByStaffMemberIdAsync(companyId, staffMemberId);
	}

	public async Task<List<EventSchedule>> GetAllAsync(Guid companyId)
		=> await _eventScheduleRepository.GetAllAsync(companyId);

	public async Task<EventSchedule?> GetByIdAsync(
		Guid id,
		Guid companyId)
		=> await _eventScheduleRepository.GetByIdAsync(id, companyId);

	public async Task<Guid> CreateAsync(EventSchedule eventSchedule)
	{
		eventSchedule.Normalize();
		return await _eventScheduleRepository.CreateAsync(eventSchedule);
	}

	public async Task UpdateAsync(EventSchedule eventSchedule)
	{
		eventSchedule.Normalize();
		await _eventScheduleRepository.UpdateAsync(eventSchedule);
	}

	public async Task DeleteAsync(
		Guid id,
		Guid companyId)
	{
		if (await _eventScheduleRepository.HasRelatedRecordsAsync(id, companyId))
		{
			EventSchedule? eventSchedule = await _eventScheduleRepository.GetByIdAsync(id, companyId);
			if (eventSchedule == null)
				throw new InvalidOperationException(
					$"EventSchedule {id} is already marked as deleted");

			eventSchedule.SoftDelete();
			await _eventScheduleRepository.UpdateStatusAsync(eventSchedule);
		}
		else
			await _eventScheduleRepository.DeleteAsync(id, companyId);
	}
}