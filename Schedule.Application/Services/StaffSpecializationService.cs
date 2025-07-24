using Schedule.Application.Interfaces;
using Schedule.Contracts.Dtos;
using Schedule.Infrastructure.Repositories;

namespace Schedule.Application.Services;

public class StaffSpecializationService : IStaffSpecializationService
{
	private readonly IStaffSpecializationRepository _repository;

	public StaffSpecializationService(IStaffSpecializationRepository repository)
	{
		_repository = repository;
	}

	public void Create(StaffSpecializationDto dto)
	{
		if (dto.Id == Guid.Empty)
			dto.Id = Guid.NewGuid();

		_repository.Create(dto);
	}

	public IEnumerable<StaffSpecializationDto> GetByStaffId(Guid staffId)
	{
		return _repository.GetByStaffId(staffId);
	}

	public void Delete(Guid id)
	{
		_repository.Delete(id);
	}
}
