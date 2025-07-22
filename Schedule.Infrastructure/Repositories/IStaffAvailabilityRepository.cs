using Schedule.Contracts.Dtos;

namespace Schedule.Infrastructure.Repositories;

public interface IStaffAvailabilityRepository
{
    IEnumerable<StaffAvailabilityDto> GetByStaffId(Guid staffId);
    void Create(StaffAvailabilityDto availability);
    void Update(StaffAvailabilityDto availability);
    void Delete(Guid id);
}
