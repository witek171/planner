using Schedule.Contracts.Dtos;
using Schedule.Infrastructure.Repositories;

namespace Schedule.Application.Services;

public class StaffService : IStaffService
{
    private readonly IStaffProfileRepository _repository;

    public StaffService(IStaffProfileRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<StaffProfileDto> GetAll()
    {
        return _repository.GetAll();
    }

    public StaffProfileDto? GetById(Guid id)
    {
        return _repository.GetById(id);
    }

    public void Create(StaffProfileDto profile)
    {
        // Możesz tu dodać walidacje lub logikę biznesową
        _repository.Create(profile);
    }

    public void Update(StaffProfileDto profile)
    {
        // Tu też możesz dodać dodatkowe sprawdzenia itp.
        _repository.Update(profile);
    }

    public void Delete(Guid id)
    {
        _repository.Delete(id);
    }
}