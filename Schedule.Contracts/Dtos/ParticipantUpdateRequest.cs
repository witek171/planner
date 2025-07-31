using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos;

public class ParticipantUpdateRequest
{
	[EmailAddress] public string? Email { get; set; }
	[StringLength(40)] public string? FirstName { get; set; }
	[StringLength(40)] public string? LastName { get; set; }
	[Phone] public string? Phone { get; set; }
}