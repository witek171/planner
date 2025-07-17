using Schedule.Contracts.Dtos;

namespace Schedule.Infrastructure.Repositories;

public interface IStaffProfileRepository
{
    IEnumerable<StaffProfileDto> GetAll();
    StaffProfileDto? GetById(Guid id);
    void Create(StaffProfileDto profile);
    void Update(StaffProfileDto profile);
    void Delete(Guid id);
}