using Microsoft.Data.SqlClient;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models;

namespace Schedule.Infrastructure.Repositories;

public class StaffAvailabilityRepository : IStaffAvailabilityRepository
{
	private readonly string _connectionString;

	public StaffAvailabilityRepository(string connectionString)
	{
		_connectionString = connectionString;
	}

	public async Task<Guid> CreateAsync(StaffAvailability availability)
	{
		const string sql = @"
			INSERT INTO StaffAvailability 
			(CompanyId, StaffId, Date, StartTime, EndTime, IsAvailable)
			VALUES (@CompanyId, @StaffId, @Date, @StartTime, @EndTime, @IsAvailable)
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", availability.CompanyId);
		command.Parameters.AddWithValue("@StaffId", availability.StaffId);
		command.Parameters.AddWithValue("@Date", availability.Date.ToDateTime(TimeOnly.MinValue));
		command.Parameters.AddWithValue("@StartTime", availability.StartTime);
		command.Parameters.AddWithValue("@EndTime", availability.EndTime);
		command.Parameters.AddWithValue("@IsAvailable", availability.IsAvailable);

		object result = (await command.ExecuteScalarAsync())!;
		return (Guid)result;
	}

	public async Task<bool> PutAsync(StaffAvailability availability)
	{
		const string sql = @"
			UPDATE StaffAvailability SET
			StartTime = @StartTime,
			EndTime = @EndTime,
			IsAvailable = @IsAvailable
			WHERE Id = @Id
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", availability.Id);
		command.Parameters.AddWithValue("@StartTime", availability.StartTime);
		command.Parameters.AddWithValue("@EndTime", availability.EndTime);
		command.Parameters.AddWithValue("@IsAvailable", availability.IsAvailable);

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}

	public async Task<bool> DeleteByIdAsync(
		Guid staffAvailabilityId,
		Guid companyId)
	{
		const string sql = @"
			DELETE FROM StaffAvailability 
			WHERE CompanyId = @CompanyId AND Id = @Id
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@Id", staffAvailabilityId);

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}

	public async Task<StaffAvailability?> GetByIdAsync(Guid staffAvailabilityId)
	{
		const string sql = @"
			SELECT Id, CompanyId, StaffId, Date, StartTime, EndTime, IsAvailable
			FROM StaffAvailability 
			WHERE Id = @Id
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", staffAvailabilityId);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		if (!await reader.ReadAsync())
			return null;

		return new StaffAvailability(
			reader.GetGuid(reader.GetOrdinal("Id")),
			reader.GetGuid(reader.GetOrdinal("CompanyId")),
			reader.GetGuid(reader.GetOrdinal("StaffId")),
			DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("Date"))),
			reader.GetDateTime(reader.GetOrdinal("StartTime")),
			reader.GetDateTime(reader.GetOrdinal("EndTime")),
			reader.GetBoolean(reader.GetOrdinal("IsAvailable"))
		);
	}

	public async Task<List<StaffAvailability>> GetByStaffIdAsync(Guid staffId)
	{
		const string sql = @"
			SELECT Id, CompanyId, StaffId, Date, StartTime, EndTime, IsAvailable
			FROM StaffAvailability
			WHERE StaffId = @StaffId
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@StaffId", staffId);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		List<StaffAvailability> availabilities = new();

		while (await reader.ReadAsync())
			availabilities.Add(new StaffAvailability(
				reader.GetGuid(reader.GetOrdinal("Id")),
				reader.GetGuid(reader.GetOrdinal("CompanyId")),
				reader.GetGuid(reader.GetOrdinal("StaffId")),
				DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("Date"))),
				reader.GetDateTime(reader.GetOrdinal("StartTime")),
				reader.GetDateTime(reader.GetOrdinal("EndTime")),
				reader.GetBoolean(reader.GetOrdinal("IsAvailable"))
			));

		return availabilities;
	}
}