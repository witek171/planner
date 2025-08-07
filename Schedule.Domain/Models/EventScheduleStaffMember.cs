namespace Schedule.Domain.Models;

public class EventScheduleStaffMember
{
	public Guid Id { get; }
	public Guid CompanyId { get; }
	public Guid EventScheduleId { get; }
	public Guid StaffMemberId { get; }

	public EventScheduleStaffMember(
		Guid id,
		Guid companyId,
		Guid eventScheduleId,
		Guid staffMemberId)
	{
		Id = id;
		CompanyId = companyId;
		EventScheduleId = eventScheduleId;
		StaffMemberId = staffMemberId;
	}
}