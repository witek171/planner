namespace Schedule.Domain.Models;

public class StaffMemberAvailability
{
	public Guid Id { get; }
	public Guid CompanyId { get; private set; }
	public Guid StaffMemberId { get; private set;}
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

	public void SetCompanyId(Guid companyId)
	{
		if (CompanyId != Guid.Empty)
			throw new InvalidOperationException(
				$"CompanyId is already set to {CompanyId} and cannot be changed");

		CompanyId = companyId;
	}
	
	public void SetStaffMemberId(Guid staffMemberId)
	{
		if (StaffMemberId != Guid.Empty)
			throw new InvalidOperationException(
				$"StaffMemberId is already set to {StaffMemberId} and cannot be changed");

		StaffMemberId = staffMemberId;
	}
}