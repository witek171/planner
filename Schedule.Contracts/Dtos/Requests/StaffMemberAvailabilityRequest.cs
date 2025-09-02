using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class StaffMemberAvailabilityRequest
{
	public StaffMemberAvailabilityRequest(
		DateOnly date,
		DateTime startTime,
		DateTime endTime)
	{
		Date = date;
		StartTime = startTime;
		EndTime = endTime;
	}

	[Required] public DateOnly Date { get; }
	[Required] public DateTime StartTime { get; }
	[Required] public DateTime EndTime { get; }
}