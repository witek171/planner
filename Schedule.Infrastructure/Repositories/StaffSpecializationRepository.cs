using System.Data;
using Microsoft.Data.SqlClient;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models.StaffRelated;
using Schedule.Infrastructure.Repositories.Common;
using Schedule.Infrastructure.Services;

namespace Schedule.Infrastructure.Repositories;

public class StaffSpecializationRepository : BaseRepository, IStaffSpecializationRepository
{
	public StaffSpecializationRepository() : base(new SqlConnection(EnvironmentService.SqlConnectionString)) { }

	public async Task<List<StaffSpecialization>> GetByStaffIdAsync(Guid staffId)
	{
        List<StaffSpecialization> result = new List<StaffSpecialization>();

		using SqlCommand command = _connection.CreateCommand();
		command.CommandText = """
			SELECT Id, ReceptionId, StaffId, SpecializationId
			FROM StaffSpecializations
			WHERE StaffId = @StaffId
		""";
		AddParameter(command, "@StaffId", staffId);

		await _connection.OpenAsync();
		using SqlDataReader reader = await command.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			result.Add(new StaffSpecialization
			{
				Id = reader.GetGuid(0),
				ReceptionId = reader.GetGuid(1),
				StaffId = reader.GetGuid(2),
				SpecializationId = reader.GetGuid(3)
			});
		}
		await _connection.CloseAsync();

		return result;
	}

	public async Task<Guid> CreateAsync(StaffSpecialization specialization)
	{
		using SqlCommand command = _connection.CreateCommand();
		command.CommandText = """
			INSERT INTO StaffSpecializations (Id, ReceptionId, StaffId, SpecializationId)
			VALUES (@Id, @ReceptionId, @StaffId, @SpecializationId)
		""";

		specialization.Id = Guid.NewGuid();

		AddParameter(command, "@Id", specialization.Id);
		AddParameter(command, "@ReceptionId", specialization.ReceptionId);
		AddParameter(command, "@StaffId", specialization.StaffId);
		AddParameter(command, "@SpecializationId", specialization.SpecializationId);

		await _connection.OpenAsync();
		await command.ExecuteNonQueryAsync();
		await _connection.CloseAsync();

		return specialization.Id;
	}

	public async Task DeleteAsync(Guid id)
	{
		using SqlCommand command = _connection.CreateCommand();
		command.CommandText = "DELETE FROM StaffSpecializations WHERE Id = @Id";
		AddParameter(command, "@Id", id);

		await _connection.OpenAsync();
		await command.ExecuteNonQueryAsync();
		await _connection.CloseAsync();
	}
}
