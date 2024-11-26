using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NSwag.Generation.Processors.Security;
using NSwag;
using Presentation.Services;

namespace Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            // Đăng ký User hiện tại thông qua IHttpContextAccessor
            services.AddScoped<IUser, CurrentUser>();

            // Đăng ký HttpContextAccessor
            services.AddHttpContextAccessor();

            // Tùy chỉnh hành vi API mặc định (nếu cần)
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            // Cấu hình Swagger (NSwag) nếu cần thiết
            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "CleanArchitecture API";

                // Thêm JWT Security vào Swagger (nếu cần)
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });
                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            return services;
        }
    }
}
