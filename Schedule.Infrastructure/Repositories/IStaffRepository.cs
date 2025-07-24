using Schedule.Contracts.Dtos;
using Schedule.Domain.Models;

namespace Schedule.Infrastructure.Repositories;

public interface IStaffRepository
{
	Task<Guid> CreateAsync(Staff staff);
	Task UpdateAsync(Staff staff);
	Task DeleteAsync(Guid id);
	Task<Staff?> GetByIdAsync(Guid id);
	Task<List<Staff>> GetAllAsync();
}