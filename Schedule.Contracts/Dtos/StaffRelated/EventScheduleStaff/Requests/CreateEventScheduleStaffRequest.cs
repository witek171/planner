namespace Schedule.Contracts.Dtos.StaffRelated.EventScheduleStaff.Requests;

public class CreateEventScheduleStaffRequest
{
	public Guid ReceptionId { get; set; }
	public Guid EventScheduleId { get; set; }
	public Guid StaffId { get; set; }
}
