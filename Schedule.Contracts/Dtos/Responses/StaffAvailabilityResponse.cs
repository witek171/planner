using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class StaffAvailabilityResponse
{
	[Required] public DateOnly Date { get; set; }
	[Required] public DateTime StartTime { get; set; }
	[Required] public DateTime EndTime { get; set; }
	[Required] public bool IsAvailable { get; set; }

	public StaffAvailabilityResponse(
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
}