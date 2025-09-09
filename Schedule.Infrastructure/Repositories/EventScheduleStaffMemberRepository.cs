using Microsoft.Data.SqlClient;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models;
using Schedule.Infrastructure.Utils;

namespace Schedule.Infrastructure.Repositories;

public class EventScheduleStaffMemberRepository : IEventScheduleStaffMemberRepository
{
	private readonly string _connectionString;

	public EventScheduleStaffMemberRepository(string connectionString)
	{
		_connectionString = connectionString;
	}

	public async Task<Guid> CreateAsync(EventScheduleStaffMember eventScheduleStaffMember)
	{
		const string sql = @"
			INSERT INTO EventScheduleStaff (CompanyId, EventScheduleId, StaffMemberId)
			OUTPUT INSERTED.Id
			VALUES (@CompanyId, @EventScheduleId, @StaffMemberId)";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", eventScheduleStaffMember.CompanyId);
		command.Parameters.AddWithValue("@EventScheduleId", eventScheduleStaffMember.EventScheduleId);
		command.Parameters.AddWithValue("@StaffMemberId", eventScheduleStaffMember.StaffMemberId);

		object result = (await command.ExecuteScalarAsync())!;
		return (Guid)result;
	}

	public async Task<bool> DeleteByIdAsync(
		Guid eventScheduleStaffMemberId,
		Guid companyId)
	{
		const string sql = @"
			DELETE FROM EventScheduleStaff
			WHERE CompanyId = @CompanyId AND Id = @Id";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@Id", eventScheduleStaffMemberId);

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}

	public async Task<List<EventScheduleStaffMember>> GetByStaffMemberIdAsync(
		Guid companyId,
		Guid staffMemberId)
	{
		const string sql = @"
			SELECT Id, CompanyId, EventScheduleId, StaffMemberId
			FROM EventScheduleStaff
			WHERE StaffMemberId = @StaffMemberId AND CompanyId = @CompanyId";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@StaffMemberId", staffMemberId);
		command.Parameters.AddWithValue("@CompanyId", companyId);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		List<EventScheduleStaffMember> eventSchedules = new();

		while (await reader.ReadAsync())
			eventSchedules.Add(DbMapper.MapEventScheduleStaffMember(reader));

		return eventSchedules;
	}

	public async Task<List<Guid>> GetEventScheduleStaffIdsByEventScheduleIdAsync(
		Guid eventScheduleId,
		Guid companyId)
	{
		const string sql = @"
			SELECT Id
			FROM EventScheduleStaff
			WHERE EventScheduleId = @EventScheduleId AND CompanyId = @CompanyId";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@EventScheduleId", eventScheduleId);
		command.Parameters.AddWithValue("@CompanyId", companyId);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		List<Guid> eventScheduleStaffIds = new();

		while (await reader.ReadAsync())
			eventScheduleStaffIds.Add(reader.GetGuid(reader.GetOrdinal("Id")));

		return eventScheduleStaffIds;
	}

	public async Task<bool> ExistsByIdAsync(
		Guid companyId,
		Guid id)
	{
		const string sql = @"
			SELECT 1
			FROM EventScheduleStaff
			WHERE CompanyId = @CompanyId AND Id = @Id";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@Id", id);

		object? result = await command.ExecuteScalarAsync();
		return result != null;
	}
}