using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class StaffMemberResponse
{
	public StaffMemberResponse(
		string role,
		string email,
		string firstName,
		string lastName,
		string phone)
	{
		Role = role;
		Email = email;
		FirstName = firstName;
		LastName = lastName;
		Phone = phone;
	}

	[Required] public string Role { get; }
	[Required] public string Email { get; }
	[Required] public string FirstName { get; }
	[Required] public string LastName { get; }
	[Required] public string Phone { get; }
}