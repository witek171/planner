using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models;

namespace Schedule.Application.Services;

public class ParticipantService : IParticipantService
{
	private readonly IParticipantRepository _repository;

	public ParticipantService(IParticipantRepository repository)
	{
		_repository = repository;
	}

	public async Task<Guid> CreateAsync(Participant participant)
	{
		if (!participant.GdprConsent)
			throw new InvalidOperationException("GDPR consent is required");

		participant.Normalize();
		return await _repository.CreateAsync(participant);
	}

	public async Task PutAsync(Participant participant)
	{
		participant.Normalize();
		await _repository.PutAsync(participant);
	}

	public async Task DeleteByIdAsync(
		Guid participantId,
		Guid companyId)
	{
		if (
			await _repository.IsParticipantAssignedToReservationsAsync(
				participantId, companyId)
		)
		{
			Participant participant = (await _repository.GetByIdAsync(
				participantId, companyId))!;

			participant.Anonymize();
			await _repository.PutAsync(participant);
		}
		else
		{
			await _repository.DeleteByIdAsync(participantId, companyId);
		}
	}

	public async Task<Participant?> GetByIdAsync(
		Guid participantId,
		Guid companyId)
	{
		return await _repository.GetByIdAsync(participantId, companyId);
	}

	public async Task<Participant?> GetByEmailAsync(
		string email,
		Guid companyId)
	{
		email = email.Trim().ToLowerInvariant();
		return await _repository.GetByEmailAsync(email, companyId);
	}

	public async Task<List<Participant>> GetAllAsync(Guid companyId)
	{
		return await _repository.GetAllAsync(companyId);
	}
}