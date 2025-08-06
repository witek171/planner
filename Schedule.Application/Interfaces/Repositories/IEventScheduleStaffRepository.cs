using Schedule.Domain.Models.StaffRelated;

namespace Schedule.Application.Interfaces.Repositories;

public interface IEventScheduleStaffRepository
{
	Task<Guid> CreateAsync(EventScheduleStaff eventScheduleStaff);

	Task<bool> DeleteByIdAsync(
		Guid eventScheduleStaffId,
		Guid companyId);

	Task<List<EventScheduleStaff>> GetByEventScheduleIdAsync(Guid eventId);
}