using Microsoft.EntityFrameworkCore;

namespace RecipePlanner.Data {
    public interface IRecipePlannerDbContextFactory {
        RecipePlannerDbContext CreateDbContext();
    }

    public sealed class RecipePlannerDbContextFactory : IRecipePlannerDbContextFactory {
        private readonly string _connectionString;

        public RecipePlannerDbContextFactory(string connectionString) {
            _connectionString = connectionString;
        }

        public RecipePlannerDbContext CreateDbContext() {
            var options = new DbContextOptionsBuilder<RecipePlannerDbContext>()
                .UseSqlite(_connectionString)
                .Options;

            return new RecipePlannerDbContext(options);
        }
    }

}
