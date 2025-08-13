using Schedule.Domain.Models.Enums;

namespace Schedule.Domain.Models;

public class StaffMember
{
	public Guid Id { get; }
	public Guid CompanyId { get; private set; }
	public StaffRole Role { get; }
	public string Email { get; private set; }
	public string Password { get; private set; }
	public string FirstName { get; private set; }
	public string LastName { get; private set; }
	public string Phone { get; private set; }
	public DateTime CreatedAt { get; }
	public bool IsDeleted { get; private set; }
	public IReadOnlyList<Specialization> Specializations { get; private set; }

	public StaffMember()
	{
	}

	public StaffMember(
		Guid id,
		Guid companyId,
		StaffRole role,
		string email,
		string password,
		string firstName,
		string lastName,
		string phone,
		DateTime createdAt,
		bool isDeleted,
		List<Specialization> specializations)
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
		IsDeleted = isDeleted;
		Specializations = specializations;
	}

	public void SetCompanyId(Guid companyId)
	{
		if (CompanyId != Guid.Empty)
			throw new InvalidOperationException(
				$"CompanyId is already set to {CompanyId} and cannot be changed");

		CompanyId = companyId;
	}

	public void Normalize()
	{
		Email = Email.Trim().ToLowerInvariant();
		FirstName = FirstName.Trim();
		LastName = LastName.Trim();
		Phone = Phone.Trim();
	}

	public void SetSpecializations(List<Specialization> specializations)
	{
		if (Specializations.Any())
			throw new InvalidOperationException(
				$"List of specializations is already set" +
				$" and cannot be changed");

		Specializations = specializations;
	}

	public void SoftDelete()
	{
		if (IsDeleted)
			throw new InvalidOperationException(
				$"Staff member {Id} is already marked as deleted for company {CompanyId}");

		IsDeleted = true;
	}
}