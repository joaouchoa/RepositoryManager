using ABC.RepositoryManager.Application.ServiceCollection;
using ABC.RepositoryManager.Infrastructure.ServiceCollection;

namespace ABC.RepositoryManager.API.Setup
{
    public static class DependenceInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddApplicationServices();
            services.AddInfrastructureServices();

            return services;
        }
    }
}
