namespace Schedule.Application.Interfaces.Utils;

public interface IHealthCheckUtils
{
	string GetConnectionString();
	string GetAssemblyVersion();
	string MaskConnectionString(string connectionString);
}