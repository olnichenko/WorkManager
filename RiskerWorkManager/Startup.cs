using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiskerWorkManager.ConfigurationSettings;
using RiskerWorkManager.Models;
using RiskerWorkManager.Services;
using System;
using System.Net;
using WorkManagerDal;
using WorkManagerDal.Services;

namespace RiskerWorkManager
{
    public static class Startup
    {
        
        public static void ConfigureServices(this IServiceCollection services)
        {
        }
        public static void MapSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbSettings>(configuration.GetSection(DbSettings.SectionName));
            services.Configure<JWTTokenSettings>(configuration.GetSection(JWTTokenSettings.SectionName));
            services.Configure<CORSSettings>(configuration.GetSection(CORSSettings.SectionName));
        }
        public static void MapServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AppMappingProfile));

            var dbSettings = configuration.GetSection(DbSettings.SectionName).Get<DbSettings>();
            var corsSettings = configuration.GetSection(CORSSettings.SectionName).Get<CORSSettings>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped((_) => new WorkManagerDbContext(dbSettings.ConnectionString));
            services.AddScoped<IWorkManagerUnitOfWork, WorkManagerUnitOfWork>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IPermissionsService, PermissionsService>();
            services.AddScoped<IUserIdentityService, UserIdentityService>();
            services.AddScoped<ILogReaderService, LogReaderService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IProjectsService, ProjectsService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", GenerateCorsPolicy(corsSettings));
            });
        }

        public static CorsPolicy GenerateCorsPolicy(CORSSettings settings)
        {
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            //corsBuilder.AllowAnyOrigin(); // For anyone access.
            corsBuilder.WithOrigins(settings.AllowOrigins); // for a specific url. Don't add a forward slash on the end!
            corsBuilder.AllowCredentials();
            return corsBuilder.Build();
        }
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"{contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }
    }
}
