using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class ParticipantResponse
{
	[Required] public string Email { get; set; }
	[Required] public string FirstName { get; set; }
	[Required] public string LastName { get; set; }
	[Required] public string Phone { get; set; }
	[Required] public DateTime CreatedAt { get; set; }

	public ParticipantResponse(
		string email,
		string firstName,
		string lastName,
		string phone,
		DateTime createdAt
	)
	{
		Email = email;
		FirstName = firstName;
		LastName = lastName;
		Phone = phone;
		CreatedAt = createdAt;
	}
}