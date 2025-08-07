using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class StaffMemberSpecializationResponse
{
	public StaffMemberSpecializationResponse(List<SpecializationResponse> specializationResponses)
	{
		SpecializationResponses = specializationResponses;
	}

	[Required] public List<SpecializationResponse> SpecializationResponses { get; }
}