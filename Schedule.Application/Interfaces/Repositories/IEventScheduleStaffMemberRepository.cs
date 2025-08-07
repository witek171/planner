using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface IEventScheduleStaffMemberRepository
{
	Task<Guid> CreateAsync(EventScheduleStaffMember eventScheduleStaffMember);

	Task<bool> DeleteByIdAsync(
		Guid eventScheduleStaffMemberId,
		Guid companyId);

	Task<List<EventScheduleStaffMember>> GetByEventScheduleIdAsync(Guid eventId);
}