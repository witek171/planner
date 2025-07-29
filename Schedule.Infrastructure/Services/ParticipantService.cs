using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models;

namespace Schedule.Infrastructure.Services;

public class ParticipantService : IParticipantService
{
	private readonly IParticipantRepository _repository;

	public ParticipantService(IParticipantRepository repository)
	{
		_repository = repository;
	}

	public async Task CreateAsync(Participant participant)
	{
		bool emailExists = await _repository.EmailExistsAsync(participant.CompanyId, participant.Email);
		if (emailExists)
			throw new InvalidOperationException(
				$"Participant with email {participant.Email} " +
				$"already exists in reception {participant.CompanyId}");

		if (!participant.GdprConsent)
			throw new InvalidOperationException("GDPR consent is required");

		await _repository.CreateAsync(participant);
	}
}