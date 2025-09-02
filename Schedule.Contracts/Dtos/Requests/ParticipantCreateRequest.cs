using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class ParticipantCreateRequest
{
	public ParticipantCreateRequest(
		string email,
		string firstName,
		string lastName,
		string phone,
		bool gdprConsent
	)
	{
		Email = email;
		FirstName = firstName;
		LastName = lastName;
		Phone = phone;
		GdprConsent = gdprConsent;
	}

	[Required][EmailAddress] public string Email { get; }

	[Required][StringLength(40)] public string FirstName { get; }

	[Required][StringLength(40)] public string LastName { get; }

	[Required][Phone] public string Phone { get; }

	[Required] public bool GdprConsent { get; }
}
