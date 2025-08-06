using Microsoft.Data.SqlClient;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models.StaffRelated;

namespace Schedule.Infrastructure.Repositories;

public class EventScheduleStaffRepository : IEventScheduleStaffRepository
{
	private readonly string _connectionString;

	public EventScheduleStaffRepository(string connectionString)
	{
		_connectionString = connectionString;
	}

	public async Task<Guid> CreateAsync(EventScheduleStaff eventScheduleStaff)
	{
		const string sql = @"
			INSERT INTO EventScheduleStaff (CompanyId, EventScheduleId, StaffId)
			OUTPUT INSERTED.Id
			VALUES (@CompanyId, @EventScheduleId, @StaffId)
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", eventScheduleStaff.CompanyId);
		command.Parameters.AddWithValue("@EventScheduleId", eventScheduleStaff.EventScheduleId);
		command.Parameters.AddWithValue("@StaffId", eventScheduleStaff.StaffId);

		object result = (await command.ExecuteScalarAsync())!;
		return (Guid)result;
	}

	public async Task<bool> DeleteByIdAsync(
		Guid eventScheduleStaffId,
		Guid companyId)
	{
		const string sql = @"
			DELETE FROM EventScheduleStaff 
			WHERE CompanyId = @CompanyId AND Id = @Id
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@Id", eventScheduleStaffId);

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}

	public async Task<List<EventScheduleStaff>> GetByEventScheduleIdAsync(Guid eventId)
	{
		const string sql = @"
			SELECT Id, CompanyId, EventScheduleId, StaffId
			FROM EventScheduleStaff
			WHERE EventScheduleId = @EventScheduleId
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@EventScheduleId", eventId);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		List<EventScheduleStaff> eventScheduleStaves = new();

		while (await reader.ReadAsync())
			eventScheduleStaves.Add(new EventScheduleStaff(
				reader.GetGuid(reader.GetOrdinal("Id")),
				reader.GetGuid(reader.GetOrdinal("CompanyId")),
				reader.GetGuid(reader.GetOrdinal("EventScheduleId")),
				reader.GetGuid(reader.GetOrdinal("StaffId"))
			));

		return eventScheduleStaves;
	}
}