using BACKEND.Data;
using BACKEND.Interfaces;
using BACKEND.Services;

namespace BACKEND.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
        {
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}