using Schedule.Domain.Models.StaffRelated;

namespace Schedule.Application.Interfaces.Repositories;

public interface IEventScheduleStaffRepository
{
	Task<List<EventScheduleStaff>> GetByEventIdAsync(Guid eventId);
	Task<Guid> CreateAsync(EventScheduleStaff entity);
	Task DeleteAsync(Guid id);
}
