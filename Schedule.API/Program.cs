using PlannerNet.Mappings;
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

		// Add services to the container.

		builder.Services.AddControllers();
		builder.Services.AddScoped<IHealthCheckService>(provider =>
		{
			IHealthCheckUtils healthCheckUtils = provider.GetRequiredService<IHealthCheckUtils>();
			ILogger<HealthCheckService> logger = provider.GetRequiredService<ILogger<HealthCheckService>>();
			string connectionString = EnvironmentService.SqlConnectionString;

			return new HealthCheckService(healthCheckUtils, logger, connectionString);
		});
		builder.Services.AddScoped<IHealthCheckUtils, HealthCheckUtils>();
		builder.Services.AddScoped<IParticipantRepository>(provider =>
			new ParticipantRepository(EnvironmentService.SqlConnectionString));
		builder.Services.AddScoped<IParticipantService, ParticipantService>();
		builder.Services.AddAutoMapper(typeof(MappingProfile));
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

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