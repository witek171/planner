using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models.StaffRelated;

namespace Schedule.Application.Services;

public class EventScheduleStaffService : IEventScheduleStaffService
{
	private readonly IEventScheduleStaffRepository _repository;

	public EventScheduleStaffService(IEventScheduleStaffRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<EventScheduleStaff>> GetByEventIdAsync(Guid eventId)
	{
		return await _repository.GetByEventScheduleIdAsync(eventId);
	}

	public async Task<Guid> CreateAsync(EventScheduleStaff entity)
	{
		return await _repository.CreateAsync(entity);
	}

	public async Task DeleteAsync(Guid id)
	{
		await _repository.DeleteByIdAsync(id);
	}
}

