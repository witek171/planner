using Microsoft.Data.SqlClient;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models.StaffRelated;
using Schedule.Infrastructure.Repositories.Common;
using Schedule.Infrastructure.Services;

namespace Schedule.Infrastructure.Repositories;

public class StaffRepository : BaseRepository, IStaffRepository
{
	public StaffRepository() : base(new SqlConnection(EnvironmentService.SqlConnectionString)) { }

	public async Task<List<Staff>> GetAllAsync()
	{
		List<Staff> result = new List<Staff>();

		using SqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM Staff";

		await _connection.OpenAsync();
		using SqlDataReader reader = await command.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			result.Add(new Staff
			{
				Id = reader.GetGuid(0),
				CompanyId = reader.GetGuid(1),
				Role = reader.GetString(2),
				Email = reader.GetString(3),
				Password = reader.GetString(4),
				FirstName = reader.GetString(5),
				LastName = reader.GetString(6),
				Phone = reader.GetString(7),
			});
		}
		await _connection.CloseAsync();

		return result;
	}

	public async Task<Staff?> GetByIdAsync(Guid id)
	{
		using SqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM Staff WHERE Id = @Id";
		AddParameter(command, "@Id", id);

		await _connection.OpenAsync();
		using SqlDataReader reader = await command.ExecuteReaderAsync();
		if (await reader.ReadAsync())
		{
			Staff staff = new Staff
			{
				Id = reader.GetGuid(0),
				CompanyId = reader.GetGuid(1),
				Role = reader.GetString(2),
				Email = reader.GetString(3),
				Password = reader.GetString(4),
				FirstName = reader.GetString(5),
				LastName = reader.GetString(6),
				Phone = reader.GetString(7),
			};
			await _connection.CloseAsync();
			return staff;
		}
		await _connection.CloseAsync();
		return null;
	}

	public async Task<Guid> CreateAsync(Staff staff)
	{
		using SqlCommand command = _connection.CreateCommand();
		command.CommandText = @"
			INSERT INTO Staff (Id, CompanyId, Role, Email, Password, FirstName, LastName, Phone)
			VALUES (@Id, @CompanyId, @Role, @Email, @Password, @FirstName, @LastName, @Phone)";

		staff.Id = Guid.NewGuid();

		AddParameter(command, "@Id", staff.Id);
		AddParameter(command, "@CompanyId", staff.CompanyId);
		AddParameter(command, "@Role", staff.Role);
		AddParameter(command, "@Email", staff.Email);
		AddParameter(command, "@Password", staff.Password);
		AddParameter(command, "@FirstName", staff.FirstName);
		AddParameter(command, "@LastName", staff.LastName);
		AddParameter(command, "@Phone", staff.Phone);

		await _connection.OpenAsync();
		await command.ExecuteNonQueryAsync();
		await _connection.CloseAsync();

		return staff.Id;
	}

	public async Task UpdateAsync(Staff staff)
	{
		using SqlCommand command = _connection.CreateCommand();
		command.CommandText = @"
			UPDATE Staff SET
				CompanyId = @CompanyId,
				Role = @Role,
				Email = @Email,
				Password = @Password,
				FirstName = @FirstName,
				LastName = @LastName,
				Phone = @Phone
			WHERE Id = @Id";

		AddParameter(command, "@Id", staff.Id);
		AddParameter(command, "@CompanyId", staff.CompanyId);
		AddParameter(command, "@Role", staff.Role);
		AddParameter(command, "@Email", staff.Email);
		AddParameter(command, "@Password", staff.Password);
		AddParameter(command, "@FirstName", staff.FirstName);
		AddParameter(command, "@LastName", staff.LastName);
		AddParameter(command, "@Phone", staff.Phone);

		await _connection.OpenAsync();
		await command.ExecuteNonQueryAsync();
		await _connection.CloseAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		using SqlCommand command = _connection.CreateCommand();
		command.CommandText = "DELETE FROM Staff WHERE Id = @Id";
		AddParameter(command, "@Id", id);

		await _connection.OpenAsync();
		await command.ExecuteNonQueryAsync();
		await _connection.CloseAsync();
	}
}
