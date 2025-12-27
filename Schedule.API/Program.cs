using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Schedule.API.Mappings;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Application.Interfaces.Utils;
using Schedule.Application.Interfaces.Validators;
using Schedule.Application.Services;
using Schedule.Infrastructure.Extensions;
using Schedule.Infrastructure.Repositories;
using Schedule.Infrastructure.Services;
using Schedule.Infrastructure.Utils;
using System.Security.Cryptography;
using Schedule.Infrastructure.Validators;

namespace PlannerNet;

public class Program
{
	public static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		builder.Services.AddControllers();
		builder.Services.AddAutoMapper(typeof(MappingProfile));
		builder.Services.AddEndpointsApiExplorer();

		builder.Services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo { Title = "Planner API", Version = "v1" });
			c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				Description = "Enter the token in the format: {token}",
				Name = "Authorization",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.Http,
				Scheme = "bearer",
				BearerFormat = "JWT"
			});

			c.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						},
						Scheme = "bearer",
						Name = "Bearer",
						In = ParameterLocation.Header
					},
					new List<string>()
				}
			});
		});

		RSA rsa = RSA.Create();

		string[] possiblePaths =
		{
			"/app/data/public.key",
			"./Data/public.key",
			"./data/public.key"
		};

		string? publicKeyContent = null;
		foreach (string path in possiblePaths)
		{
			if (File.Exists(path))
			{
				publicKeyContent = File.ReadAllText(path);
				Console.WriteLine($"Key used from: {path}");
				break;
			}
		}

		if (publicKeyContent == null)
			throw new FileNotFoundException(
				"Public key not found! Please check whether the keys have been generated.");

		rsa.ImportFromPem(publicKeyContent);

		builder.Services.AddAuthentication("Bearer")
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = EnvironmentService.JwtIssuer,

					ValidateAudience = true,
					ValidAudience = EnvironmentService.JwtAudience,

					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new RsaSecurityKey(rsa)
				};
			});

		builder.Services.AddAuthorization();

		builder.Services.AddScoped<IParticipantRepository>(provider =>
			new ParticipantRepository(EnvironmentService.SqlConnectionString));
		builder.Services.AddScoped<IStaffMemberRepository>(provider =>
			new StaffMemberRepository(EnvironmentService.SqlConnectionString));
		builder.Services.AddScoped<IStaffMemberSpecializationRepository>(provider =>
			new StaffMemberSpecializationRepository(EnvironmentService.SqlConnectionString));
		builder.Services.AddScoped<IStaffMemberAvailabilityRepository>(provider =>
			new StaffMemberAvailabilityRepository(EnvironmentService.SqlConnectionString));
		builder.Services.AddScoped<IEventScheduleRepository>(provider =>
			new EventScheduleRepository(EnvironmentService.SqlConnectionString));
		builder.Services.AddScoped<IEventScheduleStaffMemberRepository>(provider =>
			new EventScheduleStaffMemberRepository(EnvironmentService.SqlConnectionString));
		builder.Services.AddScoped<ISpecializationRepository>(provider =>
			new SpecializationRepository(EnvironmentService.SqlConnectionString));
		builder.Services.AddScoped<ICompanyRepository>(provider =>
			new CompanyRepository(EnvironmentService.SqlConnectionString));
		builder.Services.AddScoped<IEventTypeRepository>(provider =>
			new EventTypeRepository(EnvironmentService.SqlConnectionString));
		builder.Services.AddScoped<IReservationRepository>(provider =>
			new ReservationRepository(EnvironmentService.SqlConnectionString));
		builder.Services.AddScoped<IReservationParticipantRepository>(provider =>
			new ReservationParticipantRepository(EnvironmentService.SqlConnectionString));
		builder.Services.AddScoped<ICompanyConfigRepository>(provider =>
			new CompanyConfigRepository(EnvironmentService.SqlConnectionString));

		builder.Services.AddScoped<IHealthCheckService>(provider =>
		{
			IHealthCheckUtils healthCheckUtils = provider.GetRequiredService<IHealthCheckUtils>();
			ILogger<HealthCheckService> logger = provider.GetRequiredService<ILogger<HealthCheckService>>();
			string connectionString = EnvironmentService.SqlConnectionString;

			return new HealthCheckService(healthCheckUtils, logger, connectionString);
		});
		builder.Services.AddScoped<IParticipantService, ParticipantService>();
		builder.Services.AddScoped<IStaffMemberService, StaffMemberService>();
		builder.Services.AddScoped<IStaffMemberSpecializationService, StaffMemberSpecializationService>();
		builder.Services.AddScoped<IStaffMemberAvailabilityService, StaffMemberAvailabilityService>();
		builder.Services.AddScoped<IEventScheduleStaffMemberService, EventScheduleStaffMemberService>();
		builder.Services.AddScoped<IEventScheduleService, EventScheduleService>();
		builder.Services.AddScoped<ISpecializationService, SpecializationService>();
		builder.Services.AddScoped<ICompanyService, CompanyService>();
		builder.Services.AddScoped<IEventTypeService, EventTypeService>();
		builder.Services.AddScoped<IReservationService, ReservationService>();
		builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
		builder.Services.AddScoped<IAuthService, AuthService>();
		builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
		builder.Services.AddScoped<ICompanyConfigService, CompanyConfigService>();

		builder.Services.AddScoped<IHealthCheckUtils, HealthCheckUtils>();

		builder.Services.AddScoped<IScheduleConflictValidator, ScheduleConflictValidator>();
		builder.Services.AddScoped<IAvailabilityCalculator, AvailabilityCalculator>();

		WebApplication app = builder.Build();

		app.UseMiddleware<GlobalExceptionMiddleware>();
		app.UseSwagger();
		app.UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint("/swagger/v1/swagger.json", "Planner API V1");
			c.RoutePrefix = "swagger";
		});

		app.MapGet("/", () => Results.Redirect("/swagger"));

		app.UseHttpsRedirection();

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}