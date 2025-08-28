using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models;
namespace Schedule.Application.Services;

public class EventScheduleService : IEventScheduleService
{
	private readonly IEventScheduleRepository _repository;

	public EventScheduleService(IEventScheduleRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<EventSchedule>> GetByStaffMemberIdAsync(Guid companyId, Guid staffMemberId)
	{
		return await _repository.GetByStaffMemberIdAsync(companyId, staffMemberId);
	}
}
