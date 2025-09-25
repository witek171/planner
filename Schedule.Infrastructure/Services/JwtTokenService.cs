using Microsoft.IdentityModel.Tokens;
using Schedule.Application.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Schedule.Infrastructure.Services;

public class JwtTokenService : IJwtTokenService
{

	public JwtTokenService()
	{

	}

	public string GenerateToken(Guid userId)
	{
		List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, userId.ToString())
			};


		RSA rsa = RSA.Create();
		rsa.ImportFromPem(File.ReadAllText("./data/private.key"));
		SigningCredentials creds = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);

		JwtSecurityToken token = new JwtSecurityToken(
			issuer: EnvironmentService.JwtIssuer, // Schedule.Auth
			audience: EnvironmentService.JwtAudience, // Schedule.API
			claims: claims,
			expires: DateTime.UtcNow.AddMinutes(int.Parse(EnvironmentService.JwtExpiresInMinutes)), // 240 mintutes
			signingCredentials: creds);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
