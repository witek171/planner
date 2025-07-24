using Schedule.Application.Interfaces;
using Schedule.Contracts.Dtos;
using Schedule.Infrastructure.Repositories;

namespace Schedule.Application.Services;

public class StaffAvailabilityService : IStaffAvailabilityService
{
	private readonly IStaffAvailabilityRepository _repository;

	public StaffAvailabilityService(IStaffAvailabilityRepository repository)
	{
		_repository = repository;
	}

	public IEnumerable<StaffAvailabilityDto> GetByStaffId(Guid staffId)
	{
		return _repository.GetByStaffId(staffId);
	}

	public void Create(StaffAvailabilityDto availability)
	{
		if (availability.Id == Guid.Empty)
			availability.Id = Guid.NewGuid();

		_repository.Create(availability);
	}

	public void Update(StaffAvailabilityDto availability)
	{
		_repository.Update(availability);
	}

	public void Delete(Guid id)
	{
		_repository.Delete(id);
	}
}
