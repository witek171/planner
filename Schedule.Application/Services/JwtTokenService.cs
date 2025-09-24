using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.JWT;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Schedule.Application.Services;

public class JwtTokenService : IJwtTokenService
{
	private readonly JwtSettings _settings;

	public JwtTokenService(IOptions<JwtSettings> settings)
	{
		_settings = settings.Value;
	}

	public string GenerateToken(Guid userId)
	{
		var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, userId.ToString())
			};


		var rsa = RSA.Create();
		rsa.ImportFromPem(File.ReadAllText("./data/private.key"));
		var creds = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);

		var token = new JwtSecurityToken(
			issuer: _settings.Issuer,
			audience: _settings.Audience,
			claims: claims,
			expires: DateTime.UtcNow.AddMinutes(_settings.ExpiresInMinutes),
			signingCredentials: creds);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
