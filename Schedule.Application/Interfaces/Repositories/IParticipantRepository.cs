using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface IParticipantRepository
{
	Task<Participant?> GetByEmailAsync(Guid receptionId, string email);
	Task<Guid> CreateAsync(Participant participant);
	Task<bool> EmailExistsAsync(Guid receptionId, string email);
}