using System.Data;
using Schedule.Contracts.Dtos;
using Schedule.Infrastructure.Repositories.Common;

namespace Schedule.Infrastructure.Repositories;

public class StaffAvailabilityRepository : BaseRepository, IStaffAvailabilityRepository
{
	public StaffAvailabilityRepository(IDbConnection connection) : base(connection) { }

	public IEnumerable<StaffAvailabilityDto> GetByStaffId(Guid staffId)
	{
		var result = new List<StaffAvailabilityDto>();
		var query = @"SELECT Id, ReceptionId, StaffId, Date, StartTime, EndTime, IsAvailable 
					  FROM StaffAvailability WHERE StaffId = @StaffId";

		using var command = _connection.CreateCommand();
		command.CommandText = query;
		AddParameter(command, "@StaffId", staffId);

		_connection.Open();
		using var reader = command.ExecuteReader();

		while (reader.Read())
		{
			result.Add(new StaffAvailabilityDto
			{
				Id = reader.GetGuid(0),
				ReceptionId = reader.GetGuid(1),
				StaffId = reader.GetGuid(2),
				Date = reader.GetDateTime(3),
				StartTime = reader.GetDateTime(4),
				EndTime = reader.GetDateTime(5),
				IsAvailable = reader.GetBoolean(6)
			});
		}

		_connection.Close();
		return result;
	}

	public void Create(StaffAvailabilityDto dto)
	{
		var query = @"INSERT INTO StaffAvailability 
					  (Id, ReceptionId, StaffId, Date, StartTime, EndTime, IsAvailable) 
					  VALUES (@Id, @ReceptionId, @StaffId, @Date, @StartTime, @EndTime, @IsAvailable)";

		using var command = _connection.CreateCommand();
		command.CommandText = query;

		AddParameter(command, "@Id", dto.Id);
		AddParameter(command, "@ReceptionId", dto.ReceptionId);
		AddParameter(command, "@StaffId", dto.StaffId);
		AddParameter(command, "@Date", dto.Date);
		AddParameter(command, "@StartTime", dto.StartTime);
		AddParameter(command, "@EndTime", dto.EndTime);
		AddParameter(command, "@IsAvailable", dto.IsAvailable);

		_connection.Open();
		command.ExecuteNonQuery();
		_connection.Close();
	}

	public void Update(StaffAvailabilityDto dto)
	{
		var query = @"UPDATE StaffAvailability
				  SET ReceptionId = @ReceptionId,
					  StaffId = @StaffId,
					  Date = @Date,
					  StartTime = @StartTime,
					  EndTime = @EndTime,
					  IsAvailable = @IsAvailable
				  WHERE Id = @Id";

		using var command = _connection.CreateCommand();
		command.CommandText = query;

		AddParameter(command, "@Id", dto.Id);
		AddParameter(command, "@ReceptionId", dto.ReceptionId);
		AddParameter(command, "@StaffId", dto.StaffId);
		AddParameter(command, "@Date", dto.Date);
		AddParameter(command, "@StartTime", dto.StartTime);
		AddParameter(command, "@EndTime", dto.EndTime);
		AddParameter(command, "@IsAvailable", dto.IsAvailable);

		_connection.Open();
		command.ExecuteNonQuery();
		_connection.Close();
	}

	public void Delete(Guid id)
	{
		var query = "DELETE FROM StaffAvailability WHERE Id = @Id";

		using var command = _connection.CreateCommand();
		command.CommandText = query;
		AddParameter(command, "@Id", id);

		_connection.Open();
		command.ExecuteNonQuery();
		_connection.Close();
	}
}
