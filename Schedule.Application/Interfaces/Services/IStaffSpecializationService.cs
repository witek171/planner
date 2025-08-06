using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Services;

public interface IStaffSpecializationService
{
	Task<List<StaffSpecialization>> GetByStaffIdAsync(Guid staffId);
	Task<Guid> CreateAsync(StaffSpecialization specialization);
	Task DeleteAsync(Guid id);
}
