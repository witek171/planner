using Microsoft.Data.SqlClient;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models;
using Schedule.Infrastructure.Utils;

namespace Schedule.Infrastructure.Repositories;

public class ParticipantRepository : IParticipantRepository
{
	private readonly string _connectionString;
	private readonly DbMapper _dbMapper;

	public ParticipantRepository(
		string connectionString,
		DbMapper dbMapper)
	{
		_connectionString = connectionString;
		_dbMapper = dbMapper;
	}

	public async Task<Guid> CreateAsync(Participant participant)
	{
		const string sql = @"
			INSERT INTO Participants (CompanyId, Email, FirstName, LastName, Phone, GdprConsent)
			OUTPUT INSERTED.Id
			VALUES (@CompanyId, @Email, @FirstName, @LastName, @Phone, @GdprConsent);
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", participant.CompanyId);
		command.Parameters.AddWithValue("@Email", participant.Email);
		command.Parameters.AddWithValue("@FirstName", participant.FirstName);
		command.Parameters.AddWithValue("@LastName", participant.LastName);
		command.Parameters.AddWithValue("@Phone", participant.Phone);
		command.Parameters.AddWithValue("@GdprConsent", participant.GdprConsent);

		object result = (await command.ExecuteScalarAsync())!;
		return (Guid)result;
	}

	public async Task<bool> PutAsync(Participant participant)
	{
		const string sql = @"
			UPDATE Participants
			SET Email = @Email,
			FirstName = @FirstName,
			LastName = @LastName,
			Phone = @Phone
			WHERE Id = @Id AND CompanyId = @CompanyId
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", participant.Id);
		command.Parameters.AddWithValue("@CompanyId", participant.CompanyId);
		command.Parameters.AddWithValue("@Email", participant.Email);
		command.Parameters.AddWithValue("@FirstName", participant.FirstName);
		command.Parameters.AddWithValue("@LastName", participant.LastName);
		command.Parameters.AddWithValue("@Phone", participant.Phone);

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}

	public async Task<bool> DeleteByIdAsync(
		Guid participantId,
		Guid companyId
	)
	{
		const string sql = @"
			DELETE FROM Participants 
			WHERE CompanyId = @CompanyId AND Id = @Id
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@Id", participantId);

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}

	public async Task<Participant?> GetByIdAsync(
		Guid participantId,
		Guid companyId
	)
	{
		const string sql = @"
			SELECT Id, CompanyId, Email, FirstName, LastName, Phone, GdprConsent, CreatedAt
			FROM Participants 
			WHERE CompanyId = @CompanyId AND Id = @Id
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@Id", participantId);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		if (!await reader.ReadAsync())
			return null;

		return _dbMapper.MapParticipant(reader);
	}

	public async Task<Participant?> GetByEmailAsync(
		string email,
		Guid companyId
	)
	{
		const string sql = @"
			SELECT Id, CompanyId, Email, FirstName, LastName, Phone, GdprConsent, CreatedAt
			FROM Participants 
			WHERE CompanyId = @CompanyId AND Email = @Email
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@Email", email);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		if (!await reader.ReadAsync())
			return null;

		return _dbMapper.MapParticipant(reader);
	}

	public async Task<List<Participant>> GetAllAsync(Guid companyId)
	{
		const string sql = @"
			SELECT Id, CompanyId, Email, FirstName, LastName, Phone, GdprConsent, CreatedAt
			FROM Participants 
			WHERE CompanyId = @CompanyId
			ORDER BY CreatedAt DESC
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		List<Participant> participants = new();

		while (await reader.ReadAsync())
			participants.Add(_dbMapper.MapParticipant(reader));

		return participants;
	}

	public async Task<bool> IsParticipantAssignedToReservationsAsync(
		Guid participantId,
		Guid companyId
	)
	{
		const string sql = @"
			SELECT COUNT(1)
			FROM Reservations 
			WHERE CompanyId = @CompanyId AND ParticipantId = @ParticipantId
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@ParticipantId", participantId);

		int count = (int)(await command.ExecuteScalarAsync())!;
		return count > 0;
	}
}