using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class EventTypeResponse
{
	[Required] public Guid Id { get; }
	[Required] public string Name { get; }
	[Required] public string Description { get; }
	[Required] public int Duration { get; }
	[Required] public decimal Price { get; }
	[Required] public int MaxParticipants { get; }
	[Required] public int MinStaff { get; }

	public EventTypeResponse(
		Guid id,
		string name,
		string description,
		int duration,
		decimal price,
		int maxParticipants,
		int minStaff)
	{
		Id = id;
		Name = name;
		Description = description;
		Duration = duration;
		Price = price;
		MaxParticipants = maxParticipants;
		MinStaff = minStaff;
	}
}