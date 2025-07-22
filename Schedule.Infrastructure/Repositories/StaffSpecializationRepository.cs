using Schedule.Contracts.Dtos;
using Schedule.Infrastructure.Repositories.Common;
using System.Data;

namespace Schedule.Infrastructure.Repositories;

public class StaffSpecializationRepository : BaseRepository, IStaffSpecializationRepository
{
    public StaffSpecializationRepository(IDbConnection connection) : base(connection) { }

    public void Create(StaffSpecializationDto dto)
    {
        var query = @"INSERT INTO StaffSpecializations (Id, ReceptionId, StaffId, SpecializationId)
                      VALUES (@Id, @ReceptionId, @StaffId, @SpecializationId)";

        using var cmd = _connection.CreateCommand();
        cmd.CommandText = query;

        AddParameter(cmd, "@Id", dto.Id);
        AddParameter(cmd, "@ReceptionId", dto.ReceptionId);
        AddParameter(cmd, "@StaffId", dto.StaffId);
        AddParameter(cmd, "@SpecializationId", dto.SpecializationId);

        _connection.Open();
        cmd.ExecuteNonQuery();
        _connection.Close();
    }
    public void Delete(Guid id)
    {
        var query = @"DELETE FROM StaffSpecializations  
                     WHERE Id = @Id";

        using var cmd = _connection.CreateCommand();
        cmd.CommandText = query;
        AddParameter(cmd, "@Id", id);

        _connection.Open();
        cmd.ExecuteNonQuery();
        _connection.Close();
    }

    public IEnumerable<StaffSpecializationDto> GetByStaffId(Guid staffId)
    {
        var query = @"SELECT Id, ReceptionId, StaffId, SpecializationId  
                     FROM StaffSpecializations  
                     WHERE StaffId = @StaffId";

        using var cmd = _connection.CreateCommand();
        cmd.CommandText = query;
        AddParameter(cmd, "@StaffId", staffId);

        _connection.Open();
        using var reader = cmd.ExecuteReader();
        var results = new List<StaffSpecializationDto>();
        while (reader.Read())
        {
            results.Add(new StaffSpecializationDto
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                ReceptionId = reader.GetGuid(reader.GetOrdinal("ReceptionId")),
                StaffId = reader.GetGuid(reader.GetOrdinal("StaffId")),
                SpecializationId = reader.GetGuid(reader.GetOrdinal("SpecializationId"))
            });
        }
        _connection.Close();
        return results;
    }

}