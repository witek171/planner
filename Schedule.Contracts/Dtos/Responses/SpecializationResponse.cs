using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class SpecializationResponse
{
	public SpecializationResponse(
		string name,
		string description)
	{
		Name = name;
		Description = description;
	}

	[Required] public string Name { get; }
	[Required] public string Description { get; }
}