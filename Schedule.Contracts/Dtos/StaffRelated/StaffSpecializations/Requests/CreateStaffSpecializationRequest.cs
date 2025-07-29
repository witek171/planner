namespace Schedule.Contracts.Dtos.StaffRelated.StaffSpecializations.Requests;

public class CreateStaffSpecializationRequest
{
	public Guid ReceptionId { get; set; }
	public Guid StaffId { get; set; }
	public Guid SpecializationId { get; set; }
}
