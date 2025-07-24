namespace Schedule.Contracts.Dtos;

public class StaffDto
{
	public Guid Id { get; set; }
	public Guid ReceptionId { get; set; }
	public string Role { get; set; } = default!;
	public string Email { get; set; } = default!;
	public string PasswordHash { get; set; } = default!;
	public string FirstName { get; set; } = default!;
	public string LastName { get; set; } = default!;
	public string Phone { get; set; } = default!;
	public DateTime CreatedAt { get; set; }
}
