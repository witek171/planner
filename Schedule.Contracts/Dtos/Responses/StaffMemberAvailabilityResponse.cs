using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Schedule.Contracts.Dtos.Responses;

public class StaffMemberAvailabilityResponse
{
	public StaffMemberAvailabilityResponse(
		DateOnly date,
		DateTime startTime,
		DateTime endTime, StaffMemberResponse staffMember)
	{
		Date = date;
		StartTime = startTime;
		EndTime = endTime;
		StaffMember = staffMember;
	}

	public StaffMemberAvailabilityResponse()
	{
	}

	[Required] public DateOnly Date { get; }
	[Required] public DateTime StartTime { get; }
	[Required] public DateTime EndTime { get; }

	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public StaffMemberResponse? StaffMember { get; }
}