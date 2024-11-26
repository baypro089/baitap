using Application.Interfaces;
using Ardalis.GuardClauses;
using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Lấy chuỗi kết nối từ cấu hình
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

            // Cấu hình DbContext với MySQL
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            // Cấu hình JWT Authentication
            services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]))
                    };
                });

            // Cấu hình Authorization nếu cần
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            });

            // Thêm các dịch vụ cần thiết
            services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddTransient<IUserService, UserService>();

            // Thêm AutoMapper (nếu cần)
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
