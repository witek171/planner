using Microsoft.Data.SqlClient;
using Schedule.Domain.Models.StaffRelated;
using Schedule.Infrastructure.Repositories.Common;
using Schedule.Infrastructure.Services;

namespace Schedule.Infrastructure.Repositories;

public class StaffRepository : BaseRepository
{
	public StaffRepository() : base(new SqlConnection(EnvironmentService.SqlConnectionString)) { }

	public List<Staff> GetAll()
	{
		var result = new List<Staff>();

		using var command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM Staff";

		_connection.Open();
		using var reader = command.ExecuteReader();
		while (reader.Read())
		{
			result.Add(new Staff
			{
				Id = reader.GetGuid(0),
				ReceptionId = reader.GetGuid(1),
				Role = reader.GetString(2),
				Email = reader.GetString(3),
				PasswordHash = reader.GetString(4),
				FirstName = reader.GetString(5),
				LastName = reader.GetString(6),
				Phone = reader.GetString(7),
			});
		}
		_connection.Close();

		return result;
	}

	public Staff? GetById(Guid id)
	{
		using var command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM Staff WHERE Id = @Id";
		AddParameter(command, "@Id", id);

		_connection.Open();
		using var reader = command.ExecuteReader();
		if (reader.Read())
		{
			var staff = new Staff
			{
				Id = reader.GetGuid(0),
				ReceptionId = reader.GetGuid(1),
				Role = reader.GetString(2),
				Email = reader.GetString(3),
				PasswordHash = reader.GetString(4),
				FirstName = reader.GetString(5),
				LastName = reader.GetString(6),
				Phone = reader.GetString(7),
			};
			_connection.Close();
			return staff;
		}
		_connection.Close();
		return null;
	}

	public Guid Create(Staff staff)
	{
		using var command = _connection.CreateCommand();
		command.CommandText = @"
			INSERT INTO Staff (Id, ReceptionId, Role, Email, PasswordHash, FirstName, LastName, Phone)
			VALUES (@Id, @ReceptionId, @Role, @Email, @PasswordHash, @FirstName, @LastName, @Phone)";

		staff.Id = Guid.NewGuid();

		AddParameter(command, "@Id", staff.Id);
		AddParameter(command, "@ReceptionId", staff.ReceptionId);
		AddParameter(command, "@Role", staff.Role);
		AddParameter(command, "@Email", staff.Email);
		AddParameter(command, "@PasswordHash", staff.PasswordHash);
		AddParameter(command, "@FirstName", staff.FirstName);
		AddParameter(command, "@LastName", staff.LastName);
		AddParameter(command, "@Phone", staff.Phone);

		_connection.Open();
		command.ExecuteNonQuery();
		_connection.Close();

		return staff.Id;
	}

	public void Update(Staff staff)
	{
		using var command = _connection.CreateCommand();
		command.CommandText = @"
			UPDATE Staff SET
				ReceptionId = @ReceptionId,
				Role = @Role,
				Email = @Email,
				PasswordHash = @PasswordHash,
				FirstName = @FirstName,
				LastName = @LastName,
				Phone = @Phone
			WHERE Id = @Id";

		AddParameter(command, "@Id", staff.Id);
		AddParameter(command, "@ReceptionId", staff.ReceptionId);
		AddParameter(command, "@Role", staff.Role);
		AddParameter(command, "@Email", staff.Email);
		AddParameter(command, "@PasswordHash", staff.PasswordHash);
		AddParameter(command, "@FirstName", staff.FirstName);
		AddParameter(command, "@LastName", staff.LastName);
		AddParameter(command, "@Phone", staff.Phone);

		_connection.Open();
		command.ExecuteNonQuery();
		_connection.Close();
	}

	public void Delete(Guid id)
	{
		using var command = _connection.CreateCommand();
		command.CommandText = "DELETE FROM Staff WHERE Id = @Id";
		AddParameter(command, "@Id", id);

		_connection.Open();
		command.ExecuteNonQuery();
		_connection.Close();
	}
}
