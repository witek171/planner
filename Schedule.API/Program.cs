using System.Data;
using Microsoft.Data.SqlClient;
using PlannerNet.Mappings;
using Schedule.Application.Interfaces.Services;
using Schedule.Application.Interfaces.Utils;
using Schedule.Infrastructure.Services;
using Schedule.Infrastructure.Utils;
using Schedule.Application.Services;
using Schedule.Infrastructure.Repositories;
using Schedule.Application.Interfaces;
using Schedule.Application.Mappings;

namespace PlannerNet;

public class Program
{
	public static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Add services to the container.

		builder.Services.AddScoped<IDbConnection>(sp =>
			new SqlConnection(connectionString));

		builder.Services.AddControllers();
		builder.Services.AddScoped<IHealthCheckService, HealthCheckService>();
		builder.Services.AddScoped<IHealthCheckUtils, HealthCheckUtils>();
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