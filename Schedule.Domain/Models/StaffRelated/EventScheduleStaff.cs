namespace Schedule.Domain.Models.StaffRelated;

public class EventScheduleStaff
{
	public Guid Id { get; set; }
	public Guid ReceptionId { get; set; }
	public Guid EventScheduleId { get; set; }
	public Guid StaffId { get; set; }
}
