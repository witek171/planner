namespace Schedule.Contracts.Dtos.Requests;

public class UpdateStaffRequest
{
	public string Role { get; set; } = string.Empty;
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string Phone { get; set; } = string.Empty;
}
