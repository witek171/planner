namespace Schedule.Domain.Models;

public class StaffAvailability
{
	public Guid Id { get; }
	public Guid CompanyId { get; }
	public Guid StaffId { get; }
	public DateOnly Date { get; }
	public DateTime StartTime { get; }
	public DateTime EndTime { get; }
	public bool IsAvailable { get; }

	public StaffAvailability(
		Guid id,
		Guid companyId,
		Guid staffId,
		DateOnly date,
		DateTime startTime,
		DateTime endTime,
		bool isAvailable)
	{
		Id = id;
		CompanyId = companyId;
		StaffId = staffId;
		Date = date;
		StartTime = startTime;
		EndTime = endTime;
		IsAvailable = isAvailable;
	}
}