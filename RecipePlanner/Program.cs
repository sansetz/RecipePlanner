using Microsoft.Extensions.DependencyInjection;
using RecipePlanner.App;
using RecipePlanner.Data;
using RecipePlanner.UI;

namespace RecipePlanner {
    internal static class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            // TODO's:
            // - Add boolean field to ingredient for marking if it should be used for finding overlap recipes


            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();
            services.AddRecipePlannerInfraCore();
            services.AddRecipePlannerApplicationCore();

            services.AddTransient<MainForm>();
            services.AddTransient<IngredientsForm>();
            services.AddTransient<RecipesForm>();
            services.AddTransient<IngredientEditForm>();
            services.AddTransient<RecipeEditForm>();

            using var serviceProvider = services.BuildServiceProvider();
            var mainForm = serviceProvider.GetRequiredService<MainForm>();
            Application.Run(mainForm);


        }
    }
}