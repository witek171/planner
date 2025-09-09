using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models;

namespace Schedule.Application.Services;

public class EventTypeService : IEventTypeService
{
	private readonly IEventTypeRepository _eventTypeRepository;

	public EventTypeService(IEventTypeRepository eventTypeRepository)
	{
		_eventTypeRepository = eventTypeRepository;
	}

	public async Task<List<EventType>> GetAllAsync(Guid companyId)
		=> await _eventTypeRepository.GetAllAsync(companyId);

	public async Task<EventType?> GetByIdAsync(
		Guid id,
		Guid companyId)
		=> await _eventTypeRepository.GetByIdAsync(id, companyId);

	public async Task<Guid> CreateAsync(EventType eventType)
	{
		eventType.Normalize();
		return await _eventTypeRepository.CreateAsync(eventType);
	}

	public async Task UpdateAsync(EventType eventType)
	{
		eventType.Normalize();
		await _eventTypeRepository.UpdateAsync(eventType);
	}

	public async Task DeleteAsync(
		Guid id,
		Guid companyId)
	{
		if (await _eventTypeRepository.ExistsInNonDeletedEventSchedulesAsync(id, companyId))
			throw new InvalidOperationException(
				$"Event type {id} is used in event schedules and cannot be deleted");

		if (await _eventTypeRepository.ExistsOnlyInDeletedEventSchedulesAsync(id, companyId))
		{
			EventType eventType = (await _eventTypeRepository.GetByIdAsync(id, companyId))!;
			eventType.SoftDelete();
			await _eventTypeRepository.UpdateSoftDeleteAsync(eventType);
		}
		else
			await _eventTypeRepository.DeleteAsync(id, companyId);
	}
}