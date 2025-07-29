namespace Schedule.Contracts.Dtos.StaffRelated.EventScheduleStaff.Responses;

public class EventScheduleStaffResponse
{
	public Guid Id { get; set; }
	public Guid ReceptionId { get; set; }
	public Guid EventScheduleId { get; set; }
	public Guid StaffId { get; set; }
}
