using Schedule.Domain.Models.Enums;

namespace Schedule.Domain.Models;

public class EventSchedule
{
	public Guid Id { get; }
	public Guid CompanyId { get; private set; }
	public Guid EventTypeId { get; private set; }
	public string PlaceName { get; private set; }
	public DateTime StartTime { get; }
	public DateTime CreatedAt { get; }
	public EventStatus Status { get; private set; }

	public EventSchedule(
		Guid id,
		Guid companyId,
		Guid eventTypeId,
		string placeName,
		DateTime startTime,
		DateTime createdAt,
		EventStatus status)
	{
		Id = id;
		CompanyId = companyId;
		EventTypeId = eventTypeId;
		PlaceName = placeName;
		StartTime = startTime;
		CreatedAt = createdAt;
		Status = status;
	}

	public void SetCompanyId(Guid companyId)
	{
		if (CompanyId != Guid.Empty)
			throw new InvalidOperationException(
				$"CompanyId is already set to {CompanyId} and cannot be changed");

		CompanyId = companyId;
	}

	public void SetEventTypeId(Guid eventTypeId)
	{
		if (EventTypeId != Guid.Empty)
			throw new InvalidOperationException(
				$"EventTypeId is already set to {EventTypeId} and cannot be changed");

		EventTypeId = eventTypeId;
	}

	public void Normalize()
		=> PlaceName = PlaceName.Trim();

	public void SoftDelete()
	{
		if (Status == EventStatus.Deleted)
			throw new InvalidOperationException(
				$"Event {Id} is already marked as deleted");

		Status = EventStatus.Deleted;
	}
}