﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mirror.Application.Common.Interfaces;
using Mirror.Application.Common.Interfaces.Persistance;
using Mirror.Application.Common.Interfaces.Services;
using Mirror.Application.Services.Authentication;
using Mirror.Application.Services.Repository.Image;
using Mirror.Application.Services.Repository.Memory;
using Mirror.Application.Services.Repository.Progresses;
using Mirror.Application.Services.Repository.ProgressSection;
using Mirror.Application.Services.Repository.ProgressValues;
using Mirror.Infrastructure.Authentication;
using Mirror.Infrastructure.Persistance.Repository.Image;
using Mirror.Infrastructure.Persistance.Repository.ProgressSection;
using Mirror.Infrastructure.Persistance.Repository.User;
using Mirror.Infrastructure.Services;
using Mirror.Infrastructure.Services.Repository.Progress;
using Mirror.Infrastructure.Services.Repository.ProgressValues;
using Mirror.Infrastructure.Services.Repository.UserMemory;

namespace Mirror.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IProgressRepository, ProgressRepository>();
            services.AddScoped<IProgressSectionRepository, ProgressSectionRepository>();
            services.AddScoped<IProgressValueRepository, ProgressValueRepository>();
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserMemoryRepository, UserMemoryRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();

            return services;
        }
    }
}
