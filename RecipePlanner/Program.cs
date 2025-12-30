using Microsoft.Extensions.DependencyInjection;
using RecipePlanner.Data;

namespace RecipePlanner {
    internal static class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();

            // Factory registreren
            services.AddSingleton<IRecipePlannerDbContextFactory>(
                _ => new RecipePlannerDbContextFactory(DatabaseConfig.ConnectionString));

            // Forms
            services.AddTransient<frmMain>();

            using var sp = services.BuildServiceProvider();
            Application.Run(sp.GetRequiredService<frmMain>());


        }
    }
}