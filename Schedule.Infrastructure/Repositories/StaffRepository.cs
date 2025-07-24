using Microsoft.Data.SqlClient;
using Schedule.Domain.Models;
using Schedule.Infrastructure.Repositories.Common;
using System.Data;

namespace Schedule.Infrastructure.Repositories;

public class StaffRepository : BaseRepository, IStaffRepository
{
    public StaffRepository(SqlConnection connection) : base(connection) { }

    public async Task<Guid> CreateAsync(Staff staff)
    {
        var id = Guid.NewGuid();

        using var command = _connection.CreateCommand();
        command.CommandText = @"
                INSERT INTO Staff (Id, ReceptionId, Role, Email, PasswordHash, FirstName, LastName, Phone)
                VALUES (@Id, @ReceptionId, @Role, @Email, @PasswordHash, @FirstName, @LastName, @Phone);";

        AddParameter(command, "@Id", id);
        AddParameter(command, "@ReceptionId", staff.ReceptionId);
        AddParameter(command, "@Role", staff.Role);
        AddParameter(command, "@Email", staff.Email);
        AddParameter(command, "@PasswordHash", staff.PasswordHash);
        AddParameter(command, "@FirstName", staff.FirstName);
        AddParameter(command, "@LastName", staff.LastName);
        AddParameter(command, "@Phone", staff.Phone);

        await _connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
        await _connection.CloseAsync();

        return id;
    }

    public async Task UpdateAsync(Staff staff)
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

        await _connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
        await _connection.CloseAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "DELETE FROM Staff WHERE Id = @Id";
        AddParameter(command, "@Id", id);

        await _connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
        await _connection.CloseAsync();
    }

    public async Task<Staff?> GetByIdAsync(Guid id)
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM Staff WHERE Id = @Id";
        AddParameter(command, "@Id", id);

        await _connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
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
                Phone = reader.GetString(7)
            };

            await _connection.CloseAsync();
            return staff;
        }

        await _connection.CloseAsync();
        return null;
    }

    public async Task<List<Staff>> GetAllAsync()
    {
        var result = new List<Staff>();

        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM Staff";

        await _connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
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
                Phone = reader.GetString(7)
            };

            result.Add(staff);
        }

        await _connection.CloseAsync();
        return result;
    }
}
