using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models;

namespace Schedule.Infrastructure.Repositories;

public class ParticipantRepository : IParticipantRepository
{
	private readonly string _connectionString;

	public ParticipantRepository(string connectionString)
	{
		_connectionString = connectionString;
	}

	public async Task CreateAsync(Participant participant)
	{
		const string sql = @"
            INSERT INTO Participants (Id, CompanyId, Email, FirstName, LastName, Phone, GdprConsent, CreatedAt)
            VALUES (@Id, @CompanyId, @Email, @FirstName, @LastName, @Phone, @GdprConsent, @CreatedAt);";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", participant.Id);
		command.Parameters.AddWithValue("@CompanyId", participant.CompanyId);
		command.Parameters.AddWithValue("@Email", participant.Email);
		command.Parameters.AddWithValue("@FirstName", participant.FirstName);
		command.Parameters.AddWithValue("@LastName", participant.LastName);
		command.Parameters.AddWithValue("@Phone", participant.Phone);
		command.Parameters.AddWithValue("@GdprConsent", participant.GdprConsent);
		command.Parameters.AddWithValue("@CreatedAt", participant.CreatedAt);

		await command.ExecuteNonQueryAsync();
	}

	public async Task<bool> PatchAsync(Participant participant)
	{
		List<string> updateClauses = new List<string>();

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using DbTransaction transaction = await connection.BeginTransactionAsync();

		try
		{
			await using SqlCommand? command = connection.CreateCommand();
			command.Transaction = (SqlTransaction)transaction;

			command.Parameters.AddWithValue("@Id", participant.Id);
			command.Parameters.AddWithValue("@CompanyId", participant.CompanyId);

			if (participant.Email != null)
			{
				updateClauses.Add("Email = @Email");
				command.Parameters.AddWithValue("@Email", participant.Email);
			}

			if (participant.FirstName != null)
			{
				updateClauses.Add("FirstName = @FirstName");
				command.Parameters.AddWithValue("@FirstName", participant.FirstName);
			}

			if (participant.LastName != null)
			{
				updateClauses.Add("LastName = @LastName");
				command.Parameters.AddWithValue("@LastName", participant.LastName);
			}

			if (participant.Phone != null)
			{
				updateClauses.Add("Phone = @Phone");
				command.Parameters.AddWithValue("@Phone", participant.Phone);
			}

			if (participant.GdprConsent != null)
			{
				updateClauses.Add("GdprConsent = @GdprConsent");
				command.Parameters.AddWithValue("@GdprConsent", participant.GdprConsent);
			}

			if (updateClauses.Count == 0)
				return false;

			string sql = $"""
			              UPDATE Participants
			                          SET {string.Join(", ", updateClauses)}
			                          WHERE Id = @Id AND CompanyId = @CompanyId
			              """;

			command.CommandText = sql;

			int rowsAffected = await command.ExecuteNonQueryAsync();
			await transaction.CommitAsync();

			return rowsAffected > 0;
		}
		catch (Exception ex)
		{
			await transaction.RollbackAsync();
			throw new DataException("Failed to update participant", ex);
		}
	}

	public async Task<bool> DeleteByIdAsync(
		Guid id,
		Guid companyId
	)
	{
		const string sql = """

		                           DELETE FROM Participants 
		                           WHERE CompanyId = @CompanyId AND Id = @Id
		                   """;

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@Id", id);

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}

	public async Task<Participant?> GetByIdAsync(
		Guid id,
		Guid companyId
	)
	{
		const string sql = """

		                           SELECT Id, CompanyId, Email, FirstName, LastName, Phone, GdprConsent, CreatedAt
		                           FROM Participants 
		                           WHERE CompanyId = @CompanyId AND Id = @Id
		                   """;

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@Id", id);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		if (!await reader.ReadAsync())
			return null;

		return new Participant(
			reader.GetGuid(reader.GetOrdinal("Id")),
			reader.GetGuid(reader.GetOrdinal("CompanyId")),
			reader.GetString(reader.GetOrdinal("Email")),
			reader.GetString(reader.GetOrdinal("FirstName")),
			reader.GetString(reader.GetOrdinal("LastName")),
			reader.GetString(reader.GetOrdinal("Phone")),
			reader.GetBoolean(reader.GetOrdinal("GdprConsent")),
			reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
		);
	}

	public async Task<Participant?> GetByEmailAsync(
		string email,
		Guid companyId
	)
	{
		const string sql = """

		                               SELECT Id, CompanyId, Email, FirstName, LastName, Phone, GdprConsent, CreatedAt
		                               FROM Participants 
		                               WHERE CompanyId = @CompanyId AND Email = @Email
		                   """;

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@Email", email);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		if (!await reader.ReadAsync())
			return null;

		return new Participant(
			reader.GetGuid(reader.GetOrdinal("Id")),
			reader.GetGuid(reader.GetOrdinal("CompanyId")),
			reader.GetString(reader.GetOrdinal("Email")),
			reader.GetString(reader.GetOrdinal("FirstName")),
			reader.GetString(reader.GetOrdinal("LastName")),
			reader.GetString(reader.GetOrdinal("Phone")),
			reader.GetBoolean(reader.GetOrdinal("GdprConsent")),
			reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
		);
	}

	public async Task<List<Participant>> GetAllAsync(Guid companyId)
	{
		const string sql = """

		                           SELECT Id, CompanyId, Email, FirstName, LastName, Phone, GdprConsent, CreatedAt
		                           FROM Participants 
		                           WHERE CompanyId = @CompanyId
		                           ORDER BY CreatedAt DESC
		                   """;

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		List<Participant> participants = new List<Participant>();

		while (await reader.ReadAsync())
		{
			participants.Add(new Participant(
				reader.GetGuid(reader.GetOrdinal("Id")),
				reader.GetGuid(reader.GetOrdinal("CompanyId")),
				reader.GetString(reader.GetOrdinal("Email")),
				reader.GetString(reader.GetOrdinal("FirstName")),
				reader.GetString(reader.GetOrdinal("LastName")),
				reader.GetString(reader.GetOrdinal("Phone")),
				reader.GetBoolean(reader.GetOrdinal("GdprConsent")),
				reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
			));
		}

		return participants;
	}

	public async Task<bool> EmailExistsAsync(
		string email,
		Guid companyId
	)
	{
		const string sql = """

		                               SELECT COUNT(1) 
		                               FROM Participants 
		                               WHERE CompanyId = @CompanyId AND Email = @Email
		                   """;

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@Email", email);

		int count = (int)await command.ExecuteScalarAsync();
		return count > 0;
	}

	public async Task<bool> PhoneExistsAsync(
		string phone,
		Guid companyId
	)
	{
		const string sql = """

		                               SELECT COUNT(1) 
		                               FROM Participants 
		                               WHERE CompanyId = @CompanyId AND Phone = @Phone
		                   """;

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@Phone", phone);

		int count = (int)await command.ExecuteScalarAsync();
		return count > 0;
	}

	public async Task<bool> PhoneExistsExcludedParticipantAsync(
		string phone,
		Guid companyId,
		Guid excludeParticipantId
	)
	{
		const string sql = """
		                   SELECT COUNT(1) 
		                   FROM Participants 
		                   WHERE CompanyId = @CompanyId 
		                     AND Phone = @Phone 
		                     AND Id != @ExcludeParticipantId
		                   """;

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@Phone", phone);
		command.Parameters.AddWithValue("@ExcludeParticipantId", excludeParticipantId);

		int count = (int)await command.ExecuteScalarAsync();
		return count > 0;
	}

	public async Task<bool> EmailExistsExcludedParticipantAsync(
		string email,
		Guid companyId,
		Guid excludeParticipantId
	)
	{
		const string sql = """
		                   SELECT COUNT(1) 
		                   FROM Participants 
		                   WHERE CompanyId = @CompanyId 
		                     AND Email = @Email 
		                     AND Id != @ExcludeParticipantId
		                   """;

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@Email", email);
		command.Parameters.AddWithValue("@ExcludeParticipantId", excludeParticipantId);

		int count = (int)await command.ExecuteScalarAsync();
		return count > 0;
	}
}