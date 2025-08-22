using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface IEventScheduleRepository
{
	Task<List<EventSchedule>> GetByStaffMemberIdAsync(Guid companyId, Guid staffMemberId);
}
