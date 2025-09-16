using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class EventScheduleRequest
{
	[Required] public Guid EventTypeId { get; }
	[Required] public string PlaceName { get; }
	[Required] public DateTime StartTime { get; }

	public EventScheduleRequest(
		Guid eventTypeId,
		string placeName,
		DateTime startTime)
	{
		EventTypeId = eventTypeId;
		PlaceName = placeName;
		StartTime = startTime;
	}
}