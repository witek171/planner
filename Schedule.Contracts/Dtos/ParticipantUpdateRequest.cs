using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos;

public class ParticipantUpdateRequest
{
	[Required] [EmailAddress] public string Email { get; set; }
	[Required] [StringLength(40)] public string FirstName { get; set; }
	[Required] [StringLength(40)] public string LastName { get; set; }
	[Required] [Phone] public string Phone { get; set; }
}