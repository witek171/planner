namespace Schedule.Domain.Models.StaffRelated;

public class EventScheduleStaff
{
	public Guid Id { get; }
	public Guid CompanyId { get; }
	public Guid EventScheduleId { get; }
	public Guid StaffId { get; }

	public EventScheduleStaff(
		Guid id,
		Guid companyId,
		Guid eventScheduleId,
		Guid staffId)
	{
		Id = id;
		CompanyId = companyId;
		EventScheduleId = eventScheduleId;
		StaffId = staffId;
	}
}