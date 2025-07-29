namespace Schedule.Contracts.Dtos.StaffRelated.Staff.Responses;

public class StaffResponse
{
	public Guid Id { get; set; }
	public Guid ReceptionId { get; set; }
	public string Role { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string Phone { get; set; } = string.Empty;
}
