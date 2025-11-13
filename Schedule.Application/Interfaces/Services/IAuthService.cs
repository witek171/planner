using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Services;

public interface IAuthService
{
	Task<string> LoginAsync(string username, string password);
	Task<Guid> RegisterAsync(StaffMember staffMember);
}
