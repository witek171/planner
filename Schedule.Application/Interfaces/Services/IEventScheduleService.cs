using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Services;

public interface IEventScheduleService
{
	Task<List<EventSchedule>> GetByStaffMemberIdAsync(Guid companyId, Guid staffMemberId);
}
