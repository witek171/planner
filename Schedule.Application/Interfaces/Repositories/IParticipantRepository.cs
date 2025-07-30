using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface IParticipantRepository
{
	Task CreateAsync(Participant participant);
	Task<bool> PatchAsync(Participant participant);

	Task<bool> DeleteByEmailAsync(
		string email,
		Guid companyId
	);

	Task<Participant?> GetByIdAsync(
		Guid id,
		Guid companyId
	);

	Task<Participant?> GetByEmailAsync(
		string email,
		Guid companyId
	);

	Task<List<Participant>> GetAllAsync(Guid companyId);

	Task<bool> EmailExistsAsync(
		string email,
		Guid companyId
	);

	Task<bool> PhoneExistsAsync(
		string phone,
		Guid companyId
	);
}