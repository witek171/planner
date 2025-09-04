using Microsoft.Data.SqlClient;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models;
using Schedule.Infrastructure.Utils;

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
			schedules.Add(DbMapper.MapEventSchedule(reader));

		return schedules;
	}
}
