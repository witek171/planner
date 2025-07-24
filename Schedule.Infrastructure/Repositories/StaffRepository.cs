using System.Data;
using Schedule.Contracts.Dtos;
using Schedule.Infrastructure.Repositories.Common;

namespace Schedule.Infrastructure.Repositories;

public class StaffRepository : BaseRepository, IStaffRepository
{
	public StaffRepository(IDbConnection connection) : base(connection) { }

	public IEnumerable<StaffDto> GetAll()
	{
		var result = new List<StaffDto>();
		var query = "SELECT Id, ReceptionId, Role, Email, PasswordHash, FirstName, LastName, Phone FROM Staff";

		using var command = _connection.CreateCommand();
		command.CommandText = query;

		_connection.Open();
		using var reader = command.ExecuteReader();
		while (reader.Read())
		{
			result.Add(new StaffDto
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

	public StaffDto? GetById(Guid id)
	{
		var query = "SELECT Id, ReceptionId, Role, Email, PasswordHash, FirstName, LastName, Phone FROM Staff WHERE Id = @Id";

		using var command = _connection.CreateCommand();
		command.CommandText = query;
		AddParameter(command, "@Id", id);

		_connection.Open();
		using var reader = command.ExecuteReader();
		if (reader.Read())
		{
			var staff = new StaffDto
			{
				Id = reader.GetGuid(0),
				ReceptionId = reader.GetGuid(1),
				Role = reader.GetString(2),
				Email = reader.GetString(3),
				PasswordHash = reader.GetString(4),
				FirstName = reader.GetString(5),
				LastName = reader.GetString(6),
				Phone = reader.GetString(7)
			};
			_connection.Close();
			return staff;
		}
		_connection.Close();
		return null;
	}

	public void Create(StaffDto dto)
	{
		var query = @"INSERT INTO Staff (Id, ReceptionId, Role, Email, PasswordHash, FirstName, LastName, Phone)
					  VALUES (@Id, @ReceptionId, @Role, @Email, @PasswordHash, @FirstName, @LastName, @Phone)";

		using var command = _connection.CreateCommand();
		command.CommandText = query;

		AddParameter(command, "@Id", dto.Id);
		AddParameter(command, "@ReceptionId", dto.ReceptionId);
		AddParameter(command, "@Role", dto.Role);
		AddParameter(command, "@Email", dto.Email);
		AddParameter(command, "@PasswordHash", dto.PasswordHash);
		AddParameter(command, "@FirstName", dto.FirstName);
		AddParameter(command, "@LastName", dto.LastName);
		AddParameter(command, "@Phone", dto.Phone);

		_connection.Open();
		command.ExecuteNonQuery();
		_connection.Close();
	}

	public void Update(StaffDto dto)
	{
		var query = @"UPDATE Staff 
					  SET ReceptionId = @ReceptionId, Role = @Role, Email = @Email, 
						  PasswordHash = @PasswordHash, FirstName = @FirstName, LastName = @LastName, Phone = @Phone
					  WHERE Id = @Id";

		using var command = _connection.CreateCommand();
		command.CommandText = query;

		AddParameter(command, "@Id", dto.Id);
		AddParameter(command, "@ReceptionId", dto.ReceptionId);
		AddParameter(command, "@Role", dto.Role);
		AddParameter(command, "@Email", dto.Email);
		AddParameter(command, "@PasswordHash", dto.PasswordHash);
		AddParameter(command, "@FirstName", dto.FirstName);
		AddParameter(command, "@LastName", dto.LastName);
		AddParameter(command, "@Phone", dto.Phone);

		_connection.Open();
		command.ExecuteNonQuery();
		_connection.Close();
	}

	public void Delete(Guid id)
	{
		var query = "DELETE FROM Staff WHERE Id = @Id";

		using var command = _connection.CreateCommand();
		command.CommandText = query;
		AddParameter(command, "@Id", id);

		_connection.Open();
		command.ExecuteNonQuery();
		_connection.Close();
	}
}
