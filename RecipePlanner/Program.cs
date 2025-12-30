using Microsoft.Extensions.DependencyInjection;
using RecipePlanner.App;
using RecipePlanner.Data;
using RecipePlanner.UI;

namespace RecipePlanner {
    internal static class Program {
        public static IServiceProvider ServiceProvider { get; private set; } = default!;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();
            services.AddRecipePlannerInfraCore();
            services.AddRecipePlannerApplicationCore();
            services.AddTransient<frmMain>();
            services.AddTransient<frmIngredients>();

            ServiceProvider = services.BuildServiceProvider();
            var mainForm = ServiceProvider.GetRequiredService<frmMain>();
            Application.Run(mainForm);


        }
    }
}