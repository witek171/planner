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
		if (!participant.GdprConsent)
			throw new InvalidOperationException("GDPR consent is required");

		NormalizeParticipantData(participant);

		bool emailExists = await _repository.EmailExistsAsync(
			participant.Email, participant.CompanyId);
		bool phoneExists = await _repository.PhoneExistsAsync(
			participant.Phone, participant.CompanyId);
		if (emailExists)
		{
			Participant existingParticipant = (await _repository.GetByEmailAsync(
				participant.Email, participant.CompanyId))!;

			existingParticipant.FirstName = participant.FirstName;
			existingParticipant.LastName = participant.LastName;
			existingParticipant.Phone = participant.Phone;
			await _repository.PatchAsync(existingParticipant);
			return;
		}
		else if (phoneExists)
		{
			// warning	
			// ($"Participant with phone {participant.Phone} already exists in reception");
			return;
		}

		await _repository.CreateAsync(participant);
	}

	public async Task PatchAsync(Participant participant)
	{
		// Validate unique phone (if changing)
		// if (!string.IsNullOrWhiteSpace(participant.Phone))
		// {
		// 	var existing = await _repository.GetByEmailAsync(participant.Email, participant.CompanyId);
		// 	if (existing != null && existing.Phone != participant.Phone)
		// 	{
		// 		bool phoneExists = await _repository.PhoneExistsAsync(participant.Phone, participant.CompanyId);
		// 		if (phoneExists)
		// 			throw new InvalidOperationException($"Participant with phone {participant.Phone} already exists");
		// 	}
		// }

		await _repository.PatchAsync(participant);
	}

	public async Task DeleteByIdAsync(
		Guid id,
		Guid companyId)
	{
		await _repository.DeleteByIdAsync(id, companyId);
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

	private void NormalizeParticipantData(Participant participant)
	{
		participant.Email = participant.Email.Trim().ToLowerInvariant();
		participant.FirstName = participant.FirstName.Trim();
		participant.LastName = participant.LastName.Trim();
		participant.Phone = participant.Phone.Trim();
	}
}