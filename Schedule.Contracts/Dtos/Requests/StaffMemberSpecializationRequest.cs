using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class StaffMemberSpecializationRequest
{
	public StaffMemberSpecializationRequest(
		Guid staffMemberId,
		Guid specializationId)
	{
		StaffMemberId = staffMemberId;
		SpecializationId = specializationId;
	}

	[Required] public Guid StaffMemberId { get; }
	[Required] public Guid SpecializationId { get; }
}