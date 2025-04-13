using ABC.RepositoryManager.Application.Contracts;
using ABC.RepositoryManager.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ABC.RepositoryManager.Infrastructure.ServiceCollection
{
    public static class InfrastructureServiceCollection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IReposReadRepository, ReposReadRepository>();

            return services;
        }
    }
}
