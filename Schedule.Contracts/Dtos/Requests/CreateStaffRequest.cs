using System.ComponentModel.DataAnnotations;
using Schedule.Domain.Models.Enums;

namespace Schedule.Contracts.Dtos.Requests;

public class CreateStaffRequest
{
	[Required] public StaffRole Role { get; set; }
	[Required] [EmailAddress] public string Email { get; set; }
	[Required] public string Password { get; set; }
	[Required] [MaxLength(40)] public string FirstName { get; set; }
	[Required] [MaxLength(40)] public string LastName { get; set; }
	[Required] [Phone] public string Phone { get; set; }

	public CreateStaffRequest(
		StaffRole role,
		string email,
		string password,
		string firstName,
		string lastName,
		string phone)
	{
		Role = role;
		Email = email;
		Password = password;
		FirstName = firstName;
		LastName = lastName;
		Phone = phone;
	}
}