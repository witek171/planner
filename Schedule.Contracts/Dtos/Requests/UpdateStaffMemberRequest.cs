using System.ComponentModel.DataAnnotations;
using Schedule.Domain.Models.Enums;

namespace Schedule.Contracts.Dtos.Requests;

public class UpdateStaffMemberRequest
{
	public UpdateStaffMemberRequest(
		StaffRole role,
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

	[Required] public StaffRole Role { get; }
	[Required] [EmailAddress] public string Email { get; }
	[Required] [MaxLength(40)] public string FirstName { get; }
	[Required] [MaxLength(40)] public string LastName { get; }
	[Required] [Phone] public string Phone { get; }
}