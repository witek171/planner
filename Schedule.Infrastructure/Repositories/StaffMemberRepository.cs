using Microsoft.Data.SqlClient;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models;
using Schedule.Domain.Models.Enums;

namespace Schedule.Infrastructure.Repositories;

public class StaffMemberRepository : IStaffMemberRepository
{
	private readonly string _connectionString;

	public StaffMemberRepository(string connectionString)
	{
		_connectionString = connectionString;
	}

	public async Task<Guid> CreateAsync(StaffMember staffMember)
	{
		const string sql = @"
			INSERT INTO Staff (CompanyId, Role, Email, Password, FirstName, LastName, Phone)
			OUTPUT INSERTED.Id
			VALUES (@CompanyId, @Role, @Email, @Password, @FirstName, @LastName, @Phone);
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", staffMember.CompanyId);
		command.Parameters.AddWithValue("@Role", staffMember.Role.ToString());
		command.Parameters.AddWithValue("@Email", staffMember.Email);
		command.Parameters.AddWithValue("@Password", staffMember.Password);
		command.Parameters.AddWithValue("@FirstName", staffMember.FirstName);
		command.Parameters.AddWithValue("@LastName", staffMember.LastName);
		command.Parameters.AddWithValue("@Phone", staffMember.Phone);

		object result = (await command.ExecuteScalarAsync())!;
		return (Guid)result;
	}

	public async Task<bool> PutAsync(StaffMember staffMember)
	{
		const string sql = @"
			UPDATE Staff SET
			Role = @Role,
			Email = @Email,
			Password = @Password,
			FirstName = @FirstName,
			LastName = @LastName,
			Phone = @Phone
			WHERE Id = @Id AND CompanyId = @CompanyId
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", staffMember.Id);
		command.Parameters.AddWithValue("@CompanyId", staffMember.CompanyId);
		command.Parameters.AddWithValue("@Role", staffMember.Role.ToString());
		command.Parameters.AddWithValue("@Email", staffMember.Email);
		command.Parameters.AddWithValue("@Password", staffMember.Password);
		command.Parameters.AddWithValue("@FirstName", staffMember.FirstName);
		command.Parameters.AddWithValue("@LastName", staffMember.LastName);
		command.Parameters.AddWithValue("@Phone", staffMember.Phone);

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}

	public async Task<bool> DeleteByIdAsync(
		Guid staffMemberId,
		Guid companyId)
	{
		const string sql = @"
			DELETE FROM Staff 
			WHERE CompanyId = @CompanyId AND Id = @Id
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", staffMemberId);
		command.Parameters.AddWithValue("@CompanyId", companyId);

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}

	public async Task<List<StaffMember>> GetAllAsync(Guid companyId)
	{
		const string sql = @"
			SELECT 
				s.Id, s.CompanyId, s.Role, s.Email, s.Password, 
				s.FirstName, s.LastName, s.Phone, s.CreatedAt, s.IsDeleted,
				sp.Id as SpecId, sp.Name as SpecName, sp.Description as SpecDescription
			FROM Staff s
			LEFT JOIN StaffSpecializations ss 
				ON s.Id = ss.StaffMemberId AND s.CompanyId = ss.CompanyId
			LEFT JOIN Specializations sp 
				ON ss.SpecializationId = sp.Id
			WHERE s.CompanyId = @CompanyId AND s.IsDeleted = 0
			ORDER BY s.CreatedAt DESC
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		Dictionary<Guid, StaffMember> staffMap = new();
		Dictionary<Guid, List<Specialization>> staffMemberSpecializationsMap = new();

		while (await reader.ReadAsync())
		{
			Guid staffMemberId = reader.GetGuid(reader.GetOrdinal("Id"));

			if (!staffMap.ContainsKey(staffMemberId))
			{
				staffMap[staffMemberId] = new StaffMember(
					staffMemberId,
					reader.GetGuid(reader.GetOrdinal("CompanyId")),
					Enum.Parse<StaffRole>(reader.GetString(reader.GetOrdinal("Role"))),
					reader.GetString(reader.GetOrdinal("Email")),
					reader.GetString(reader.GetOrdinal("Password")),
					reader.GetString(reader.GetOrdinal("FirstName")),
					reader.GetString(reader.GetOrdinal("LastName")),
					reader.GetString(reader.GetOrdinal("Phone")),
					reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
					reader.GetBoolean(reader.GetOrdinal("IsDeleted")),
					new List<Specialization>()
				);

				staffMemberSpecializationsMap[staffMemberId] = new List<Specialization>();
			}

			if (!reader.IsDBNull(reader.GetOrdinal("SpecId")))
			{
				staffMemberSpecializationsMap[staffMemberId].Add(new Specialization(
					reader.GetGuid(reader.GetOrdinal("SpecId")),
					companyId,
					reader.GetString(reader.GetOrdinal("SpecName")),
					reader.GetString(reader.GetOrdinal("SpecDescription"))
				));
			}
		}

		foreach (
			(Guid staffMemberId, List<Specialization> specializations)
			in staffMemberSpecializationsMap
		)
			staffMap[staffMemberId].SetSpecializations(specializations);

		return staffMap.Values.ToList();
	}

	public async Task<StaffMember?> GetByIdAsync(
		Guid staffMemberId,
		Guid companyId)
	{
		const string sql = @"
			SELECT 
				s.Id, s.CompanyId, s.Role, s.Email, s.Password, 
				s.FirstName, s.LastName, s.Phone, s.CreatedAt, s.IsDeleted,
				sp.Id as SpecId, sp.Name as SpecName, sp.Description as SpecDescription
			FROM Staff s
			LEFT JOIN StaffSpecializations ss ON s.Id = ss.StaffMemberId AND s.CompanyId = ss.CompanyId
			LEFT JOIN Specializations sp ON ss.SpecializationId = sp.Id
			WHERE s.Id = @Id AND s.CompanyId = @CompanyId AND s.IsDeleted = 0
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", staffMemberId);
		command.Parameters.AddWithValue("@CompanyId", companyId);

		await using SqlDataReader reader = await command.ExecuteReaderAsync();

		List<Specialization> specializations = new();
		StaffMember? staffMember = null;

		while (await reader.ReadAsync())
		{
			if (staffMember == null)
			{
				staffMember = new StaffMember(
					reader.GetGuid(reader.GetOrdinal("Id")),
					reader.GetGuid(reader.GetOrdinal("CompanyId")),
					Enum.Parse<StaffRole>(reader.GetString(reader.GetOrdinal("Role"))),
					reader.GetString(reader.GetOrdinal("Email")),
					reader.GetString(reader.GetOrdinal("Password")),
					reader.GetString(reader.GetOrdinal("FirstName")),
					reader.GetString(reader.GetOrdinal("LastName")),
					reader.GetString(reader.GetOrdinal("Phone")),
					reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
					reader.GetBoolean(reader.GetOrdinal("IsDeleted")),
					new List<Specialization>()
				);
			}

			if (!reader.IsDBNull(reader.GetOrdinal("SpecId")))
			{
				specializations.Add(new Specialization(
					reader.GetGuid(reader.GetOrdinal("SpecId")),
					companyId,
					reader.GetString(reader.GetOrdinal("SpecName")),
					reader.GetString(reader.GetOrdinal("SpecDescription"))
				));
			}
		}

		staffMember!.SetSpecializations(specializations);
		return staffMember;
	}

	public async Task<bool> HasRelatedRecordsAsync(
		Guid staffMemberId,
		Guid companyId)
	{
		const string sql = @"
			SELECT CASE 
			WHEN EXISTS (
				SELECT 1 FROM EventScheduleStaff WHERE StaffMemberId = @StaffMemberId AND CompanyId = @CompanyId
			) 
			OR EXISTS (
				SELECT 1 FROM StaffAvailability WHERE StaffMemberId = @StaffMemberId AND CompanyId = @CompanyId  
			)
			OR EXISTS (
				SELECT 1 FROM StaffSpecializations WHERE StaffMemberId = @StaffMemberId AND CompanyId = @CompanyId
			)
			OR EXISTS (
				SELECT 1 FROM Messages WHERE SenderId = @StaffMemberId AND CompanyId = @CompanyId
			)
			THEN 1 ELSE 0 END
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@StaffMemberId", staffMemberId);
		command.Parameters.AddWithValue("@CompanyId", companyId);

		int result = (int)(await command.ExecuteScalarAsync())!;
		return result == 1;
	}

	public async Task<bool> EmailExistsForOtherAsync(
		Guid companyId,
		Guid staffMemberId,
		string email)
	{
		const string sql = @"
			SELECT 1 
			FROM Staff
			WHERE CompanyId = @CompanyId 
			AND Email = @Email 
			AND Phone <> '(deleted)'
			AND Id <> @StaffMemberId
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@Email", email);
		command.Parameters.AddWithValue("@StaffMemberId", staffMemberId);

		object? result = await command.ExecuteScalarAsync();
		return result != null;
	}

	public async Task<bool> PhoneExistsForOtherAsync(
		Guid companyId,
		Guid staffMemberId,
		string phone)
	{
		const string sql = @"
			SELECT 1 
			FROM Staff
			WHERE CompanyId = @CompanyId 
			AND Phone = @Phone 
			AND Phone <> '(deleted)'
			AND Id <> @StaffMemberId
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@CompanyId", companyId);
		command.Parameters.AddWithValue("@Phone", phone);
		command.Parameters.AddWithValue("@StaffMemberId", staffMemberId);

		object? result = await command.ExecuteScalarAsync();
		return result != null;
	}

	public async Task<bool> UpdateSoftDeleteAsync(StaffMember staffMember)
	{
		const string sql = @"
			UPDATE Staff SET
			Email = @Email,
			Phone = @Phone,
			IsDeleted = @IsDeleted
			WHERE Id = @Id AND CompanyId = @CompanyId
		";

		await using SqlConnection connection = new(_connectionString);
		await connection.OpenAsync();

		await using SqlCommand command = new(sql, connection);
		command.Parameters.AddWithValue("@Id", staffMember.Id);
		command.Parameters.AddWithValue("@CompanyId", staffMember.CompanyId);
		command.Parameters.AddWithValue("@Email", staffMember.Email);
		command.Parameters.AddWithValue("@Phone", staffMember.Phone);
		command.Parameters.AddWithValue("@IsDeleted", staffMember.IsDeleted);

		int rowsAffected = await command.ExecuteNonQueryAsync();
		return rowsAffected > 0;
	}
}