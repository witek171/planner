using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class EventScheduleResponse
{
	public EventScheduleResponse(
		Guid id,
		Guid eventTypeId,
		string placeName,
		DateTime startTime,
		DateTime createdAt,
		string status)
	{
		Id = id;
		EventTypeId = eventTypeId;
		PlaceName = placeName;
		StartTime = startTime;
		CreatedAt = createdAt;
		Status = status;
	}

	[Required] public Guid Id { get; }
	[Required] public Guid EventTypeId { get; }
	[Required] public string PlaceName { get; }
	[Required] public DateTime StartTime { get; }
	[Required] public DateTime CreatedAt { get; }
	[Required] public string Status { get; }
}