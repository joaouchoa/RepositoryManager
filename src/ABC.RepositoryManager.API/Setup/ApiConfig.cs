using ABC.RepositoryManager.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ABC.RepositoryManager.API.Setup
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDependencyInjection();
            services.AddDbContext<ReposManagerContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
