using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models;

namespace Schedule.Application.Services;

public class EventScheduleStaffMemberService : IEventScheduleStaffMemberService
{
	private readonly IEventScheduleStaffMemberRepository _repository;

	public EventScheduleStaffMemberService(IEventScheduleStaffMemberRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<EventScheduleStaffMember>> GetByEventIdAsync(Guid eventId)
	{
		return await _repository.GetByEventScheduleIdAsync(eventId);
	}

	public async Task<Guid> CreateAsync(EventScheduleStaffMember entity)
	{
		return await _repository.CreateAsync(entity);
	}

	public async Task DeleteAsync(
		Guid companyId,
		Guid id)
	{
		await _repository.DeleteByIdAsync(companyId, id);
	}
}