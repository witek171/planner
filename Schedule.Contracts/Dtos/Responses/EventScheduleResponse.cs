using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class EventScheduleResponse
{
	[Required] public Guid Id { get; }
	[Required] public EventTypeResponse EventType { get; }
	[Required] public string PlaceName { get; }
	[Required] public DateTime StartTime { get; }
	[Required] public DateTime CreatedAt { get; }
	[Required] public string Status { get; }

	public EventScheduleResponse(
		Guid id,
		EventTypeResponse eventType,
		string placeName,
		DateTime startTime,
		DateTime createdAt,
		string status)
	{
		Id = id;
		EventType = eventType;
		PlaceName = placeName;
		StartTime = startTime;
		CreatedAt = createdAt;
		Status = status;
	}
}