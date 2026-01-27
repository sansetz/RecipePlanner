using Microsoft.Extensions.DependencyInjection;

namespace RecipePlanner.App {
    public static class RecipePlannerServiceCollectionExtensions {
        public static IServiceCollection AddRecipePlannerApplicationCore(
            this IServiceCollection services
        ) {
            services.AddScoped<RecipeService>();
            services.AddScoped<IngredientService>();
            services.AddScoped<UnitService>();
            services.AddScoped<RecipeIngredientService>();
            services.AddScoped<WeekplanService>();
            services.AddScoped<GroceryListService>();


            return services;
        }
    }
}
