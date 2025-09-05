using Schedule.API.Mappings;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Application.Interfaces.Utils;
using Schedule.Application.Services;
using Schedule.Infrastructure.Repositories;
using Schedule.Infrastructure.Services;
using Schedule.Infrastructure.Utils;

namespace PlannerNet;

public class Program
{
	public static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		builder.Services.AddControllers();
		builder.Services.AddAutoMapper(typeof(MappingProfile));
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		// Repositories
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

		// Services
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

		builder.Services.AddScoped<IHealthCheckUtils, HealthCheckUtils>();

		WebApplication app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}