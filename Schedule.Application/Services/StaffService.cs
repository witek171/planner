using Schedule.Domain.Models.StaffRelated;
using Schedule.Infrastructure.Repositories;

namespace Schedule.Application.Services;

public class StaffService
{
	private readonly StaffRepository _repository;

	public StaffService(StaffRepository repository)
	{
		_repository = repository;
	}

	public List<Staff> GetAll() => _repository.GetAll();

	public Staff? GetById(Guid id) => _repository.GetById(id);

	public Guid Create(Staff staff) => _repository.Create(staff);

	public void Update(Staff staff) => _repository.Update(staff);

	public void Delete(Guid id) => _repository.Delete(id);
}
