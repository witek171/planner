using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Services;

public interface IParticipantService
{
	Task CreateAsync(Participant participant);
}