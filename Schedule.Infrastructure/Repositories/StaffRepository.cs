using Microsoft.Data.SqlClient;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models.Enums;
using Schedule.Domain.Models.StaffRelated;

namespace Schedule.Infrastructure.Repositories;

public class StaffRepository : IStaffRepository
{
	private readonly string _connectionString;

	public StaffRepository(string connectionString)
	{
		_connectionString = connectionString;
	}

	public async Task<Guid> CreateAsync(Staff staff)
	{
		const string sql = @"
			INSERT INTO Staff (CompanyId, Role, Email, Password, FirstName, LastName, Phone)
			VALUES (@CompanyId, @Role, @Email, @Password, @FirstName, @LastName, @Phone)
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", staff.CompanyId);
		command.Parameters.AddWithValue("@Role", staff.Role.ToString());
		command.Parameters.AddWithValue("@Email", staff.Email);
		command.Parameters.AddWithValue("@Password", staff.Password);
		command.Parameters.AddWithValue("@FirstName", staff.FirstName);
		command.Parameters.AddWithValue("@LastName", staff.LastName);
		command.Parameters.AddWithValue("@Phone", staff.Phone);

		object result = (await command.ExecuteScalarAsync())!;
		return (Guid)result;
	}

	public async Task<bool> PutAsync(Staff staff)
	{
		const string sql = @"
			UPDATE Staff SET
			Email = @Email,
			Password = @Password,
			FirstName = @FirstName,
			LastName = @LastName,
			Phone = @Phone
			WHERE Id = @Id
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", staff.Id);
		command.Parameters.AddWithValue("@CompanyId", staff.CompanyId);
		command.Parameters.AddWithValue("@Role", staff.Role);
		command.Parameters.AddWithValue("@Email", staff.Email);
		command.Parameters.AddWithValue("@Password", staff.Password);
		command.Parameters.AddWithValue("@FirstName", staff.FirstName);
		command.Parameters.AddWithValue("@LastName", staff.LastName);
		command.Parameters.AddWithValue("@Phone", staff.Phone);

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}

	public async Task<bool> DeleteByIdAsync(
		Guid companyId,
		Guid staffId)
	{
		const string sql = @"
			DELETE FROM Staff 
			WHERE CompanyId = @CompanyId AND Id = @Id
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@Id", staffId);

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}

	public async Task<List<Staff>> GetAllAsync()
	{
		const string sql = @"
			SELECT Id, CompanyId, Role, Email, Password, FirstName, LastName, Phone, CreatedAt
			FROM Staff
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		List<Staff> staves = new();

		while (await reader.ReadAsync())
			staves.Add(new Staff(
				reader.GetGuid(reader.GetOrdinal("Id")),
				reader.GetGuid(reader.GetOrdinal("CompanyId")),
				Enum.Parse<StaffRole>(reader.GetString(reader.GetOrdinal("Role"))),
				reader.GetString(reader.GetOrdinal("Email")),
				reader.GetString(reader.GetOrdinal("Password")),
				reader.GetString(reader.GetOrdinal("FirstName")),
				reader.GetString(reader.GetOrdinal("LastName")),
				reader.GetString(reader.GetOrdinal("Phone")),
				reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
			));

		return staves;
	}

	public async Task<Staff?> GetByIdAsync(Guid staffId)
	{
		const string sql = @"
			SELECT Id, CompanyId, Role, Email, Password, FirstName, LastName, Phone, CreatedAt
			FROM Staff 
			WHERE Id = @Id
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", staffId);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		if (!await reader.ReadAsync())
			return null;

		return new Staff(
			reader.GetGuid(reader.GetOrdinal("Id")),
			reader.GetGuid(reader.GetOrdinal("CompanyId")),
			Enum.Parse<StaffRole>(reader.GetString(reader.GetOrdinal("Role"))),
			reader.GetString(reader.GetOrdinal("Email")),
			reader.GetString(reader.GetOrdinal("Password")),
			reader.GetString(reader.GetOrdinal("FirstName")),
			reader.GetString(reader.GetOrdinal("LastName")),
			reader.GetString(reader.GetOrdinal("Phone")),
			reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
		);
	}
}