using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class EventScheduleStaffResponse
{
	public EventScheduleStaffResponse(
		Guid eventScheduleId,
		Guid staffId)
	{
		EventScheduleId = eventScheduleId;
		StaffId = staffId;
	}

	[Required] public Guid EventScheduleId { get; }
	[Required] public Guid StaffId { get; }
}