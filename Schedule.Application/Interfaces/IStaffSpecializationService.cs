using Schedule.Contracts.Dtos;

namespace Schedule.Application.Interfaces;

public interface IStaffSpecializationService
{
    void Create(StaffSpecializationDto dto);
    IEnumerable<StaffSpecializationDto?> GetByStaffId(Guid id);
    void Delete(Guid id);
}
