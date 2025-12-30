using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RecipePlanner.Data {
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RecipePlannerDbContext> {
        public RecipePlannerDbContext CreateDbContext(string[] args) {
            var options = new DbContextOptionsBuilder<RecipePlannerDbContext>()
                .UseSqlite(DatabaseConfig.ConnectionString)
                .Options;

            return new RecipePlannerDbContext(options);
        }
    }
}
