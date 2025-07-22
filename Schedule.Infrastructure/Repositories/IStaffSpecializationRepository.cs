using Schedule.Contracts.Dtos;

namespace Schedule.Infrastructure.Repositories;

public interface IStaffSpecializationRepository
{
    void Create(StaffSpecializationDto dto);
    IEnumerable<StaffSpecializationDto> GetByStaffId(Guid staffId);
    void Delete(Guid id);
}
