using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mov.Domain.Interfaces.Repositories;
using Mov.Infrastructure.Data;
using Mov.Infrastructure.Repositories;

namespace Mov.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString,
                    ServerVersion.AutoDetect(connectionString),
                    b => b.MigrationsAssembly("Eco.Infrastructure")));

            // Register repositories
            services.AddScoped<ICalendarioRepository, CalendarioRepository>();

            return services;
        }
    }
}