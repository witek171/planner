using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface IEventScheduleStaffMemberRepository
{
	Task<Guid> CreateAsync(EventScheduleStaffMember eventScheduleStaffMember);

	Task<bool> DeleteByIdAsync(
		Guid companyId,
		Guid eventScheduleStaffMemberId);

	Task<List<EventScheduleStaffMember>> GetByStaffMemberIdAsync(
		Guid companyId,
		Guid staffMemberId);

	Task<bool> ExistsByIdAsync(
		Guid companyId,
		Guid id);
}