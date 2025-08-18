using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class CompanyResponse
{
	[Required] public string Name { get; }
	[Required] public string TaxCode { get; }
	[Required] public string Street { get; }
	[Required] public string City { get; }
	[Required] public string PostalCode { get; }
	[Required] public string Phone { get; }
	[Required] public string Email { get; }

	public CompanyResponse(
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