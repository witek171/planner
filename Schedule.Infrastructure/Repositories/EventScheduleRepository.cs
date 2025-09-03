using Microsoft.Data.SqlClient;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models;

namespace Schedule.Infrastructure.Repositories;

public class EventScheduleRepository : IEventScheduleRepository
{
	private readonly string _connectionString;

	public EventScheduleRepository(string connectionString)
	{
		_connectionString = connectionString;
	}

	public async Task<List<EventSchedule>> GetByStaffMemberIdAsync(Guid companyId, Guid staffMemberId)
	{
		const string sql = @"
			SELECT es.Id, es.CompanyId, es.EventTypeId, es.PlaceName, es.StartTime, es.CreatedAt, es.Status
			FROM EventSchedules es
			INNER JOIN EventScheduleStaff ess ON es.Id = ess.EventScheduleId
			WHERE es.CompanyId = @CompanyId AND ess.StaffMemberId = @StaffMemberId";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@StaffMemberId", staffMemberId);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		List<EventSchedule> schedules = new();
		while (await reader.ReadAsync())
		{
			schedules.Add(new EventSchedule(
				reader.GetGuid(reader.GetOrdinal("Id")),
				reader.GetGuid(reader.GetOrdinal("CompanyId")),
				reader.GetGuid(reader.GetOrdinal("EventTypeId")),
				reader.GetString(reader.GetOrdinal("PlaceName")),
				reader.GetDateTime(reader.GetOrdinal("StartTime")),
				reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
				reader.GetString(reader.GetOrdinal("Status"))));
		}
		return schedules;
	}
}
