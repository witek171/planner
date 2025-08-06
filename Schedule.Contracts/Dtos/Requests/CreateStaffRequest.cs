using System.ComponentModel.DataAnnotations;
using Schedule.Domain.Models.Enums;

namespace Schedule.Contracts.Dtos.Requests;

public class CreateStaffRequest
{
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

	[Required] public StaffRole Role { get; }
	[Required] [EmailAddress] public string Email { get; }
	[Required] public string Password { get; }
	[Required] [MaxLength(40)] public string FirstName { get; }
	[Required] [MaxLength(40)] public string LastName { get; }
	[Required] [Phone] public string Phone { get; }
}