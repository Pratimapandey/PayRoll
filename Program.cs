using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PayRollManagement.Data;
using PayRollManagement.Model;
using PayRollManagement.Services.Implementation;
using PayRollManagement.Services.Interface;
using System.Text;

namespace PayRollManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration; // Retrieve the configuration

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<PayRollDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<PayRollDbContext>();

            builder.Services.AddScoped<IEmployeeService, EmployeeService>(); // Register the employee service
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PayRoll API", Version = "v1" });
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidAudience = configuration["Jwt:ValidAudience"], // Update configuration key
                    ValidIssuer = configuration["Jwt:ValidIssuer"], // Update configuration key
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"])) // Update configuration key
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication(); // Add authentication middleware
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}