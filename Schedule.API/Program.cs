
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

            // Add services to the container.

            builder.Services.AddScoped<IDbConnection>(sp =>
            new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IStaffProfileRepository, StaffProfileRepository>();
            builder.Services.AddScoped<IStaffService, StaffService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
