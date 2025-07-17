using Schedule.Contracts.Dtos;
using System.Data;

namespace Schedule.Infrastructure.Repositories;

public class StaffProfileRepository : IStaffProfileRepository
{
    private readonly IDbConnection _connection;

    public StaffProfileRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public IEnumerable<StaffProfileDto> GetAll()
    {
        var result = new List<StaffProfileDto>();
        var query = "SELECT Id, ReceptionId, UserId FROM StaffProfiles";

        using var command = _connection.CreateCommand();
        command.CommandText = query;

        _connection.Open();
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            result.Add(new StaffProfileDto
            {
                Id = reader.GetGuid(0),
                ReceptionId = reader.GetGuid(1),
                UserId = reader.GetGuid(2)
            });
        }

        _connection.Close();
        return result;
    }

    public StaffProfileDto? GetById(Guid id)
    {
        var query = "SELECT Id, ReceptionId, UserId FROM StaffProfiles WHERE Id = @Id";

        using var command = _connection.CreateCommand();
        command.CommandText = query;
        AddParameter(command, "@Id", id);

        _connection.Open();
        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            var profile = new StaffProfileDto
            {
                Id = reader.GetGuid(0),
                ReceptionId = reader.GetGuid(1),
                UserId = reader.GetGuid(2)
            };
            _connection.Close();
            return profile;
        }

        _connection.Close();
        return null;
    }

    public void Create(StaffProfileDto profile)
    {
        var query = "INSERT INTO StaffProfiles (Id, ReceptionId, UserId) VALUES (@Id, @ReceptionId, @UserId)";

        using var command = _connection.CreateCommand();
        command.CommandText = query;

        AddParameter(command, "@Id", profile.Id);
        AddParameter(command, "@ReceptionId", profile.ReceptionId);
        AddParameter(command, "@UserId", profile.UserId);

        _connection.Open();
        command.ExecuteNonQuery();
        _connection.Close();
    }

    public void Update(StaffProfileDto profile)
    {
        var query = "UPDATE StaffProfiles SET ReceptionId = @ReceptionId, UserId = @UserId WHERE Id = @Id";

        using var command = _connection.CreateCommand();
        command.CommandText = query;

        AddParameter(command, "@Id", profile.Id);
        AddParameter(command, "@ReceptionId", profile.ReceptionId);
        AddParameter(command, "@UserId", profile.UserId);

        _connection.Open();
        command.ExecuteNonQuery();
        _connection.Close();
    }

    public void Delete(Guid id)
    {
        var query = "DELETE FROM StaffProfiles WHERE Id = @Id";

        using var command = _connection.CreateCommand();
        command.CommandText = query;

        AddParameter(command, "@Id", id);

        _connection.Open();
        command.ExecuteNonQuery();
        _connection.Close();
    }

    private void AddParameter(IDbCommand command, string name, object value)
    {
        var param = command.CreateParameter();
        param.ParameterName = name;
        param.Value = value;
        command.Parameters.Add(param);
    }
}