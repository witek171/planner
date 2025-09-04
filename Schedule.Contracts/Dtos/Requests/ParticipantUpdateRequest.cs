using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class ParticipantUpdateRequest
{
	public ParticipantUpdateRequest(
		string email,
		string firstName,
		string lastName,
		string phone)
	{
		Email = email;
		FirstName = firstName;
		LastName = lastName;
		Phone = phone;
	}

	[Required] [EmailAddress] public string Email { get; }
	[Required] [StringLength(40)] public string FirstName { get; }
	[Required] [StringLength(40)] public string LastName { get; }
	[Required] [Phone] public string Phone { get; }
}