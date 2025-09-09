using Microsoft.Data.SqlClient;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models;
using Schedule.Domain.Models.Enums;
using Schedule.Infrastructure.Utils;

namespace Schedule.Infrastructure.Repositories;

public class EventTypeRepository : IEventTypeRepository
{
	private readonly string _connectionString;

	public EventTypeRepository(string connectionString)
	{
		_connectionString = connectionString;
	}

	public async Task<List<EventType>> GetAllAsync(Guid companyId)
	{
		const string sql = @"
			SELECT Id, CompanyId, Name, Description, Duration, Price, MaxParticipants, MinStaff
			FROM EventTypes 
			WHERE CompanyId = @CompanyId AND isDeleted = 0";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		SqlDataReader reader = await command.ExecuteReaderAsync();
		List<EventType> eventTypes = new();
		while (await reader.ReadAsync())
			eventTypes.Add(DbMapper.MapEventType(reader));

		return eventTypes;
	}

	public async Task<EventType?> GetByIdAsync(
		Guid id,
		Guid companyId)
	{
		const string sql = @"
			SELECT Id, CompanyId, Name, Description, Duration, 
			Price, MaxParticipants, MinStaff, isDeleted
			FROM EventTypes 
			WHERE Id = @Id AND CompanyId = @CompanyId AND isDeleted = 0";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", id);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		SqlDataReader reader = await command.ExecuteReaderAsync();
		if (await reader.ReadAsync())
			return DbMapper.MapEventType(reader);

		return null;
	}

	public async Task<Guid> CreateAsync(EventType eventType)
	{
		const string sql = @"
			INSERT INTO EventTypes 
			(CompanyId, Name, Description, Duration, Price, MaxParticipants, MinStaff)
			OUTPUT INSERTED.Id
			VALUES 
			(@CompanyId, @Name, @Description, @Duration, @Price, @MaxParticipants, @MinStaff)";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", eventType.CompanyId);
		command.Parameters.AddWithValue("@Name", eventType.Name);
		command.Parameters.AddWithValue("@Description", eventType.Description);
		command.Parameters.AddWithValue("@Duration", eventType.Duration);
		command.Parameters.AddWithValue("@Price", eventType.Price);
		command.Parameters.AddWithValue("@MaxParticipants", eventType.MaxParticipants);
		command.Parameters.AddWithValue("@MinStaff", eventType.MinStaff);

		object result = (await command.ExecuteScalarAsync())!;
		return (Guid)result;
	}

	public async Task<bool> UpdateAsync(EventType eventType)
	{
		const string sql = @"
			UPDATE EventTypes 
			SET Name = @Name, Description = @Description, Duration = @Duration,
				Price = @Price, MaxParticipants = @MaxParticipants, MinStaff = @MinStaff
			WHERE Id = @Id AND CompanyId = @CompanyId";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", eventType.Id);
		command.Parameters.AddWithValue("@CompanyId", eventType.CompanyId);
		command.Parameters.AddWithValue("@Name", eventType.Name);
		command.Parameters.AddWithValue("@Description", eventType.Description);
		command.Parameters.AddWithValue("@Duration", eventType.Duration);
		command.Parameters.AddWithValue("@Price", eventType.Price);
		command.Parameters.AddWithValue("@MaxParticipants", eventType.MaxParticipants);
		command.Parameters.AddWithValue("@MinStaff", eventType.MinStaff);

		Int32 affected = await command.ExecuteNonQueryAsync();
		return affected > 0;
	}

	public async Task<bool> DeleteAsync(
		Guid id,
		Guid companyId)
	{
		const string sql = @"
			DELETE FROM EventTypes WHERE Id = @Id AND CompanyId = @CompanyId";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", id);
		command.Parameters.AddWithValue("@CompanyId", companyId);

		Int32 affected = await command.ExecuteNonQueryAsync();
		return affected > 0;
	}

	public async Task<bool> ExistsInNonDeletedEventSchedulesAsync(
		Guid id,
		Guid companyId)
	{
		const string sql = @"
			SELECT CASE WHEN EXISTS (
				SELECT 1 
				FROM EventSchedules 
				WHERE EventTypeId = @EventTypeId 
				  AND CompanyId = @CompanyId
				  AND Status <> @DeletedStatus
			) THEN 1 ELSE 0 END";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@EventTypeId", id);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@DeletedStatus", nameof(EventScheduleStatus.Deleted));

		object result = (await command.ExecuteScalarAsync())!;
		return (int)result == 1;
	}

	public async Task<bool> ExistsOnlyInDeletedEventSchedulesAsync(
		Guid id,
		Guid companyId)
	{
		const string sql = @"
		SELECT CASE WHEN EXISTS (
			SELECT 1
			FROM EventSchedules es
			WHERE es.EventTypeId = @EventTypeId
			AND es.CompanyId = @CompanyId
			AND es.Status = @DeletedStatus
			AND NOT EXISTS (
				SELECT 1 
				FROM EventSchedules es2
				WHERE es2.EventTypeId = @EventTypeId
				AND es2.CompanyId = @CompanyId
				AND es2.Status <> @DeletedStatus
			)
		) THEN 1 ELSE 0 END";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@EventTypeId", id);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@DeletedStatus", nameof(EventScheduleStatus.Deleted));

		object result = (await command.ExecuteScalarAsync())!;
		return (int)result == 1;
	}
	
	public async Task<bool> UpdateSoftDeleteAsync(EventType eventType)
	{
		const string sql = @"
			UPDATE EventTypes SET
			IsDeleted = @IsDeleted
			WHERE Id = @Id AND CompanyId = @CompanyId";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", eventType.Id);
		command.Parameters.AddWithValue("@CompanyId", eventType.CompanyId);
		command.Parameters.AddWithValue("@IsDeleted", eventType.IsDeleted);

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}
}