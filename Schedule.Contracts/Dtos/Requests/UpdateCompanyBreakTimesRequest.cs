using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class UpdateCompanyBreakTimesRequest
{
	[Required] public int BreakTimeStaff { get; }
	[Required] public int BreakTimeParticipants { get; }

	public UpdateCompanyBreakTimesRequest(
		int breakTimeStaff,
		int breakTimeParticipants)
	{
		BreakTimeStaff = breakTimeStaff;
		BreakTimeParticipants = breakTimeParticipants;
	}
}