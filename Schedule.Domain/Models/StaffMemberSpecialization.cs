namespace Schedule.Domain.Models;

public class StaffMemberSpecialization
{
	public Guid Id { get; }
	public Guid CompanyId { get; }
	public Guid StaffMemberId { get; }
	public Guid SpecializationId { get; }

	public StaffMemberSpecialization(
		Guid id,
		Guid companyId,
		Guid staffMemberId,
		Guid specializationId)
	{
		Id = id;
		CompanyId = companyId;
		StaffMemberId = staffMemberId;
		SpecializationId = specializationId;
	}
}