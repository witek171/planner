using Microsoft.Data.SqlClient;
using System.Data;

namespace Schedule.Infrastructure.Repositories.Common;

public abstract class BaseRepository
{
    protected readonly SqlConnection _connection;

    protected BaseRepository(SqlConnection connection)
    {
        _connection = connection;
    }

    protected void AddParameter(IDbCommand command, string name, object value)
    {
        var param = command.CreateParameter();
        param.ParameterName = name;
        param.Value = value ?? DBNull.Value;
        command.Parameters.Add(param);
    }
}
