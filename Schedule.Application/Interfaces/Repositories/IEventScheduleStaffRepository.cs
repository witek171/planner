using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface IEventScheduleStaffRepository
{
	Task<Guid> CreateAsync(EventScheduleStaff eventScheduleStaff);

	Task<bool> DeleteByIdAsync(
		Guid eventScheduleStaffId,
		Guid companyId);

	Task<List<EventScheduleStaff>> GetByEventScheduleIdAsync(Guid eventId);
}