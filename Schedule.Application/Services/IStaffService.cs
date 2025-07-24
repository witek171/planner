using Schedule.Contracts.Dtos;
using Schedule.Contracts.Dtos.Staff.Requests;
using Schedule.Contracts.Dtos.Staff.Responses;

namespace Schedule.Application.Services;

public interface IStaffService
{
    Task<List<StaffResponse>> GetAllAsync();
    Task<StaffResponse?> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(CreateStaffRequest request);
    Task UpdateAsync(Guid id, UpdateStaffRequest request);
    Task DeleteAsync(Guid id);
}
