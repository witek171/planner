using Schedule.Contracts.Dtos;

namespace Schedule.Application.Services;

public interface IStaffService
{
    Task<IEnumerable<StaffProfileDto>> GetAllAsync();
    Task<StaffProfileDto?> GetByIdAsync(Guid id);
    Task CreateAsync(StaffProfileDto dto);
    Task UpdateAsync(StaffProfileDto dto);
    Task DeleteAsync(Guid id);
}
