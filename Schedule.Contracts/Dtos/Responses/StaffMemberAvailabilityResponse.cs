using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class StaffMemberAvailabilityResponse
{
	public StaffMemberAvailabilityResponse(
		StaffMemberResponse staffMember,
		List<AvailabilityResponse> availabilities)
	{
		StaffMember = staffMember;
		Availabilities = availabilities;
	}

	[Required] public StaffMemberResponse StaffMember { get; }
	[Required] public List<AvailabilityResponse> Availabilities { get; }
}