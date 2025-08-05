using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class EventScheduleStaffResponse
{
	[Required] public Guid EventScheduleId { get; set; }
	[Required] public Guid StaffId { get; set; }

	public EventScheduleStaffResponse(
		Guid eventScheduleId,
		Guid staffId)
	{
		EventScheduleId = eventScheduleId;
		StaffId = staffId;
	}
}