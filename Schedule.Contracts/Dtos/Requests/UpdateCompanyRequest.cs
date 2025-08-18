using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class UpdateCompanyRequest
{
	[Required] public string Name { get; }
	[Required] public string TaxCode { get; }
	[Required] public string Street { get; }
	[Required] public string City { get; }
	[Required] public string PostalCode { get; }
	[Required] [Phone] public string Phone { get; }
	[Required] [EmailAddress] public string Email { get; }

	public UpdateCompanyRequest(
		string name,
		string taxCode,
		string street,
		string city,
		string postalCode,
		string phone,
		string email)
	{
		Name = name;
		TaxCode = taxCode;
		Street = street;
		City = city;
		PostalCode = postalCode;
		Phone = phone;
		Email = email;
	}
}