using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class StaffSpecializationResponse
{
	[Required] public Guid SpecializationId { get; set; }

	public StaffSpecializationResponse(Guid specializationId)
	{
		SpecializationId = specializationId;
	}
}