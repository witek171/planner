namespace Schedule.Domain.Models;

public class StaffSpecialization
{
	public Guid Id { get; }
	public Guid CompanyId { get; }
	public Guid StaffId { get; }
	public Guid SpecializationId { get; }

	public StaffSpecialization(
		Guid id,
		Guid companyId,
		Guid staffId,
		Guid specializationId)
	{
		Id = id;
		CompanyId = companyId;
		StaffId = staffId;
		SpecializationId = specializationId;
	}
}