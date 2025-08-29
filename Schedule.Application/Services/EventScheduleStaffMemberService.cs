using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models;

namespace Schedule.Application.Services;

public class EventScheduleStaffMemberService : IEventScheduleStaffMemberService
{
	private readonly IEventScheduleStaffMemberRepository _eventScheduleStaffMemberRepository;

	public EventScheduleStaffMemberService(IEventScheduleStaffMemberRepository eventScheduleStaffMemberRepository)
	{
		_eventScheduleStaffMemberRepository = eventScheduleStaffMemberRepository;
	}

	public async Task<List<EventScheduleStaffMember>> GetByStaffMemberIdAsync(
		Guid companyId,
		Guid staffMemberId)
		=> await _eventScheduleStaffMemberRepository
			.GetByStaffMemberIdAsync(companyId, staffMemberId);

	public async Task<Guid> CreateAsync(EventScheduleStaffMember entity)
	{
		return await _eventScheduleStaffMemberRepository.CreateAsync(entity);
	}

	public async Task DeleteAsync(
		Guid companyId,
		Guid id)
	{
		await _eventScheduleStaffMemberRepository.DeleteByIdAsync(companyId, id);
	}
}