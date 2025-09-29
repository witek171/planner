using Schedule.Application.Interfaces.Services;
using Schedule.Application.Interfaces.Utils;
using Schedule.Domain.Exceptions.Login;
using Schedule.Domain.Models;

namespace Schedule.Application.Services;

public class AuthService : IAuthService
{
	protected IJwtTokenService _jwtTokenService;
	protected IStaffMemberService _staffMemberService;
	protected IPasswordHasher _passwordHasher;

	public AuthService(IJwtTokenService jwtTokenService, IStaffMemberService staffMemberService, IPasswordHasher passwordHasher)
	{
		_jwtTokenService = jwtTokenService;
		_staffMemberService = staffMemberService;
		_passwordHasher = passwordHasher;
	}


	public async Task<string> LoginAsync(string email, string password)
	{
		StaffMember? staffMember = await _staffMemberService.GetByEmailAsync(email);

		Boolean isPasswordValid = _passwordHasher.Verify(password, staffMember.Password);
		if (!isPasswordValid)
			throw new InvalidCredentialsException();

		String token = _jwtTokenService.GenerateToken(staffMember.Id);
		return token;
	}

	public async Task<Guid> RegisterAsync(StaffMember staffMember)
	{
		staffMember.Normalize();
		string hashed = _passwordHasher.Hash(staffMember.Password);
		staffMember.SetPassword(hashed);
		return await _staffMemberService.CreateAsync(staffMember);
	}
}
