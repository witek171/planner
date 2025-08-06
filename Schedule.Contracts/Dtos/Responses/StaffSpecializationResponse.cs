using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class StaffSpecializationResponse
{
	public StaffSpecializationResponse(Guid specializationId)
	{
		SpecializationId = specializationId;
	}

	[Required] public Guid SpecializationId { get; }
}