using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class CreateStaffSpecializationRequest
{
	public CreateStaffSpecializationRequest(
		Guid staffId,
		Guid specializationId)
	{
		StaffId = staffId;
		SpecializationId = specializationId;
	}

	[Required] public Guid StaffId { get; }
	[Required] public Guid SpecializationId { get; }
}