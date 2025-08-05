using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class CreateEventScheduleStaffRequest
{
	[Required] public Guid EventScheduleId { get; set; }
	[Required] public Guid StaffId { get; set; }

	public CreateEventScheduleStaffRequest(
		Guid eventScheduleId,
		Guid staffId)
	{
		EventScheduleId = eventScheduleId;
		StaffId = staffId;
	}
}