namespace Schedule.Contracts.Dtos.StaffRelated.Staff.Requests;

public class CreateStaffRequest
{
	public Guid CompanyId { get; set; }
	public string Role { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string Phone { get; set; } = string.Empty;
}
