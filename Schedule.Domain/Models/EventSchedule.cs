namespace Schedule.Domain.Models;

public class EventSchedule
{
	public Guid Id { get; }
	public Guid CompanyId { get; }
	public Guid EventTypeId { get; }
	public string PlaceName { get; }
	public DateTime StartTime { get; }
	public DateTime CreatedAt { get; }
	public string Status { get; }

	public EventSchedule(
		Guid id,
		Guid companyId,
		Guid eventTypeId,
		string placeName,
		DateTime startTime,
		DateTime createdAt,
		string status)
	{
		Id = id;
		CompanyId = companyId;
		EventTypeId = eventTypeId;
		PlaceName = placeName;
		StartTime = startTime;
		CreatedAt = createdAt;
		Status = status;
	}
}