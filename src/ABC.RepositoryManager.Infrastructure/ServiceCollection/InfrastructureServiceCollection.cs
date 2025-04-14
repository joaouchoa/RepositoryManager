using ABC.RepositoryManager.Application.Contracts;
using ABC.RepositoryManager.Infrastructure.Context;
using ABC.RepositoryManager.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ABC.RepositoryManager.Infrastructure.ServiceCollection
{
    public static class InfrastructureServiceCollection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ReposManagerContext>();
            services.AddScoped<IReposReadRepository, ReposReadRepository>();
            services.AddScoped<IReposWriteRepository, ReposWriteRepository>();

            return services;
        }
    }
}
