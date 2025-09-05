using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface IEventTypeRepository
{
	Task<List<EventType>> GetAllAsync(Guid companyId);

	Task<EventType?> GetByIdAsync(
		Guid id,
		Guid companyId);

	Task<Guid> CreateAsync(EventType eventType);
	Task<bool> UpdateAsync(EventType eventType);

	Task<bool> DeleteAsync(
		Guid id,
		Guid companyId);

	Task<bool> HasRelatedRecordsAsync(
		Guid eventTypeId,
		Guid companyId);
}