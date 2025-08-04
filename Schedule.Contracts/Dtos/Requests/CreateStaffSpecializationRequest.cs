namespace Schedule.Contracts.Dtos.Requests;

public class CreateStaffSpecializationRequest
{
	public Guid CompanyId { get; set; }
	public Guid StaffId { get; set; }
	public Guid SpecializationId { get; set; }
}
