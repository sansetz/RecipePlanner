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
            // - Add close button to EntityListViewControl that closed the parent form
            // - typing in combobox for selecting ingredients/recipes does not work correctly, can only type first letter
            // - print shoppinglist
            // - hover over recipes shows overlap ingredients
            // - save planned week + option to select week


            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();
            services.AddRecipePlannerInfraCore();
            services.AddRecipePlannerApplicationCore();

            services.AddTransient<MainForm>();
            services.AddTransient<IngredientsForm>();
            services.AddTransient<RecipesForm>();
            services.AddTransient<IngredientEditForm>();
            services.AddTransient<RecipeEditForm>();
            services.AddTransient<RecipeIngredientEditForm>();
            services.AddTransient<GroceryListForm>();

            using var serviceProvider = services.BuildServiceProvider();
            var mainForm = serviceProvider.GetRequiredService<MainForm>();
            Application.Run(mainForm);


        }
    }
}