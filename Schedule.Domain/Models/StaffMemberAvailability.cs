namespace Schedule.Domain.Models;

public class StaffMemberAvailability
{
	public Guid Id { get; }
	public Guid CompanyId { get; }
	public Guid StaffMemberId { get; }
	public DateOnly Date { get; }
	public DateTime StartTime { get; }
	public DateTime EndTime { get; }
	public bool IsAvailable { get; }

	public StaffMemberAvailability(
		Guid id,
		Guid companyId,
		Guid staffMemberId,
		DateOnly date,
		DateTime startTime,
		DateTime endTime,
		bool isAvailable)
	{
		Id = id;
		CompanyId = companyId;
		StaffMemberId = staffMemberId;
		Date = date;
		StartTime = startTime;
		EndTime = endTime;
		IsAvailable = isAvailable;
	}
}