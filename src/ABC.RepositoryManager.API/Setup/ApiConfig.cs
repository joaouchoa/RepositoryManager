namespace ABC.RepositoryManager.API.Setup
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDependencyInjection();

            return services;
        }
    }
}
