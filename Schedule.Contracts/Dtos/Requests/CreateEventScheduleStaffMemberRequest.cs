using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class CreateEventScheduleStaffMemberRequest
{
	public CreateEventScheduleStaffMemberRequest(
		Guid eventScheduleId,
		Guid staffMemberId)
	{
		EventScheduleId = eventScheduleId;
		StaffMemberId = staffMemberId;
	}

	[Required] public Guid EventScheduleId { get; }
	[Required] public Guid StaffMemberId { get; }
}