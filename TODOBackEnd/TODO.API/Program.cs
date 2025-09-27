
using Microsoft.EntityFrameworkCore;
using TODO.API.Settings;
using TODO.Application.Contracts.Service;
using TODO.Application.Service;
using TODO.Domain.Contracts.Repository;
using TODO.Infrastructure.Data;
using TODO.Infrastructure.Repository;

namespace TODO.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "TODO API",
                    Version = "v1",
                });
            });


            builder.Services.AddOpenApi();
            
            // Перенести DI по слоям
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(o => o.UseNpgsql(connectionString));
            builder.Services.AddScoped<ITodoRepository, EfTodoRepository>();
            builder.Services.AddScoped<IUserRepository, EfUserRepository>();
            builder.Services.AddScoped<ITodoService, TodoService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.Configure<JwtSettings>(
                builder.Configuration.GetSection("JwtSettings"));

            

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:5173") 
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });

                options.AddPolicy("AllowSwager", policy =>
                {
                    policy.WithOrigins("http://localhost:7045")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers();

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TODO API v1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseCors("AllowReactApp");
            app.UseCors("AllowSwager");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
