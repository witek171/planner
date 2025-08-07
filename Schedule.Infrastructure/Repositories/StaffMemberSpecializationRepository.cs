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

	public async Task<Guid> CreateAsync(
		Guid companyId,
		StaffMemberSpecialization staffMemberSpecialization)
	{
		const string sql = @"
			INSERT INTO StaffSpecializations (CompanyId, StaffMemberId, SpecializationId)
			VALUES (@CompanyId, @StaffMemberId, @SpecializationId)
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue(
			"@CompanyId", companyId);
		command.Parameters.AddWithValue(
			"@StaffMemberId", staffMemberSpecialization.StaffMemberId);
		command.Parameters.AddWithValue(
			"@SpecializationId", staffMemberSpecialization.SpecializationId);

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

	public async Task<List<Specialization>> GetStaffMemberSpecializationsAsync(
		Guid staffMemberId,
		Guid companyId)
	{
		const string sql = @"
			SELECT s.Id, s.CompanyId, s.Name, s.Description
			FROM StaffSpecializations ss
			INNER JOIN Specializations s ON ss.SpecializationId = s.Id
			WHERE ss.StaffMemberId = @StaffMemberId 
			AND ss.CompanyId = @CompanyId
			AND s.CompanyId = @CompanyId
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@StaffMemberId", staffMemberId);
		command.Parameters.AddWithValue("@CompanyId", companyId);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		List<Specialization> specializations = new();

		while (await reader.ReadAsync())
		{
			specializations.Add(new Specialization(
				reader.GetGuid(reader.GetOrdinal("Id")),
				reader.GetGuid(reader.GetOrdinal("CompanyId")),
				reader.GetString(reader.GetOrdinal("Name")),
				reader.GetString(reader.GetOrdinal("Description"))
			));
		}

		return specializations;
	}
}