using Microsoft.EntityFrameworkCore;
using RecipePlanner.Contracts.Ingredient;
using RecipePlanner.Contracts.Recipe;
using RecipePlanner.Entities;

namespace RecipePlanner.Data {
    public interface IRecipePlannerStorage {
        Task<List<Unit>> GetAllUnitsAsync(CancellationToken ct = default);

        Task<List<IngredientListItem>> GetAllIngredientsAsync(CancellationToken ct = default);
        Task<Ingredient?> GetIngredientByIdAsync(int id, CancellationToken ct = default);
        Task<int> AddIngredientAsync(string name, int defaultUnitId, CancellationToken ct = default);
        Task UpdateIngredientAsync(int id, string name, int defaultUnitId, CancellationToken ct = default);
        Task DeleteIngredientAsync(int id, CancellationToken ct = default);

        Task<List<RecipeListItem>> GetAllRecipesAsync(CancellationToken ct = default);

        Task SaveSeedDataAsync(CancellationToken ct = default);

    }

    public class RecipePlannerStorage : IRecipePlannerStorage {
        private readonly IRecipePlannerDbContextFactory _factory;

        public RecipePlannerStorage(IRecipePlannerDbContextFactory factory) {
            _factory = factory;
        }

        // ***************** Units *****************
        public async Task<List<Unit>> GetAllUnitsAsync(CancellationToken ct = default) {
            using var db = _factory.CreateDbContext();

            return await db.Units
                .AsNoTracking()
                .OrderBy(u => u.Name)
                .ToListAsync(ct);
        }


        // ***************** Ingredients *****************
        public async Task<List<IngredientListItem>> GetAllIngredientsAsync(CancellationToken ct = default) {
            using var db = _factory.CreateDbContext();

            return await db.Ingredients
                .AsNoTracking()
                .OrderBy(i => i.Name)
                .Select(i => new IngredientListItem(
                    i.Id,
                    i.Name,
                    i.DefaultUnit != null ? i.DefaultUnit.Name : null
                ))
                .ToListAsync(ct);
        }

        public async Task<Ingredient?> GetIngredientByIdAsync(int id, CancellationToken ct = default) {
            using var db = _factory.CreateDbContext();

            return await db.Ingredients
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id, ct);
        }

        public async Task<int> AddIngredientAsync(string name, int defaultUnitId, CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            var ingredient = new Ingredient { Name = name, DefaultUnitId = defaultUnitId };
            db.Ingredients.Add(ingredient);
            await db.SaveChangesAsync(ct);

            return ingredient.Id;
        }
        public async Task UpdateIngredientAsync(int id, string name, int defaultUnitId, CancellationToken ct = default) {

            await using var db = _factory.CreateDbContext();

            var ingredient = await db.Ingredients.FindAsync([id], ct);
            if (ingredient is null)
                throw new InvalidOperationException("Ingrediënt bestaat niet (meer).");

            ingredient.Name = name;
            ingredient.DefaultUnitId = defaultUnitId;

            await db.SaveChangesAsync(ct);
        }
        public async Task DeleteIngredientAsync(int id, CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            var ingredient = await db.Ingredients.FindAsync([id], ct);
            if (ingredient is null)
                throw new InvalidOperationException("Ingrediënt bestaat niet (meer).");

            db.Ingredients.Remove(ingredient);

            await db.SaveChangesAsync(ct);
        }

        // ***************** Recipes *****************
        public async Task<List<RecipeListItem>> GetAllRecipesAsync(CancellationToken ct = default) {
            using var db = _factory.CreateDbContext();

            return await db.Recipes
                .AsNoTracking()
                .OrderBy(r => r.Name)
                .Select(r => new RecipeListItem(
                    r.Id,
                    r.Name,
                    r.PrepTime
            ))
            .ToListAsync(ct);
        }


        // ***************** Seed Data *****************
        public async Task SaveSeedDataAsync(CancellationToken ct = default) {

            using var db = _factory.CreateDbContext();

            // Voorkom dubbel seeden: als er al recepten zijn, stoppen.
            if (db.Recipes.Any()) {
                return;
            }

            // Units
            var gram = new Unit { Name = "gram" };
            var stuk = new Unit { Name = "stuk" };
            db.Units.AddRange(gram, stuk);
            await db.SaveChangesAsync(); // nodig zodat gram.Id/stuk.Id beschikbaar zijn

            // Ingredients (met DefaultUnitId)
            var broccoli = new Ingredient { Name = "Broccoli", DefaultUnitId = gram.Id };
            var rijst = new Ingredient { Name = "Rijst", DefaultUnitId = gram.Id };
            var ui = new Ingredient { Name = "Ui", DefaultUnitId = stuk.Id };
            db.Ingredients.AddRange(broccoli, rijst, ui);
            await db.SaveChangesAsync();

            // Recipes
            var r1 = new Recipe { Name = "Broccoli rijst", PrepTime = PrepTime.Short };
            var r2 = new Recipe { Name = "Rijst met ui", PrepTime = PrepTime.Short };
            db.Recipes.AddRange(r1, r2);
            await db.SaveChangesAsync();

            // RecipeIngredient (koppelingen + hoeveelheden)
            db.RecipeIngredients.AddRange(
                new RecipeIngredient {
                    RecipeId = r1.Id,
                    IngredientId = broccoli.Id,
                    UnitId = gram.Id,
                    NumberOfUnits = 300 // of 300m als decimal
                },
                new RecipeIngredient {
                    RecipeId = r1.Id,
                    IngredientId = rijst.Id,
                    UnitId = gram.Id,
                    NumberOfUnits = 200
                },
                new RecipeIngredient {
                    RecipeId = r2.Id,
                    IngredientId = rijst.Id,
                    UnitId = gram.Id,
                    NumberOfUnits = 200
                },
                new RecipeIngredient {
                    RecipeId = r2.Id,
                    IngredientId = ui.Id,
                    UnitId = stuk.Id,
                    NumberOfUnits = 1
                }
            );

            await db.SaveChangesAsync();

        }

    }
}
