using System.Data;
using Microsoft.Data.SqlClient;
using Schedule.Domain.Models.StaffRelated;
using Schedule.Infrastructure.Repositories.Common;
using Schedule.Infrastructure.Services;

namespace Schedule.Infrastructure.Repositories;

public class StaffSpecializationRepository : BaseRepository
{
	public StaffSpecializationRepository() : base(new SqlConnection(EnvironmentService.SqlConnectionString)) { }

    public List<StaffSpecialization> GetByStaffId(Guid staffId)
	{
		var result = new List<StaffSpecialization>();

		using var command = _connection.CreateCommand();
		command.CommandText = """
			SELECT Id, ReceptionId, StaffId, SpecializationId
			FROM StaffSpecializations
			WHERE StaffId = @StaffId
		""";
		AddParameter(command, "@StaffId", staffId);

		using var reader = command.ExecuteReader();
		while (reader.Read())
		{
			result.Add(new StaffSpecialization
			{
				Id = reader.GetGuid(0),
				ReceptionId = reader.GetGuid(1),
				StaffId = reader.GetGuid(2),
				SpecializationId = reader.GetGuid(3)
			});
		}

		return result;
	}

	public Guid Create(StaffSpecialization specialization)
	{
		using var command = _connection.CreateCommand();
		command.CommandText = """
			INSERT INTO StaffSpecializations (Id, ReceptionId, StaffId, SpecializationId)
			VALUES (@Id, @ReceptionId, @StaffId, @SpecializationId)
		""";

		specialization.Id = Guid.NewGuid();

		AddParameter(command, "@Id", specialization.Id);
		AddParameter(command, "@ReceptionId", specialization.ReceptionId);
		AddParameter(command, "@StaffId", specialization.StaffId);
		AddParameter(command, "@SpecializationId", specialization.SpecializationId);

		command.ExecuteNonQuery();
		return specialization.Id;
	}
}
