namespace Schedule.Contracts.Dtos.StaffRelated.StaffAvailability.Requests;

public class UpdateStaffAvailabilityRequest
{
	public DateOnly Date { get; set; }
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public bool IsAvailable { get; set; }
}
