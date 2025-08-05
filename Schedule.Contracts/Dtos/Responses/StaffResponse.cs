using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class StaffResponse
{
	[Required] public string Role { get; set; }
	[Required] public string Email { get; set; }
	[Required] public string FirstName { get; set; }
	[Required] public string LastName { get; set; }
	[Required] public string Phone { get; set; }

	public StaffResponse(
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
}