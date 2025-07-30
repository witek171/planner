using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos;

public class ParticipantCreateRequest
{
	[Required] public Guid CompanyId { get; set; }

	[Required] [EmailAddress] public string Email { get; set; }

	[Required] [StringLength(40)] public string FirstName { get; set; }

	[Required] [StringLength(40)] public string LastName { get; set; }

	[Required] [Phone] public string Phone { get; set; }

	[Required] public bool GdprConsent { get; set; }

	public ParticipantCreateRequest(
		Guid companyId,
		string email,
		string firstName,
		string lastName,
		string phone,
		bool gdprConsent
	)
	{
		CompanyId = companyId;
		Email = email;
		FirstName = firstName;
		LastName = lastName;
		Phone = phone;
		GdprConsent = gdprConsent;
	}
}