namespace Schedule.Contracts.Dtos.Responses;

public class StaffAvailabilityResponse
{
	public Guid Id { get; set; }
	public Guid CompanyId { get; set; }
	public Guid StaffId { get; set; }
	public DateOnly Date { get; set; }
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public bool IsAvailable { get; set; }
}
