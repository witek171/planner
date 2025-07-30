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
		// czy zgoda gdpr musi byc true aby utworzyc participant?
		if (!participant.GdprConsent)
			throw new InvalidOperationException("GDPR consent is required");
// musimy ustalic co dokladnie ma sie zrobic gdy participant
// poda te same dane w tej samej recepcji
		bool emailExists = await _repository.EmailExistsAsync(
			participant.Email, participant.CompanyId);
		bool phoneExists = await _repository.PhoneExistsAsync(
			participant.Phone, participant.CompanyId);
		if (phoneExists && emailExists)
		{
			Participant existingParticipant = (await _repository.GetByEmailAsync(
				participant.Email, participant.CompanyId))!;
			existingParticipant.FirstName = participant.FirstName.Trim();
			existingParticipant.LastName = participant.LastName.Trim();
			await _repository.PatchAsync(existingParticipant);
			return;
		}
		else if (emailExists)
		{
			throw new InvalidOperationException(
				$"Participant with email {participant.Email} already exists in reception");
		}
		else if (phoneExists)
		{
			throw new InvalidOperationException(
				$"Participant with phone {participant.Phone} already exists in reception");
		}

		participant.Email = participant.Email.Trim().ToLowerInvariant();
		participant.FirstName = participant.FirstName.Trim();
		participant.LastName = participant.LastName.Trim();
		participant.Phone = participant.Phone.Trim();

		await _repository.CreateAsync(participant);
	}

	public async Task<bool> PatchAsync(Participant participant)
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

		return await _repository.PatchAsync(participant);
	}

	public async Task<bool> DeleteByEmailAsync(
		string email,
		Guid companyId)
	{
		email = email.Trim().ToLowerInvariant();
		return await _repository.DeleteByEmailAsync(email, companyId);
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