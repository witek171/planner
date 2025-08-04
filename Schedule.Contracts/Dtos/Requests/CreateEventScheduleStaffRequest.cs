namespace Schedule.Contracts.Dtos.Requests;

public class CreateEventScheduleStaffRequest
{
	public Guid CompanyId { get; set; }
	public Guid EventScheduleId { get; set; }
	public Guid StaffId { get; set; }
}
