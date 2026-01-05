using Microsoft.EntityFrameworkCore;
using RecipePlanner.Entities;

namespace RecipePlanner.Data {


    public class RecipePlannerDbContext : DbContext {
        public RecipePlannerDbContext(DbContextOptions<RecipePlannerDbContext> options)
            : base(options) { }

        public DbSet<Ingredient> Ingredients => Set<Ingredient>();
        public DbSet<Unit> Units => Set<Unit>();
        public DbSet<Recipe> Recipes => Set<Recipe>();
        public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();
        public DbSet<Weekplan> Weekplans => Set<Weekplan>();
        public DbSet<PlannedDay> PlannedDays => Set<PlannedDay>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // Ingredient -> DefaultUnit
            modelBuilder.Entity<Ingredient>(entity => {
                entity.HasOne(i => i.DefaultUnit)
                      .WithMany()
                      .HasForeignKey(i => i.DefaultUnitId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // RecipeIngredient (join entity met extra velden)
            modelBuilder.Entity<RecipeIngredient>(entity => {
                entity.HasKey(x => new { x.RecipeId, x.IngredientId });

                entity.HasOne(x => x.Recipe)
                      .WithMany(r => r.RecipeIngredients)
                      .HasForeignKey(x => x.RecipeId);

                entity.HasOne(x => x.Ingredient)
                      .WithMany(i => i.RecipeIngredients)
                      .HasForeignKey(x => x.IngredientId);

                entity.HasOne(x => x.Unit)
                      .WithMany()
                      .HasForeignKey(x => x.UnitId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .IsRequired();
            });

            // Weekplan -> PlannedDay
            modelBuilder.Entity<PlannedDay>(entity => {
                entity.HasOne(d => d.Weekplan)
                      .WithMany(w => w.PlannedDays)
                      .HasForeignKey(d => d.WeekplanId);

                entity.HasOne(d => d.Recipe)
                      .WithMany()
                      .HasForeignKey(d => d.RecipeId)
                      .IsRequired(false);
            });
        }
    }

}
