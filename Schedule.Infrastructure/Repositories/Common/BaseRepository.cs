using System.Data;

namespace Schedule.Infrastructure.Repositories.Common;

public abstract class BaseRepository
{
    protected readonly IDbConnection _connection;

    protected BaseRepository(IDbConnection connection)
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
