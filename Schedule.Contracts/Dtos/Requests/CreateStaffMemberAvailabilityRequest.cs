using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class CreateStaffMemberAvailabilityRequest
{
	public CreateStaffMemberAvailabilityRequest(
		Guid staffMemberId,
		DateOnly date,
		DateTime startTime,
		DateTime endTime,
		bool isAvailable)
	{
		StaffMemberId = staffMemberId;
		Date = date;
		StartTime = startTime;
		EndTime = endTime;
		IsAvailable = isAvailable;
	}

	[Required] public Guid StaffMemberId { get; }
	[Required] public DateOnly Date { get; }
	[Required] public DateTime StartTime { get; }
	[Required] public DateTime EndTime { get; }
	[Required] public bool IsAvailable { get; }
}