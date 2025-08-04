namespace Schedule.Contracts.Dtos.StaffRelated.StaffSpecializations.Responses;

public class StaffSpecializationResponse
{
	public Guid Id { get; set; }
	public Guid CompanyId { get; set; }
	public Guid StaffId { get; set; }
	public Guid SpecializationId { get; set; }
}
