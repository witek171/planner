namespace Schedule.Application.Interfaces.Services;

public interface IJwtTokenService
{
	string GenerateToken(Guid userId);
}
