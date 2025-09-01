using Microsoft.Data.SqlClient;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models;
using Schedule.Infrastructure.Utils;

namespace Schedule.Infrastructure.Repositories;

public class SpecializationRepository : ISpecializationRepository
{
	private readonly string _connectionString;
	private readonly DbMapper _dbMapper;

	public SpecializationRepository(
		string connectionString,
		DbMapper dbMapper)
	{
		_connectionString = connectionString;
		_dbMapper = dbMapper;
	}

	public async Task<List<Specialization>> GetAllAsync(Guid companyId)
	{
		const string sql = @"SELECT Id, CompanyId, Name, Description FROM Specializations WHERE CompanyId = @CompanyId";
		await using SqlConnection? connection = new SqlConnection(_connectionString);
		await connection.OpenAsync();
		await using SqlCommand? command = new SqlCommand(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		SqlDataReader reader = await command.ExecuteReaderAsync();
		List<Specialization> result = new List<Specialization>();
		while (await reader.ReadAsync())
			result.Add(_dbMapper.MapSpecialization(reader));

		return result;
	}

	public async Task<Specialization?> GetByIdAsync(Guid id, Guid companyId)
	{
		const string sql =
			@"SELECT Id, CompanyId, Name, Description FROM Specializations WHERE Id = @Id AND CompanyId = @CompanyId";
		await using SqlConnection? connection = new SqlConnection(_connectionString);
		await connection.OpenAsync();
		await using SqlCommand? command = new SqlCommand(sql, connection);
		command.Parameters.AddWithValue("@Id", id);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		SqlDataReader reader = await command.ExecuteReaderAsync();
		if (await reader.ReadAsync())
			return _dbMapper.MapSpecialization(reader);

		return null;
	}

	public async Task<Guid> CreateAsync(Specialization specialization)
	{
		const string sql = @"
		INSERT INTO Specializations (CompanyId, Name, Description)
		OUTPUT INSERTED.Id
		VALUES (@CompanyId, @Name, @Description);
		";

		await using SqlConnection? connection = new SqlConnection(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand? command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", specialization.CompanyId);
		command.Parameters.AddWithValue("@Name", specialization.Name);
		command.Parameters.AddWithValue("@Description", specialization.Description);

		object result = (await command.ExecuteScalarAsync())!;
		return (Guid)result;
	}

	public async Task<bool> UpdateAsync(Specialization specialization)
	{
		const string sql =
			@"UPDATE Specializations SET Name = @Name, Description = @Description WHERE Id = @Id AND CompanyId = @CompanyId";
		await using SqlConnection? connection = new SqlConnection(_connectionString);
		await connection.OpenAsync();
		await using SqlCommand? command = new SqlCommand(sql, connection);
		command.Parameters.AddWithValue("@Id", specialization.Id);
		command.Parameters.AddWithValue("@CompanyId", specialization.CompanyId);
		command.Parameters.AddWithValue("@Name", specialization.Name);
		command.Parameters.AddWithValue("@Description", specialization.Description);
		Int32 affected = await command.ExecuteNonQueryAsync();
		return affected > 0;
	}

	public async Task<bool> DeleteAsync(Guid id, Guid companyId)
	{
		const string sql = @"DELETE FROM Specializations WHERE Id = @Id AND CompanyId = @CompanyId";
		await using SqlConnection? connection = new SqlConnection(_connectionString);
		await connection.OpenAsync();
		await using SqlCommand? command = new SqlCommand(sql, connection);
		command.Parameters.AddWithValue("@Id", id);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		Int32 affected = await command.ExecuteNonQueryAsync();
		return affected > 0;
	}
}