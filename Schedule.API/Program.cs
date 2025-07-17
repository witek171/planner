using Microsoft.Data.SqlClient;
using System.Data;
using Common.Application.Services;
using Common.Application.Utils;
using Common.Infrastructure.Services;
using Common.Infrastructure.Utils;

namespace PlannerNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            
            // Add services to the container.

            builder.Services.AddScoped<IDbConnection>(sp =>
                new SqlConnection(connectionString));

            builder.Services.AddControllers();
            builder.Services.AddScoped<IHealthCheckService, HealthCheckService>();
            builder.Services.AddScoped<IHealthCheckUtils, HealthCheckUtils>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
}
