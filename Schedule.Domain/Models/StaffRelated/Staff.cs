namespace Schedule.Domain.Models.StaffRelated;

public class Staff
{
	public Guid Id { get; set; }
	public Guid CompanyId { get; set; }
	public string Role { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string Password { get; set; } = null!;
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string Phone { get; set; } = null!;
	public DateTime CreatedAt { get; set; }
}
