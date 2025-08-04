namespace Schedule.Contracts.Dtos.Responses;

public class EventScheduleStaffResponse
{
	public Guid Id { get; set; }
	public Guid CompanyId { get; set; }
	public Guid EventScheduleId { get; set; }
	public Guid StaffId { get; set; }
}
