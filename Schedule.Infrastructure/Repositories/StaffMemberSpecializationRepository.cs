using Microsoft.Data.SqlClient;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models;

namespace Schedule.Infrastructure.Repositories;

public class StaffMemberSpecializationRepository : IStaffMemberSpecializationRepository
{
	private readonly string _connectionString;

	public StaffMemberSpecializationRepository(string connectionString)
	{
		_connectionString = connectionString;
	}

	public async Task<Guid> CreateAsync(StaffMemberSpecialization specialization)
	{
		const string sql = @"
			INSERT INTO StaffSpecializations (CompanyId, StaffMemberId, SpecializationId)
			VALUES (@CompanyId, @StaffMemberId, @SpecializationId)
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", specialization.CompanyId);
		command.Parameters.AddWithValue("@StaffMemberId", specialization.StaffMemberId);
		command.Parameters.AddWithValue("@SpecializationId", specialization.SpecializationId);

		object result = (await command.ExecuteScalarAsync())!;
		return (Guid)result;
	}

	public async Task<bool> DeleteByIdAsync(
		Guid companyId,
		Guid staffMemberSpecializationId)
	{
		const string sql = @"
			DELETE FROM StaffSpecializations 
			WHERE CompanyId = @CompanyId AND Id = @Id
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@Id", staffMemberSpecializationId);

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}

	public async Task<List<StaffMemberSpecialization>> GetByStaffMemberIdAsync(Guid staffMemberId)
	{
		const string sql = @"
			SELECT Id, CompanyId, StaffMemberId, SpecializationId
			FROM StaffSpecializations
			WHERE StaffMemberId = @StaffMemberId
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@StaffMemberId", staffMemberId);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		List<StaffMemberSpecialization> specializations = new();

		while (await reader.ReadAsync())
			specializations.Add(new StaffMemberSpecialization(
				reader.GetGuid(reader.GetOrdinal("Id")),
				reader.GetGuid(reader.GetOrdinal("CompanyId")),
				reader.GetGuid(reader.GetOrdinal("StaffMemberId")),
				reader.GetGuid(reader.GetOrdinal("SpecializationId"))
			));

		return specializations;
	}
}