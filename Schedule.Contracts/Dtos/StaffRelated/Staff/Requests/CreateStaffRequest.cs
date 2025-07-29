namespace Schedule.Contracts.Dtos.StaffRelated.Staff.Requests;

public class CreateStaffRequest
{
	public Guid ReceptionId { get; set; }
	public string Role { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string PasswordHash { get; set; } = string.Empty;
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string Phone { get; set; } = string.Empty;
}
