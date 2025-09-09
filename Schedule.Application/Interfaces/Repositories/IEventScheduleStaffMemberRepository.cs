using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface IEventScheduleStaffMemberRepository
{
	Task<Guid> CreateAsync(EventScheduleStaffMember eventScheduleStaffMember);

	Task<bool> DeleteByIdAsync(
		Guid eventScheduleStaffMemberId,
		Guid companyId);

	Task<List<EventScheduleStaffMember>> GetByStaffMemberIdAsync(
		Guid companyId,
		Guid staffMemberId);

	Task<List<Guid>> GetEventScheduleStaffIdsByEventScheduleIdAsync(
		Guid eventScheduleId,
		Guid companyId);

	Task<bool> ExistsByIdAsync(
		Guid companyId,
		Guid id);
}