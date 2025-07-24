using Schedule.Contracts.Dtos.Staff.Requests;
using Schedule.Contracts.Dtos.Staff.Responses;

namespace Schedule.Application.Interfaces;

public interface IStaffService
{
	Task<Guid> CreateAsync(CreateStaffRequest request);
	Task UpdateAsync(Guid id, UpdateStaffRequest request);
	Task DeleteAsync(Guid id);
	Task<StaffResponse> GetByIdAsync(Guid id);
	Task<List<StaffResponse>> GetAllAsync();
}
