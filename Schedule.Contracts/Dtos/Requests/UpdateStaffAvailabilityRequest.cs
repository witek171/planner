using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class UpdateStaffAvailabilityRequest
{
	public UpdateStaffAvailabilityRequest(
		DateOnly date,
		DateTime startTime,
		DateTime endTime,
		bool isAvailable)
	{
		Date = date;
		StartTime = startTime;
		EndTime = endTime;
		IsAvailable = isAvailable;
	}

	[Required] public DateOnly Date { get; }
	[Required] public DateTime StartTime { get; }
	[Required] public DateTime EndTime { get; }
	[Required] public bool IsAvailable { get; }
}