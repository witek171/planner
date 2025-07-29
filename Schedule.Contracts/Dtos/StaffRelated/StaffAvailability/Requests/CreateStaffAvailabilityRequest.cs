namespace Schedule.Contracts.Dtos.StaffRelated.StaffAvailability.Requests;

public class CreateStaffAvailabilityRequest
{
	public Guid ReceptionId { get; set; }
	public Guid StaffId { get; set; }
	public DateOnly Date { get; set; }
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public bool IsAvailable { get; set; } = true;
}
