using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<FootballLeagueContext>(opts =>
                opts.UseSqlServer(connectionString));

            services.AddScoped<ITeamRepository, TeamRepository>();

            return services;
        }
    }
}
