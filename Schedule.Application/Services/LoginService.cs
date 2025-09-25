using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Application.Interfaces.Utils;
using Schedule.Domain.Exceptions.Login;
using Schedule.Domain.Models;

namespace Schedule.Application.Services;

public class LoginService : ILoginService
{
	protected IJwtTokenService _jwtTokenService;
	protected IStaffMemberRepository _staffMemberRepository;
	protected IPasswordHasher _passwordHasher;

	public LoginService(IJwtTokenService jwtTokenService, IStaffMemberRepository staffMemberRepository, IPasswordHasher passwordHasher)
	{
		_jwtTokenService = jwtTokenService;
		_staffMemberRepository = staffMemberRepository;
		_passwordHasher = passwordHasher;
	}


	public async Task<string> LoginAsync(Guid company_id, string email, string password)
	{
		StaffMember? staffMember = await _staffMemberRepository.GetByEmailAsync(email, company_id);

		Boolean isPasswordValid = _passwordHasher.Verify(password, staffMember.Password);
		if (!isPasswordValid)
			throw new InvalidCredentialsException();

		String token = _jwtTokenService.GenerateToken(staffMember.Id);
		return token;
	}
}
