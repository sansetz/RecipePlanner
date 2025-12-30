using Microsoft.Extensions.DependencyInjection;

namespace RecipePlanner.App {
    public static class RecipePlannerServiceCollectionExtensions {
        public static IServiceCollection AddRecipePlannerApplicationCore(
            this IServiceCollection services
        ) {
            services.AddScoped<RecipePlannerService>();

            return services;
        }
    }
}
