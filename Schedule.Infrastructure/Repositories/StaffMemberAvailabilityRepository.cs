using Microsoft.Data.SqlClient;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models;
using Schedule.Infrastructure.Utils;

namespace Schedule.Infrastructure.Repositories;

public class StaffMemberAvailabilityRepository : IStaffMemberAvailabilityRepository
{
	private readonly string _connectionString;
	private readonly DbMapper _dbMapper;

	public StaffMemberAvailabilityRepository(
		string connectionString,
		DbMapper dbMapper)
	{
		_connectionString = connectionString;
		_dbMapper = dbMapper;
	}

	public async Task<Guid> CreateAsync(StaffMemberAvailability availability)
	{
		const string sql = @"
			INSERT INTO StaffAvailability 
			(CompanyId, StaffMemberId, Date, StartTime, EndTime, IsAvailable)
			OUTPUT INSERTED.Id
			VALUES (@CompanyId, @StaffMemberId, @Date, @StartTime, @EndTime, @IsAvailable)
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", availability.CompanyId);
		command.Parameters.AddWithValue("@StaffMemberId", availability.StaffMemberId);
		command.Parameters.AddWithValue("@Date", availability.Date.ToDateTime(TimeOnly.MinValue));
		command.Parameters.AddWithValue("@StartTime", availability.StartTime);
		command.Parameters.AddWithValue("@EndTime", availability.EndTime);
		command.Parameters.AddWithValue("@IsAvailable", availability.IsAvailable);

		object result = (await command.ExecuteScalarAsync())!;
		return (Guid)result;
	}

	public async Task<bool> DeleteByIdAsync(
		Guid companyId,
		Guid staffMemberAvailabilityId)
	{
		const string sql = @"
			DELETE FROM StaffAvailability 
			WHERE CompanyId = @CompanyId AND Id = @Id
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@Id", staffMemberAvailabilityId);

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}

	public async Task<List<StaffMemberAvailability>> GetByStaffMemberIdAsync(
		Guid companyId,
		Guid staffMemberId)
	{
		const string sql = @"
			SELECT Id, CompanyId, StaffMemberId, Date, StartTime, EndTime, IsAvailable
			FROM StaffAvailability
			WHERE StaffMemberId = @StaffMemberId AND CompanyId = @CompanyId
			AND IsAvailable = 1
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@StaffMemberId", staffMemberId);
		command.Parameters.AddWithValue("@CompanyId", companyId);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		List<StaffMemberAvailability> availabilities = new();

		while (await reader.ReadAsync())
			availabilities.Add(_dbMapper.MapStaffMemberAvailability(reader));

		return availabilities;
	}
}