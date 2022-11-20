using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiskerWorkManager.ConfigurationSettings;
using RiskerWorkManager.Services;
using System;
using WorkManagerDal;
using WorkManagerDal.Services;

namespace RiskerWorkManager
{
    public static class Startup
    {
        
        public static void ConfigureServices(this IServiceCollection services)
        {
            //services.AddAutoMapper(typeof(Program));
            //services.AddControllersWithViews();
        }
        public static void MapSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbSettings>(configuration.GetSection(DbSettings.SectionName));
            services.Configure<JWTTokenSettings>(configuration.GetSection(JWTTokenSettings.SectionName));
        }
        public static void MapRepositories(this IServiceCollection services)
        {
            //services.AddScoped<IUserRepository<User, long>, UserRepository>();
            //services.AddScoped<IResetPasswordKeyRepository<ResetPasswordKey, Guid>, ResetPasswordKeyRepository>();
        }
        public static void MapServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AppMappingProfile));

            var dbSettings = configuration.GetSection(DbSettings.SectionName).Get<DbSettings>();
            var corsSettgins = configuration.GetSection(CORSSettings.SectionName).Get<CORSSettings>();

            // var context = new WorkManagerDbContext(dbSettings.ConnectionString);
            // var unitOfWork = new WorkManagerUnitOfWork(context);

            services.AddScoped((_) => new WorkManagerDbContext(dbSettings.ConnectionString));
            services.AddScoped((_) => new WorkManagerUnitOfWork(new WorkManagerDbContext(dbSettings.ConnectionString)));
            services.AddScoped((_) => new UsersService(new WorkManagerUnitOfWork(new WorkManagerDbContext(dbSettings.ConnectionString))));
            services.AddScoped((_) => {
                return new UserIdentityService(
                    new UsersService(new WorkManagerUnitOfWork(new WorkManagerDbContext(dbSettings.ConnectionString)))
                    );
                });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", GenerateCorsPolicy(corsSettgins));
            });

            //services.AddScoped((_) => new WorkManagerUnitOfWork(new WorkManagerDbContext(dbSettings.ConnectionString)));
            //services.AddScoped<IValidator<User>, UserValidator>();

            //services.AddScoped<ITokenService, TokenService>();
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IResetPasswordKeyService, ResetPasswordKeyService>();
            //services.AddScoped<IMailService, SmtpMailService>();
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
    }
}
