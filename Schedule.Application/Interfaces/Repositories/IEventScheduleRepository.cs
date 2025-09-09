using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface IEventScheduleRepository
{
	Task<List<EventSchedule>> GetByStaffMemberIdAsync(
		Guid companyId,
		Guid staffMemberId);

	Task<List<EventSchedule>> GetAllAsync(Guid companyId);

	Task<EventSchedule?> GetByIdAsync(
		Guid id,
		Guid companyId);

	Task<Guid> CreateAsync(EventSchedule eventSchedule);
	Task<bool> UpdateAsync(EventSchedule eventSchedule);

	Task<bool> DeleteAsync(
		Guid id,
		Guid companyId);

	Task<bool> HasRelatedRecordsAsync(
		Guid id,
		Guid companyId);

	Task<bool> UpdateStatusAsync(EventSchedule eventSchedule);
}