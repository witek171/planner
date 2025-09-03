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

	public async Task<bool> UpdateAsync(EventType eventType)
	{
		eventType.Normalize();
		return await _eventTypeRepository.UpdateAsync(eventType);
	}

	public async Task<bool> DeleteAsync(
		Guid id,
		Guid companyId)
		=> await _eventTypeRepository.DeleteAsync(id, companyId);
}