using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class CreateStaffAvailabilityRequest
{
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

	[Required] public Guid StaffId { get; }
	[Required] public DateOnly Date { get; }
	[Required] public DateTime StartTime { get; }
	[Required] public DateTime EndTime { get; }
	[Required] public bool IsAvailable { get; }
}