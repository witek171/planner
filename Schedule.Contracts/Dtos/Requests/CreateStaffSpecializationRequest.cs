using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class CreateStaffSpecializationRequest
{
	[Required] public Guid StaffId { get; set; }
	[Required] public Guid SpecializationId { get; set; }

	public CreateStaffSpecializationRequest(
		Guid staffId,
		Guid specializationId)
	{
		StaffId = staffId;
		SpecializationId = specializationId;
	}
}