using Schedule.Contracts.Dtos;

namespace Schedule.Application.Services;

public interface IStaffAvailabilityService
{
    IEnumerable<StaffAvailabilityDto> GetByStaffId(Guid staffId);
    void Create(StaffAvailabilityDto availability);
    void Update(StaffAvailabilityDto availability);
    void Delete(Guid id);
}
