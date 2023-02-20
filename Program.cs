using WebApiTest.Data;
using WebApiTest.Interfaces;
using WebApiTest.Services;

using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace WebApiTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            var corsConfigName = "CORS-Config";

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: corsConfigName, policy =>
                {
                    policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                });
            });

            builder.Services.AddControllers(); //checks the project for controllers and automatically add it

            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("MSSqlConnection"));//get from appsettings.json
            });

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add services here
            // builder.Services.AddScoped<ITaskItemsService, TaskItemsApplicationContextService>();
            builder.Services.AddScoped<ITaskItemsService, TaskItemsMSSQLService>();
            builder.Services.AddScoped<IUserService, UserMSSQLService>();
            builder.Services.AddScoped<IPriorityService, PriorityMSSQLService>();
            builder.Services.AddScoped<IStatusService, StatusMSSQLService>();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(corsConfigName);

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}

