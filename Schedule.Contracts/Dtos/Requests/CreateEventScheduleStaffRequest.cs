using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class CreateEventScheduleStaffRequest
{
	public CreateEventScheduleStaffRequest(
		Guid eventScheduleId,
		Guid staffId)
	{
		EventScheduleId = eventScheduleId;
		StaffId = staffId;
	}

	[Required] public Guid EventScheduleId { get; }
	[Required] public Guid StaffId { get; }
}