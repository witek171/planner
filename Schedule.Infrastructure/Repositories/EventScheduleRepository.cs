using Microsoft.Data.SqlClient;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models;
using Schedule.Domain.Models.Enums;
using Schedule.Infrastructure.Utils;

namespace Schedule.Infrastructure.Repositories;

public class EventScheduleRepository : IEventScheduleRepository
{
	private readonly string _connectionString;

	public EventScheduleRepository(string connectionString)
	{
		_connectionString = connectionString;
	}

	public async Task<List<EventSchedule>> GetByStaffMemberIdAsync(
		Guid companyId,
		Guid staffMemberId)
	{
		const string sql = @"
			SELECT es.Id, es.CompanyId, es.EventTypeId, es.PlaceName, es.StartTime, es.CreatedAt, es.Status
			FROM EventSchedules es
			INNER JOIN EventScheduleStaff ess ON es.Id = ess.EventScheduleId
			WHERE es.CompanyId = @CompanyId AND es.Status <> @DeletedStatus 
			AND ess.StaffMemberId = @StaffMemberId";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@StaffMemberId", staffMemberId);
		command.Parameters.AddWithValue("@DeletedStatus", nameof(EventScheduleStatus.Deleted));

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		List<EventSchedule> eventSchedules = new();
		while (await reader.ReadAsync())
			eventSchedules.Add(DbMapper.MapEventSchedule(reader));

		return eventSchedules;
	}

	public async Task<List<EventSchedule>> GetAllAsync(Guid companyId)
	{
		const string sql = @"
			SELECT Id, CompanyId, EventTypeId, PlaceName, StartTime, CreatedAt, Status
			FROM EventSchedules 
			WHERE CompanyId = @CompanyId AND Status <> @DeletedStatus";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@DeletedStatus", nameof(EventScheduleStatus.Deleted));
		SqlDataReader reader = await command.ExecuteReaderAsync();
		List<EventSchedule> eventSchedules = new();
		while (await reader.ReadAsync())
			eventSchedules.Add(DbMapper.MapEventSchedule(reader));

		return eventSchedules;
	}

	public async Task<EventSchedule?> GetByIdAsync(
		Guid id,
		Guid companyId)
	{
		const string sql = @"
			SELECT Id, CompanyId, EventTypeId, PlaceName, StartTime, CreatedAt, Status
			FROM EventSchedules 
			WHERE Id = @Id AND CompanyId = @CompanyId AND Status <> @DeletedStatus";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", id);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@DeletedStatus", nameof(EventScheduleStatus.Deleted));
		SqlDataReader reader = await command.ExecuteReaderAsync();
		if (await reader.ReadAsync())
			return DbMapper.MapEventSchedule(reader);

		return null;
	}

	public async Task<Guid> CreateAsync(EventSchedule eventSchedule)
	{
		const string sql = @"
			INSERT INTO EventSchedules 
			(CompanyId, EventTypeId, PlaceName, StartTime)
			OUTPUT INSERTED.Id
			VALUES 
			(@CompanyId, @EventTypeId, @PlaceName, @StartTime)";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", eventSchedule.CompanyId);
		command.Parameters.AddWithValue("@EventTypeId", eventSchedule.EventTypeId);
		command.Parameters.AddWithValue("@PlaceName", eventSchedule.PlaceName);
		command.Parameters.AddWithValue("@StartTime", eventSchedule.StartTime);
		// command.Parameters.AddWithValue("@Status", eventSchedule.Status);

		object result = (await command.ExecuteScalarAsync())!;
		return (Guid)result;
	}

	public async Task<bool> UpdateAsync(EventSchedule eventSchedule)
	{
		const string sql = @"
			UPDATE EventSchedules 
			SET EventTypeId = @EventTypeId, PlaceName = @PlaceName, StartTime = @StartTime
			WHERE Id = @Id AND CompanyId = @CompanyId";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", eventSchedule.Id);
		command.Parameters.AddWithValue("@CompanyId", eventSchedule.CompanyId);
		command.Parameters.AddWithValue("@EventTypeId", eventSchedule.EventTypeId);
		command.Parameters.AddWithValue("@PlaceName", eventSchedule.PlaceName);
		command.Parameters.AddWithValue("@StartTime", eventSchedule.StartTime);

		Int32 affected = await command.ExecuteNonQueryAsync();
		return affected > 0;
	}

	public async Task<bool> DeleteAsync(
		Guid id,
		Guid companyId)
	{
		const string sql = @"
			DELETE FROM EventSchedules WHERE Id = @Id AND CompanyId = @CompanyId";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", id);
		command.Parameters.AddWithValue("@CompanyId", companyId);

		Int32 affected = await command.ExecuteNonQueryAsync();
		return affected > 0;
	}

	public async Task<bool> HasRelatedRecordsAsync(
		Guid id,
		Guid companyId)
	{
		const string sql = @"
			SELECT CASE WHEN EXISTS (
				SELECT 1 
				FROM EventScheduleStaff 
				WHERE EventScheduleId = @EventScheduleId 
				AND CompanyId = @CompanyId
				UNION ALL
				SELECT 1 
				FROM Reservations 
				WHERE EventScheduleId = @EventScheduleId 
				AND CompanyId = @CompanyId
			) THEN 1 ELSE 0 END";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@EventScheduleId", id);
		command.Parameters.AddWithValue("@CompanyId", companyId);

		object result = (await command.ExecuteScalarAsync())!;
		return (int)result == 1;
	}

	public async Task<bool> UpdateStatusAsync(EventSchedule eventSchedule)
	{
		const string sql = @"
			UPDATE EventSchedules SET
			Status = @Status
			WHERE Id = @Id AND CompanyId = @CompanyId";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", eventSchedule.Id);
		command.Parameters.AddWithValue("@CompanyId", eventSchedule.CompanyId);
		command.Parameters.AddWithValue("@Status", eventSchedule.Status.ToString());

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}
}