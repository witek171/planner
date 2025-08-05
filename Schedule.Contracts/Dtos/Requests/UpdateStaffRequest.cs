using System.ComponentModel.DataAnnotations;
using Schedule.Domain.Models.Enums;

namespace Schedule.Contracts.Dtos.Requests;

public class UpdateStaffRequest
{
	[Required] [EmailAddress] public string Email { get; set; }
	[Required] [MaxLength(40)] public string FirstName { get; set; }
	[Required] [MaxLength(40)] public string LastName { get; set; }
	[Required] [Phone] public string Phone { get; set; }

	public UpdateStaffRequest(
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
}