using Schedule.Contracts.Dtos;
using Schedule.Infrastructure.Repositories;

namespace Schedule.Application.Services;

public class StaffService : IStaffService
{
    private readonly IStaffRepository _repository;

    public StaffService(IStaffRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<StaffDto> GetAll() => _repository.GetAll();
    public StaffDto? GetById(Guid id) => _repository.GetById(id);
    public void Create(StaffDto staff) => _repository.Create(staff);
    public void Update(StaffDto staff) => _repository.Update(staff);
    public void Delete(Guid id) => _repository.Delete(id);
}