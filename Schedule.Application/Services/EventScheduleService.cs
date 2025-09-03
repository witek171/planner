using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models;
namespace Schedule.Application.Services;

public class EventScheduleService : IEventScheduleService
{
	private readonly IEventScheduleRepository _eventScheduleRepository;

	public EventScheduleService(IEventScheduleRepository eventScheduleRepository)
	{
		_eventScheduleRepository = eventScheduleRepository;
	}

	public async Task<List<EventSchedule>> GetByStaffMemberIdAsync(
		Guid companyId,
		Guid staffMemberId)
	{
		return await _eventScheduleRepository.GetByStaffMemberIdAsync(companyId, staffMemberId);
	}
}
