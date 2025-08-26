using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class EventScheduleStaffMemberResponse
{
	public EventScheduleStaffMemberResponse(
		Guid eventScheduleId,
		Guid staffMemberId)
	{
		EventScheduleId = eventScheduleId;
		StaffMemberId = staffMemberId;
	}

	[Required] public Guid EventScheduleId { get; }
	[Required] public Guid StaffMemberId { get; }
}