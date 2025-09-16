using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Services;

public interface IEventTypeService
{
	Task<List<EventType>> GetAllAsync(Guid companyId);

	Task<EventType?> GetByIdAsync(
		Guid id,
		Guid companyId);

	Task<Guid> CreateAsync(EventType eventType);
	Task UpdateAsync(EventType eventType);

	Task DeleteAsync(
		Guid id,
		Guid companyId);
}