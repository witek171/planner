using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class CompanyConfigResponse
{
	[Required] public int BreakTimeStaff { get; private set; }
	[Required] public int BreakTimeParticipants { get; private set; }
}