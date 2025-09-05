namespace Schedule.Domain.Models;

public class EventType
{
	public Guid Id { get; }
	public Guid CompanyId { get; private set; }
	public string Name { get; private set; }
	public string Description { get; private set; }
	public int Duration { get; }
	public decimal Price { get; }
	public int MaxParticipants { get; private set; }
	public int MinStaff { get; private set; }

	public EventType(
		Guid id,
		Guid companyId,
		string name,
		string description,
		int duration,
		decimal price,
		int maxParticipants,
		int minStaff)
	{
		Id = id;
		CompanyId = companyId;
		Name = name;
		Description = description;
		Duration = duration;
		Price = price;
		MaxParticipants = maxParticipants;
		MinStaff = minStaff;
	}

	public EventType()
	{
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
		Name = Name.Trim();
		Description = Description.Trim();
	}
}