using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class SpecializationResponse
{
	public SpecializationResponse(
		Guid id,
		string name,
		string description)
	{
		Id = id;
		Name = name;
		Description = description;
	}

	[Required] public Guid Id { get; }
	[Required] public string Name { get; }
	[Required] public string Description { get; }
}