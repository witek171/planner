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

	public async Task CreateAsync(Participant participant)
	{
		if (!participant.GdprConsent)
			throw new InvalidOperationException("GDPR consent is required");

		NormalizeParticipantData(participant);

		bool emailExists = await _repository.EmailExistsAsync(
			participant.Email, participant.CompanyId);
		bool phoneExists = await _repository.PhoneExistsAsync(
			participant.Phone, participant.CompanyId);
		if (phoneExists)
		{
			// warning	
			// ($"Participant with phone {participant.Phone} already exists in reception");
			return;
		}

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

		await _repository.CreateAsync(participant);
	}

	public async Task PatchAsync(Participant participant)
	{
		NormalizeParticipantData(participant);

		bool phoneExistsExcludedParticipant = await _repository
			.PhoneExistsExcludedParticipantAsync(
			participant.Phone, participant.CompanyId, participant.Id);
		bool emailExistsExcludedParticipant = await _repository
			.EmailExistsExcludedParticipantAsync(
			participant.Email, participant.CompanyId, participant.Id);
		
		if (phoneExistsExcludedParticipant)
			// warning ($"Participant with phone {participant.Phone} already exists");
			return;

		if (emailExistsExcludedParticipant)
			// warning ($"Participant with email {participant.Email} already exists");
			return;

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