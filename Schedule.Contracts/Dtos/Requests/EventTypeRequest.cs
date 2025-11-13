using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class EventTypeRequest
{
	[Required] public string Name { get; }
	[Required] public string Description { get; }
	[Required] public int Duration { get; }
	[Required] public decimal Price { get; }
	[Required] public int MaxParticipants { get; }
	[Required] public int MinStaff { get; }

	public EventTypeRequest(
		string name,
		string description,
		int duration,
		decimal price,
		int maxParticipants,
		int minStaff)
	{
		Name = name;
		Description = description;
		Duration = duration;
		Price = price;
		MaxParticipants = maxParticipants;
		MinStaff = minStaff;
	}
}