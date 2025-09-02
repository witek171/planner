using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class SpecializationRequest
{
	public SpecializationRequest(string name, string description)
	{
		Name = name;
		Description = description;
	}

	[Required] public string Name { get; }
	[Required] public string Description { get; }
}
