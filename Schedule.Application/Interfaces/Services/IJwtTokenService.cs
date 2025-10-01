using Schedule.Domain.Models.Enums;

namespace Schedule.Application.Interfaces.Services;

public interface IJwtTokenService
{
	string GenerateToken(Guid userId, StaffRole role);
}
