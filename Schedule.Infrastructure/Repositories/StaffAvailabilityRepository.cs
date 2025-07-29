using Microsoft.Data.SqlClient;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models.StaffRelated;
using Schedule.Infrastructure.Repositories.Common;
using Schedule.Infrastructure.Services;

namespace Schedule.Infrastructure.Repositories;

public class StaffAvailabilityRepository : BaseRepository, IStaffAvailabilityRepository
{
	public StaffAvailabilityRepository() : base(new SqlConnection(EnvironmentService.SqlConnectionString)) { }

	public async Task<List<StaffAvailability>> GetByStaffIdAsync(Guid staffId)
	{
		var result = new List<StaffAvailability>();

		using var command = _connection.CreateCommand();
		command.CommandText = """
			SELECT Id, ReceptionId, StaffId, Date, StartTime, EndTime, IsAvailable
			FROM StaffAvailability
			WHERE StaffId = @StaffId
		""";
		AddParameter(command, "@StaffId", staffId);

		await _connection.OpenAsync();
		using var reader = await command.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			result.Add(new StaffAvailability
			{
				Id = reader.GetGuid(0),
				ReceptionId = reader.GetGuid(1),
				StaffId = reader.GetGuid(2),
				Date = DateOnly.FromDateTime(reader.GetDateTime(3)),
				StartTime = reader.GetDateTime(4),
				EndTime = reader.GetDateTime(5),
				IsAvailable = reader.GetBoolean(6)
			});
		}
		await _connection.CloseAsync();

		return result;
	}

	public async Task<StaffAvailability?> GetByIdAsync(Guid id)
	{
		using var command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM StaffAvailability WHERE Id = @Id";
		AddParameter(command, "@Id", id);

		await _connection.OpenAsync();
		using var reader = await command.ExecuteReaderAsync();
		if (await reader.ReadAsync())
		{
			var availability = new StaffAvailability
			{
				Id = reader.GetGuid(0),
				ReceptionId = reader.GetGuid(1),
				StaffId = reader.GetGuid(2),
				Date = DateOnly.FromDateTime(reader.GetDateTime(3)),
				StartTime = reader.GetDateTime(4),
				EndTime = reader.GetDateTime(5),
				IsAvailable = reader.GetBoolean(6)
			};
			await _connection.CloseAsync();
			return availability;
		}
		await _connection.CloseAsync();
		return null;
	}

	public async Task<Guid> CreateAsync(StaffAvailability availability)
	{
		using var command = _connection.CreateCommand();
		command.CommandText = """
			INSERT INTO StaffAvailability 
			(Id, ReceptionId, StaffId, Date, StartTime, EndTime, IsAvailable)
			VALUES (@Id, @ReceptionId, @StaffId, @Date, @StartTime, @EndTime, @IsAvailable)
		""";

		availability.Id = Guid.NewGuid();

		AddParameter(command, "@Id", availability.Id);
		AddParameter(command, "@ReceptionId", availability.ReceptionId);
		AddParameter(command, "@StaffId", availability.StaffId);
		AddParameter(command, "@Date", availability.Date.ToDateTime(TimeOnly.MinValue));
		AddParameter(command, "@StartTime", availability.StartTime);
		AddParameter(command, "@EndTime", availability.EndTime);
		AddParameter(command, "@IsAvailable", availability.IsAvailable);

		await _connection.OpenAsync();
		await command.ExecuteNonQueryAsync();
		await _connection.CloseAsync();

		return availability.Id;
	}

	public async Task UpdateAsync(StaffAvailability availability)
	{
		using var command = _connection.CreateCommand();
		command.CommandText = """
			UPDATE StaffAvailability SET
				StartTime = @StartTime,
				EndTime = @EndTime,
				IsAvailable = @IsAvailable
			WHERE Id = @Id
		""";

		AddParameter(command, "@Id", availability.Id);
		AddParameter(command, "@StartTime", availability.StartTime);
		AddParameter(command, "@EndTime", availability.EndTime);
		AddParameter(command, "@IsAvailable", availability.IsAvailable);

		await _connection.OpenAsync();
		await command.ExecuteNonQueryAsync();
		await _connection.CloseAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		using var command = _connection.CreateCommand();
		command.CommandText = "DELETE FROM StaffAvailability WHERE Id = @Id";
		AddParameter(command, "@Id", id);

		await _connection.OpenAsync();
		await command.ExecuteNonQueryAsync();
		await _connection.CloseAsync();
	}
}
