using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Services;

public interface IEventScheduleStaffService
{
	Task<List<EventScheduleStaff>> GetByEventIdAsync(Guid eventId);
	Task<Guid> CreateAsync(EventScheduleStaff entity);
	Task DeleteAsync(Guid id);
}
