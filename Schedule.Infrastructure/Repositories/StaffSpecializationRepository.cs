using Microsoft.Data.SqlClient;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models.StaffRelated;

namespace Schedule.Infrastructure.Repositories;

public class StaffSpecializationRepository : IStaffSpecializationRepository
{
	private readonly string _connectionString;

	public StaffSpecializationRepository(string connectionString)
	{
		_connectionString = connectionString;
	}

	public async Task<Guid> CreateAsync(StaffSpecialization specialization)
	{
		const string sql = @"
			INSERT INTO StaffSpecializations (CompanyId, StaffId, SpecializationId)
			VALUES (@CompanyId, @StaffId, @SpecializationId)
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", specialization.CompanyId);
		command.Parameters.AddWithValue("@StaffId", specialization.StaffId);
		command.Parameters.AddWithValue("@SpecializationId", specialization.SpecializationId);

		object result = (await command.ExecuteScalarAsync())!;
		return (Guid)result;
	}

	public async Task<bool> DeleteByIdAsync(
		Guid companyId,
		Guid staffSpecializationId)
	{
		const string sql = @"
			DELETE FROM StaffSpecializations 
			WHERE CompanyId = @CompanyId AND Id = @Id
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@Id", staffSpecializationId);

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}

	public async Task<List<StaffSpecialization>> GetByStaffIdAsync(Guid staffId)
	{
		const string sql = @"
			SELECT Id, CompanyId, StaffId, SpecializationId
			FROM StaffSpecializations
			WHERE StaffId = @StaffId
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@StaffId", staffId);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		List<StaffSpecialization> specializations = new();

		while (await reader.ReadAsync())
			specializations.Add(new StaffSpecialization(
				reader.GetGuid(reader.GetOrdinal("Id")),
				reader.GetGuid(reader.GetOrdinal("CompanyId")),
				reader.GetGuid(reader.GetOrdinal("StaffId")),
				reader.GetGuid(reader.GetOrdinal("SpecializationId"))
			));

		return specializations;
	}
}