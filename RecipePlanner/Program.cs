using Microsoft.Extensions.DependencyInjection;
using RecipePlanner.App;
using RecipePlanner.Data;

namespace RecipePlanner {
    internal static class Program {
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

            using var provider = services.BuildServiceProvider();
            var mainForm = provider.GetRequiredService<frmMain>();
            Application.Run(mainForm);


        }
    }
}