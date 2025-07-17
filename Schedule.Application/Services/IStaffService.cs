using Schedule.Contracts.Dtos;

namespace Schedule.Application.Services;

public interface IStaffService
{
    IEnumerable<StaffProfileDto> GetAll();
    StaffProfileDto? GetById(Guid id);
    void Create(StaffProfileDto profile);
    void Update(StaffProfileDto profile);
    void Delete(Guid id);
}
