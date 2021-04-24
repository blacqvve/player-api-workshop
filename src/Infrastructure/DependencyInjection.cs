using Player_API.Application.Common.Interfaces;
using Player_API.Infrastructure.Files;
using Player_API.Infrastructure.Identity;
using Player_API.Infrastructure.Persistence;
using Player_API.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence;

namespace Player_API.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("Player_APIDb"));
            }
            else
            {
                var conString =configuration.GetConnectionString("DefaultConnection");

                services.AddDbContext<PlayerDbContext>(options =>
                    options.UseMySql(
                        conString,
                        ServerVersion.AutoDetect(conString),
                        b => b.MigrationsAssembly(typeof(PlayerDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<PlayerDbContext>());

            services.AddScoped<IDomainEventService, DomainEventService>();

            // services
            //     .AddDefaultIdentity<ApplicationUser>()
            //     .AddRoles<IdentityRole>()
            //     .AddEntityFrameworkStores<ApplicationDbContext>();

            // services.AddIdentityServer()
            //     .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddTransient<IDateTime, DateTimeService>();
            //services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            // services.AddAuthorization(options =>
            // {
            //     options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            // });

            return services;
        }
    }
}