using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VideogameTracker.Application.Interfaces;
using VideogameTracker.Infrastructure.Data;
using VideogameTracker.Infrastructure.Repositories;

namespace VideogameTracker.Infrastructure;

public static class DependencyInjection {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString) {

        
        services.AddDbContext<VideogameTrackerDbContext>(options =>
            options.UseNpgsql(connectionString));

        
        services.AddScoped<IVideogameRepository, VideogameRepository>();

        return services;
    }
}