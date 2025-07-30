using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos;

public class ParticipantUpdateRequest
{
	[EmailAddress] public string? Email { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	[Phone] public string? Phone { get; set; }
	public bool? GdprConsent { get; set; }
}