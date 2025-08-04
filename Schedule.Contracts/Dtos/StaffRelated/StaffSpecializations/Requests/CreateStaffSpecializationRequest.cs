namespace Schedule.Contracts.Dtos.StaffRelated.StaffSpecializations.Requests;

public class CreateStaffSpecializationRequest
{
	public Guid CompanyId { get; set; }
	public Guid StaffId { get; set; }
	public Guid SpecializationId { get; set; }
}
