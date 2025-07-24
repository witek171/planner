
using Microsoft.Data.SqlClient;
using System.Data;

using Schedule.Application.Services;
using Schedule.Infrastructure.Repositories;

namespace PlannerNet
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddScoped<IDbConnection>(sp =>
			{
				var config = sp.GetRequiredService<IConfiguration>();
				var connectionString = config.GetConnectionString("DefaultConnection");
				return new SqlConnection(connectionString);
			});

			builder.Services.AddScoped<IStaffRepository, StaffRepository>();
			builder.Services.AddScoped<IStaffService, StaffService>();
			builder.Services.AddScoped<IStaffSpecializationRepository, StaffSpecializationRepository>();

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseAuthorization();
			app.MapControllers();
			app.Run();
		}
	}
}
