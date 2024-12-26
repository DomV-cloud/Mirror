using Microsoft.Extensions.DependencyInjection;
using Mirror.Application.Services.Authentication;
using Mirror.Application.Services.FileService;

namespace Mirror.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}
