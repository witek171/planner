using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class CreateStaffAvailabilityRequest
{
	[Required] public Guid StaffId { get; set; }
	[Required] public DateOnly Date { get; set; }
	[Required] public DateTime StartTime { get; set; }
	[Required] public DateTime EndTime { get; set; }
	[Required] public bool IsAvailable { get; set; }

	public CreateStaffAvailabilityRequest(
		Guid staffId,
		DateOnly date,
		DateTime startTime,
		DateTime endTime,
		bool isAvailable)
	{
		StaffId = staffId;
		Date = date;
		StartTime = startTime;
		EndTime = endTime;
		IsAvailable = isAvailable;
	}
}