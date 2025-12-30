using Microsoft.Extensions.DependencyInjection;

namespace RecipePlanner.Data {
    public static class RecipePlannerServiceCollectionExtensions {
        public static IServiceCollection AddRecipePlannerInfraCore(
            this IServiceCollection services
        ) {

            // DbContext
            services.AddSingleton<IRecipePlannerDbContextFactory>(
            _ => new RecipePlannerDbContextFactory(DatabaseConfig.ConnectionString));

            // Storage
            services.AddScoped<IRecipePlannerStorage, RecipePlannerStorage>();


            return services;
        }
    }
}
