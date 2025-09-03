using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models;

namespace Schedule.Infrastructure.Repositories;

public class EventTypeRepository : IEventTypeRepository
{
	public Task<List<EventType>> GetAllAsync(Guid companyId)
	{
		throw new NotImplementedException();
	}

	public Task<EventType?> GetByIdAsync(
		Guid id,
		Guid companyId)
	{
		throw new NotImplementedException();
	}

	public Task<Guid> CreateAsync(EventType eventType)
	{
		throw new NotImplementedException();
	}

	public Task<bool> UpdateAsync(EventType eventType)
	{
		throw new NotImplementedException();
	}

	public Task<bool> DeleteAsync(
		Guid id,
		Guid companyId)
	{
		throw new NotImplementedException();
	}
}