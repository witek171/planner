using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class ParticipantUpdateRequest
{
	[Required][EmailAddress] public string Email { get; }
	[Required][StringLength(40)] public string FirstName { get; }
	[Required][StringLength(40)] public string LastName { get; }
	[Required][Phone] public string Phone { get; }
}
