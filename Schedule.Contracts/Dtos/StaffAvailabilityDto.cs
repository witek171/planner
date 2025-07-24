namespace Schedule.Contracts.Dtos;

public class StaffAvailabilityDto
{
	public Guid Id { get; set; }
	public Guid ReceptionId { get; set; }
	public Guid StaffId { get; set; }
	public DateTime Date { get; set; }
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public bool IsAvailable { get; set; }
}
