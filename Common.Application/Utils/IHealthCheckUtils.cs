namespace Common.Application.Utils;

public interface IHealthCheckUtils
{
    string GetConnectionString();
    string GetAssemblyVersion();
    string MaskConnectionString(string connectionString);
}