using System.Data.Common;
using Microsoft.Data.SqlClient;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models;

namespace Schedule.Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
	private readonly string _connectionString;

	public CompanyRepository(string connectionString)
	{
		_connectionString = connectionString;
	}

	public async Task<Guid> CreateAsync(Company company)
	{
		const string sql = @"
			INSERT INTO Companies 
				(Name, TaxCode, Street, City, PostalCode, Phone, Email)
			OUTPUT INSERTED.Id
			VALUES (@Name, @TaxCode, @Street, @City, @PostalCode, @Phone, @Email);
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Name", company.Name);
		command.Parameters.AddWithValue("@TaxCode", company.TaxCode);
		command.Parameters.AddWithValue("@Street", company.Street);
		command.Parameters.AddWithValue("@City", company.City);
		command.Parameters.AddWithValue("@PostalCode", company.PostalCode);
		command.Parameters.AddWithValue("@Phone", company.Phone);
		command.Parameters.AddWithValue("@Email", company.Email);

		object result = (await command.ExecuteScalarAsync())!;
		return (Guid)result;
	}

	public async Task<bool> PutAsync(Company company)
	{
		const string sql = @"
			UPDATE Companies
			SET 
			Name = @Name,
			TaxCode = @TaxCode,
			Street = @Street,
			City = @City,
			PostalCode = @PostalCode,
			Phone = @Phone,
			Email = @Email
			WHERE Id = @Id
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", company.Id);
		command.Parameters.AddWithValue("@Name", company.Name);
		command.Parameters.AddWithValue("@TaxCode", company.TaxCode);
		command.Parameters.AddWithValue("@Street", company.Street);
		command.Parameters.AddWithValue("@City", company.City);
		command.Parameters.AddWithValue("@PostalCode", company.PostalCode);
		command.Parameters.AddWithValue("@Phone", company.Phone);
		command.Parameters.AddWithValue("@Email", company.Email);

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}

	public async Task<bool> DeleteByIdAsync(Guid companyId)
	{
		const string sql = @"
			DELETE FROM Companies WHERE Id = @CompanyId; 
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using DbTransaction transaction = await connection.BeginTransactionAsync();
		try
		{
			await using SqlCommand command = new(sql, connection, (SqlTransaction)transaction);
			command.Parameters.AddWithValue("@CompanyId", companyId);

			int rowsAffected = await command.ExecuteNonQueryAsync();
			await transaction.CommitAsync();

			return rowsAffected > 0;
		}
		catch
		{
			await transaction.RollbackAsync();
			throw;
		}
	}

	public async Task<Company?> GetByIdAsync(Guid companyId)
	{
		const string sql = @"
			SELECT 
				Id, Name, TaxCode, Street, City, PostalCode, 
				Phone, Email, IsParentNode, IsReception, CreatedAt
			FROM Companies 
			WHERE Id = @Id
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", companyId);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		if (!await reader.ReadAsync())
			return null;

		return new Company(
			reader.GetGuid(reader.GetOrdinal("Id")),
			reader.GetString(reader.GetOrdinal("Name")),
			reader.GetString(reader.GetOrdinal("TaxCode")),
			reader.GetString(reader.GetOrdinal("Street")),
			reader.GetString(reader.GetOrdinal("City")),
			reader.GetString(reader.GetOrdinal("PostalCode")),
			reader.GetString(reader.GetOrdinal("Phone")),
			reader.GetString(reader.GetOrdinal("Email")),
			reader.GetBoolean(reader.GetOrdinal("IsParentNode")),
			reader.GetBoolean(reader.GetOrdinal("IsReception")),
			reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
		);
	}

	public async Task<bool> UpdateIsParentNodeFlagAsync(Company company)
	{
		const string sql = @"
			UPDATE Companies
			SET IsParentNode = @IsParentNode 
			WHERE Id = @Id
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", company.Id);
		command.Parameters.AddWithValue("@IsParentNode", company.IsParentNode);

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}

	public async Task<bool> UpdateIsReceptionFlagAsync(Company company)
	{
		const string sql = @"
			UPDATE Companies
			SET IsReception = @IsReception 
			WHERE Id = @Id
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", company.Id);
		command.Parameters.AddWithValue("@IsReception", company.IsReception);

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}

	public async Task<bool> ExistsAsParentAsync(Guid companyId)
	{
		const string sql = @"
			SELECT CAST(
				CASE WHEN EXISTS (
					SELECT 1 FROM CompanyHierarchy 
					WHERE ParentCompanyId = @ParentCompanyId
				) THEN 1 ELSE 0 END 
			AS bit)
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@ParentCompanyId", companyId);

		object result = (await command.ExecuteScalarAsync())!;
		return (bool)result;
	}
}