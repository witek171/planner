namespace Schedule.Domain.Models;

public class Participant
{
	public Guid Id { get; set; }
	public Guid ReceptionId { get; set; }
	public string Email { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Phone { get; set; }
	public bool GdprConsent { get; set; }
	public DateTime CreatedAt { get; set; }

	public Participant(
		Guid id,
		Guid receptionId,
		string email,
		string firstName,
		string lastName,
		string phone,
		bool gdprConsent,
		DateTime createdAt
	)
	{
		Id = id;
		ReceptionId = receptionId;
		Email = email;
		FirstName = firstName;
		LastName = lastName;
		Phone = phone;
		GdprConsent = gdprConsent;
		CreatedAt = createdAt;
	}
}