using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Services;

public interface IEventScheduleStaffMemberService
{
	Task<List<EventScheduleStaffMember>> GetByEventIdAsync(Guid eventId);
	Task<Guid> CreateAsync(EventScheduleStaffMember entity);
	Task DeleteAsync(
		Guid companyId,
		Guid id);
}
