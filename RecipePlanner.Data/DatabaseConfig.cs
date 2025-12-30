namespace RecipePlanner.Data {
    public static class DatabaseConfig {

        static Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        static string path = Environment.GetFolderPath(folder);
        static string dbpath = Path.Combine(path, "RecipePlanner.db");

        public static string ConnectionString { get; } = $"Data Source={dbpath}";
    }
}
