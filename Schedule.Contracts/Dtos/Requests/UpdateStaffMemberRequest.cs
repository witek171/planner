using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class UpdateStaffMemberRequest
{
	public UpdateStaffMemberRequest(
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
	[Required] [MaxLength(40)] public string FirstName { get; }
	[Required] [MaxLength(40)] public string LastName { get; }
	[Required] [Phone] public string Phone { get; }
}