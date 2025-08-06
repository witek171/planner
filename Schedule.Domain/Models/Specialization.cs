namespace Schedule.Domain.Models;

public class Specialization
{
	public Specialization(
		Guid id,
		Guid companyId,
		string name,
		string description)
	{
		Id = id;
		CompanyId = companyId;
		Name = name;
		Description = description;
	}

	public Guid Id { get; }
	public Guid CompanyId { get; }
	public string Name { get; }
	public string Description { get; }
}