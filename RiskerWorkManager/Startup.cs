using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiskerWorkManager.ConfigurationSettings;
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
            var dbSettings = configuration.GetSection(DbSettings.SectionName).Get<DbSettings>();

            var context = new WorkManagerDbContext(dbSettings.ConnectionString);
            var unitOfWork = new WorkManagerUnitOfWork(context);

            services.AddScoped((_) => context);
            services.AddScoped((_) => unitOfWork);
            services.AddScoped((_) => new UsersService(unitOfWork));

            //services.AddScoped((_) => new WorkManagerUnitOfWork(new WorkManagerDbContext(dbSettings.ConnectionString)));
            //services.AddScoped<IValidator<User>, UserValidator>();

            //services.AddScoped<ITokenService, TokenService>();
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IResetPasswordKeyService, ResetPasswordKeyService>();
            //services.AddScoped<IMailService, SmtpMailService>();
        }
    }
}
