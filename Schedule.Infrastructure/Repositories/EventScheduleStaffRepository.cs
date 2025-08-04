using Microsoft.Data.SqlClient;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models.StaffRelated;
using Schedule.Infrastructure.Repositories.Common;
using Schedule.Infrastructure.Services;

namespace Schedule.Infrastructure.Repositories;

public class EventScheduleStaffRepository : BaseRepository, IEventScheduleStaffRepository
{
	public EventScheduleStaffRepository() : base(new SqlConnection(EnvironmentService.SqlConnectionString)) { }

	public async Task<List<EventScheduleStaff>> GetByEventIdAsync(Guid eventId)
	{
		List<EventScheduleStaff> result = new List<EventScheduleStaff>();

		using SqlCommand command = _connection.CreateCommand();
		command.CommandText = """
			SELECT Id, ReceptionId, EventScheduleId, StaffId
			FROM EventScheduleStaff
			WHERE EventScheduleId = @EventScheduleId
		""";
		AddParameter(command, "@EventScheduleId", eventId);

		_connection.Open();
		using SqlDataReader reader = await command.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			result.Add(new EventScheduleStaff
			{
				Id = reader.GetGuid(0),
				ReceptionId = reader.GetGuid(1),
				EventScheduleId = reader.GetGuid(2),
				StaffId = reader.GetGuid(3)
			});
		}
		_connection.Close();

		return result;
	}

	public async Task<Guid> CreateAsync(EventScheduleStaff entity)
	{
		entity.Id = Guid.NewGuid();

		using SqlCommand command = _connection.CreateCommand();
		command.CommandText = """
			INSERT INTO EventScheduleStaff (Id, ReceptionId, EventScheduleId, StaffId)
			VALUES (@Id, @ReceptionId, @EventScheduleId, @StaffId)
		""";
		AddParameter(command, "@Id", entity.Id);
		AddParameter(command, "@ReceptionId", entity.ReceptionId);
		AddParameter(command, "@EventScheduleId", entity.EventScheduleId);
		AddParameter(command, "@StaffId", entity.StaffId);

		_connection.Open();
		await command.ExecuteNonQueryAsync();
		_connection.Close();

		return entity.Id;
	}

	public async Task DeleteAsync(Guid id)
	{
		using SqlCommand command = _connection.CreateCommand();
		command.CommandText = "DELETE FROM EventScheduleStaff WHERE Id = @Id";
		AddParameter(command, "@Id", id);

		_connection.Open();
		await command.ExecuteNonQueryAsync();
		_connection.Close();
	}
}
