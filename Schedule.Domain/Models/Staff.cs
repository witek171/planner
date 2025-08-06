using Schedule.Domain.Models.Enums;

namespace Schedule.Domain.Models;

public class Staff
{
	public Guid Id { get; }
	public Guid CompanyId { get; private set; }
	public StaffRole Role { get; }
	public string Email { get; }
	public string Password { get; }
	public string FirstName { get; }
	public string LastName { get; }
	public string Phone { get; }
	public DateTime CreatedAt { get; }

	public Staff(
		Guid id,
		Guid companyId,
		StaffRole role,
		string email,
		string password,
		string firstName,
		string lastName,
		string phone,
		DateTime createdAt)
	{
		Id = id;
		CompanyId = companyId;
		Role = role;
		Email = email;
		Password = password;
		FirstName = firstName;
		LastName = lastName;
		Phone = phone;
		CreatedAt = createdAt;
	}

	public void SetCompanyId(Guid companyId)
	{
		if (CompanyId != Guid.Empty)
			throw new InvalidOperationException(
				$"CompanyId is already set to {CompanyId} and cannot be changed");

		CompanyId = companyId;
	}
}