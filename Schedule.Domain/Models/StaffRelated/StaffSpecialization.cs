namespace Schedule.Domain.Models.StaffRelated;

public class StaffSpecialization
{
	public Guid Id { get; set; }
	public Guid ReceptionId { get; set; }
	public Guid StaffId { get; set; }
	public Guid SpecializationId { get; set; }
}