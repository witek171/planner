using Schedule.Domain.Models.StaffRelated;

namespace Schedule.Application.Interfaces.Repositories;

public interface IStaffSpecializationRepository
{
	Task<List<StaffSpecialization>> GetByStaffIdAsync(Guid staffId);
	Task<Guid> CreateAsync(StaffSpecialization specialization);
	Task DeleteAsync(Guid id);
}
