using Microsoft.Data.SqlClient;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models;
using Schedule.Domain.Models.Enums;

namespace Schedule.Infrastructure.Repositories;

public class StaffMemberRepository : IStaffMemberRepository
{
	private readonly string _connectionString;

	public StaffMemberRepository(string connectionString)
	{
		_connectionString = connectionString;
	}

	public async Task<Guid> CreateAsync(StaffMember staffMember)
	{
		const string sql = @"
			INSERT INTO Staff (CompanyId, Role, Email, Password, FirstName, LastName, Phone)
			VALUES (@CompanyId, @Role, @Email, @Password, @FirstName, @LastName, @Phone)
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", staffMember.CompanyId);
		command.Parameters.AddWithValue("@Role", staffMember.Role.ToString());
		command.Parameters.AddWithValue("@Email", staffMember.Email);
		command.Parameters.AddWithValue("@Password", staffMember.Password);
		command.Parameters.AddWithValue("@FirstName", staffMember.FirstName);
		command.Parameters.AddWithValue("@LastName", staffMember.LastName);
		command.Parameters.AddWithValue("@Phone", staffMember.Phone);

		object result = (await command.ExecuteScalarAsync())!;
		return (Guid)result;
	}

	public async Task<bool> PutAsync(StaffMember staffMember)
	{
		const string sql = @"
			UPDATE Staff SET
			Email = @Email,
			Password = @Password,
			FirstName = @FirstName,
			LastName = @LastName,
			Phone = @Phone
			WHERE Id = @Id AND CompanyId = @CompanyId
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", staffMember.Id);
		command.Parameters.AddWithValue("@CompanyId", staffMember.CompanyId);
		command.Parameters.AddWithValue("@Role", staffMember.Role);
		command.Parameters.AddWithValue("@Email", staffMember.Email);
		command.Parameters.AddWithValue("@Password", staffMember.Password);
		command.Parameters.AddWithValue("@FirstName", staffMember.FirstName);
		command.Parameters.AddWithValue("@LastName", staffMember.LastName);
		command.Parameters.AddWithValue("@Phone", staffMember.Phone);

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}

	public async Task<bool> DeleteByIdAsync(
		Guid companyId,
		Guid staffMemberId)
	{
		const string sql = @"
			DELETE FROM Staff 
			WHERE CompanyId = @CompanyId AND Id = @Id
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@Id", staffMemberId);

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}

	public async Task<List<StaffMember>> GetAllAsync(Guid companyId)
	{
		const string sql = @"
			SELECT Id, CompanyId, Role, Email, Password, FirstName, LastName, Phone, CreatedAt, IsDeleted
			FROM Staff
			WHERE CompanyId = @CompanyId AND IsDeleted = 0
			ORDER BY CreatedAt DESC
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		List<StaffMember> staff = new();

		while (await reader.ReadAsync())
			staff.Add(new StaffMember(
				reader.GetGuid(reader.GetOrdinal("Id")),
				reader.GetGuid(reader.GetOrdinal("CompanyId")),
				Enum.Parse<StaffRole>(reader.GetString(reader.GetOrdinal("Role"))),
				reader.GetString(reader.GetOrdinal("Email")),
				reader.GetString(reader.GetOrdinal("Password")),
				reader.GetString(reader.GetOrdinal("FirstName")),
				reader.GetString(reader.GetOrdinal("LastName")),
				reader.GetString(reader.GetOrdinal("Phone")),
				reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
				reader.GetBoolean(reader.GetOrdinal("IsDeleted"))
			));

		return staff;
	}

	public async Task<StaffMember?> GetByIdAsync(
		Guid staffMemberId,
		Guid companyId)
	{
		const string sql = @"
			SELECT Id, CompanyId, Role, Email, Password, FirstName, LastName, Phone, CreatedAt, IsDeleted
			FROM Staff 
			WHERE Id = @Id AND CompanyId = @CompanyId AND IsDeleted = 0
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", staffMemberId);
		command.Parameters.AddWithValue("@CompanyId", companyId);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		if (!await reader.ReadAsync())
			return null;

		return new StaffMember(
			reader.GetGuid(reader.GetOrdinal("Id")),
			reader.GetGuid(reader.GetOrdinal("CompanyId")),
			Enum.Parse<StaffRole>(reader.GetString(reader.GetOrdinal("Role"))),
			reader.GetString(reader.GetOrdinal("Email")),
			reader.GetString(reader.GetOrdinal("Password")),
			reader.GetString(reader.GetOrdinal("FirstName")),
			reader.GetString(reader.GetOrdinal("LastName")),
			reader.GetString(reader.GetOrdinal("Phone")),
			reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
			reader.GetBoolean(reader.GetOrdinal("IsDeleted"))
		);
	}

	public async Task<bool> HasRelatedRecordsAsync(
		Guid staffMemberId,
		Guid companyId)
	{
		const string sql = @"
			SELECT CASE 
			WHEN EXISTS (
				SELECT 1 FROM EventScheduleStaff WHERE StaffMemberId = @StaffMemberId AND CompanyId = @CompanyId
			) 
			OR EXISTS (
				SELECT 1 FROM StaffAvailability WHERE StaffMemberId = @StaffMemberId AND CompanyId = @CompanyId  
			)
			OR EXISTS (
				SELECT 1 FROM StaffSpecializations WHERE StaffMemberId = @StaffMemberId AND CompanyId = @CompanyId
			)
			THEN CAST(1 AS BIT)
			ELSE CAST(0 AS BIT)
			END
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@StaffMemberId", staffMemberId);
		command.Parameters.AddWithValue("@CompanyId", companyId);

		object result = (await command.ExecuteScalarAsync())!;
		return (bool)result;
	}
}