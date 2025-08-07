using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Services;

public interface IStaffMemberSpecializationService
{
	Task<List<StaffMemberSpecialization>> GetByStaffMemberIdAsync(Guid staffMemberId);
	Task<Guid> CreateAsync(StaffMemberSpecialization specialization);
	Task DeleteAsync(Guid id);
}
