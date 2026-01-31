using Microsoft.EntityFrameworkCore;
using RecipePlanner.Contracts.GroceryList;
using RecipePlanner.Contracts.Ingredient;
using RecipePlanner.Contracts.PlannedDay;
using RecipePlanner.Contracts.Recipe;
using RecipePlanner.Contracts.RecipeIngredient;
using RecipePlanner.Entities;

namespace RecipePlanner.Data {
    public interface IRecipePlannerStorage {
        Task<List<Unit>> GetAllUnitsAsync(CancellationToken ct = default);

        Task<List<IngredientListItem>> GetAllIngredientsForListAsync(CancellationToken ct = default);
        Task<List<IngredientComboItem>> GetAllIngredientsForComboAsync(CancellationToken ct = default);
        Task<Ingredient?> GetIngredientByIdAsync(int id, CancellationToken ct = default);
        Task<int> AddIngredientAsync(string name, int defaultUnitId, bool countForOverlap, CancellationToken ct = default);
        Task UpdateIngredientAsync(int id, string name, int defaultUnitId, bool countForOverlap, CancellationToken ct = default);
        Task DeleteIngredientAsync(int id, CancellationToken ct = default);

        Task<List<RecipeListItem>> GetAllRecipesAsync(CancellationToken ct = default);
        Task<Recipe?> GetRecipeByIdAsync(int id, CancellationToken ct = default);
        Task<int> AddRecipeAsync(string name, PrepTime preptime, string? info, CancellationToken ct = default);
        Task UpdateRecipeAsync(int id, string name, PrepTime preptime, string? info, CancellationToken ct = default);
        Task DeleteRecipeAsync(int id, CancellationToken ct = default);
        Task<List<RecipeSource>> GetRecipeSourcesForPlanningAsync(CancellationToken ct = default);


        Task<List<RecipeIngredientListItem>> GetAllRecipeIngredientsAsync(int recipeId, CancellationToken ct = default);
        Task<int> AddRecipeIngredientAsync(int recipeId, int ingredientId, int unitId, decimal quantity, CancellationToken ct = default);
        Task UpdateRecipeIngredientAsync(int recipeIngredientId, int ingredientId, int unitId, decimal quantity, CancellationToken ct = default);
        Task DeleteRecipeIngredientAsync(int recipeIngredientId, CancellationToken ct = default);


        Task<Weekplan?> GetWeekplanByIdAsync(int id, CancellationToken ct = default);
        Task<Weekplan?> GetWeekplanWithDaysByIdAsync(int id, CancellationToken ct = default);
        Task<Weekplan?> GetWeekplanWithDaysByStartdateAsync(DateOnly weekStartDate, CancellationToken ct = default);
        Task<int> AddWeekplanAsync(DateOnly weekStartDate, CancellationToken ct = default);
        Task<PlannedDay?> GetPlannedDayByIdAsync(int id, CancellationToken ct = default);
        Task<PlannedDay?> GetPlannedDayByWeekplanAndDateAsync(int weekplanId, DateOnly date, CancellationToken ct = default);
        Task<int> AddPlannedDayAsync(int weekplanId, DateOnly date, PrepTime availablePrepTime, int? recipeId, CancellationToken ct = default);
        Task UpdatePlannedDayAsync(int id, PrepTime availablePrepTime, int? recipeId, CancellationToken ct = default);


        Task<List<int>> GetRecipeIdsForWeekAsync(DateOnly weekStartDate, CancellationToken ct = default);
        Task<List<GroceryListItem>> GetGroceryListItemsForRecipesAsync(IReadOnlyDictionary<int, int> recipeCounts, CancellationToken ct = default);
        Task<Dictionary<int, int>> GetRecipeCountsForWeekAsync(DateOnly weekStartDate, CancellationToken ct = default);

        Task SaveSeedDataAsync(CancellationToken ct = default);

    }

    public class RecipePlannerStorage : IRecipePlannerStorage {
        private readonly IRecipePlannerDbContextFactory _factory;

        public RecipePlannerStorage(IRecipePlannerDbContextFactory factory) {
            _factory = factory;
        }


        //**************** Grocery List ****************


        public async Task<List<GroceryListItem>> GetGroceryListItemsForRecipesAsync(
            IReadOnlyDictionary<int, int> recipeCounts,
            CancellationToken ct = default
        ) {
            if (recipeCounts == null || recipeCounts.Count == 0)
                return [];

            await using var db = _factory.CreateDbContext();

            var rows = await db.RecipeIngredients
                .AsNoTracking()
                .Where(ri => recipeCounts.Keys.Contains(ri.RecipeId))
                .Select(ri => new {
                    ri.RecipeId,
                    ri.IngredientId,
                    IngredientName = ri.Ingredient.Name,
                    UnitName = ri.Unit.Name,
                    CountForOverlap = ri.Ingredient.CountForOverlap,
                    ri.Quantity
                })
                .ToListAsync(ct);

            return rows
                .Select(x => new {
                    x.IngredientId,
                    x.IngredientName,
                    x.UnitName,
                    x.CountForOverlap,
                    Quantity = x.Quantity * recipeCounts[x.RecipeId]
                })
                .GroupBy(x => new { x.IngredientId, x.IngredientName, x.UnitName, x.CountForOverlap })
                .Select(g => new GroceryListItem(
                    g.Key.IngredientId,
                    g.Key.IngredientName,
                    g.Sum(x => x.Quantity),
                    g.Key.UnitName,
                    g.Key.CountForOverlap
                ))
                .OrderByDescending(x => x.CountForOverlap)
                .ThenBy(x => x.IngredientName)
                .ToList();
        }

        public async Task<List<int>> GetRecipeIdsForWeekAsync(DateOnly weekStartDate, CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            return await db.PlannedDays
                .AsNoTracking()
                .Where(pd => pd.Weekplan.WeekStartDate == weekStartDate)
                .Where(pd => pd.RecipeId != null)
                .Select(pd => pd.RecipeId!.Value)
                .Distinct()
                .ToListAsync(ct);
        }
        public async Task<Dictionary<int, int>> GetRecipeCountsForWeekAsync(
            DateOnly weekStartDate,
            CancellationToken ct = default
        ) {
            await using var db = _factory.CreateDbContext();

            return await db.PlannedDays
                .AsNoTracking()
                .Where(pd => pd.Weekplan.WeekStartDate == weekStartDate)
                .Where(pd => pd.RecipeId != null)
                .GroupBy(pd => pd.RecipeId!.Value)
                .Select(g => new { RecipeId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.RecipeId, x => x.Count, ct);
        }


        //**************** Weekplan ********************
        public async Task<Weekplan?> GetWeekplanByIdAsync(int id, CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            return await db.Weekplans
                .AsNoTracking()
                .FirstOrDefaultAsync(wp => wp.Id == id, ct);
        }
        public async Task<Weekplan?> GetWeekplanWithDaysByIdAsync(int id, CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            return await db.Weekplans
                .AsNoTracking()
                .Include(wp => wp.PlannedDays)
                .ThenInclude(pd => pd.Recipe)
                .FirstOrDefaultAsync(wp => wp.Id == id, ct);
        }

        public async Task<Weekplan?> GetWeekplanWithDaysByStartdateAsync(DateOnly weekStartDate, CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            return await db.Weekplans
                .AsNoTracking()
                .Include(wp => wp.PlannedDays)
                .ThenInclude(pd => pd.Recipe)
                .FirstOrDefaultAsync(wp => wp.WeekStartDate == weekStartDate, ct);
        }


        public async Task<int> AddWeekplanAsync(DateOnly weekStartDate, CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            var weekplan = new Weekplan { WeekStartDate = weekStartDate };
            db.Weekplans.Add(weekplan);
            await db.SaveChangesAsync(ct);

            return weekplan.Id;
        }



        //no update needed because makes no sense. New start date is new week. Update is only done through PlannedDays

        public async Task<PlannedDay?> GetPlannedDayByIdAsync(int id, CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            return await db.PlannedDays
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id, ct);
        }
        public async Task<PlannedDay?> GetPlannedDayByWeekplanAndDateAsync(
            int weekplanId,
            DateOnly date,
            CancellationToken ct = default
        ) {
            await using var db = _factory.CreateDbContext();

            return await db.PlannedDays
                .AsNoTracking()
                .FirstOrDefaultAsync(pd => pd.WeekplanId == weekplanId && pd.Date == date, ct);
        }


        public async Task<int> AddPlannedDayAsync(int weekplanId, DateOnly date, PrepTime availablePrepTime, int? recipeId, CancellationToken ct = default) {

            await using var db = _factory.CreateDbContext();

            var plannedDay = new PlannedDay {
                WeekplanId = weekplanId,
                Date = date,
                AvailablePrepTime = availablePrepTime,
                RecipeId = recipeId
            };
            db.PlannedDays.Add(plannedDay);
            await db.SaveChangesAsync(ct);

            return plannedDay.Id;
        }

        public async Task UpdatePlannedDayAsync(int id, PrepTime availablePrepTime, int? recipeId, CancellationToken ct = default) {

            await using var db = _factory.CreateDbContext();

            var plannedDay = await db.PlannedDays.FindAsync([id], ct);
            if (plannedDay is null)
                throw new InvalidOperationException($"PlannedDay with id {id} not available in DB");

            plannedDay.AvailablePrepTime = availablePrepTime;
            plannedDay.RecipeId = recipeId;

            await db.SaveChangesAsync(ct);
        }



        // ***************** Units *****************
        public async Task<List<Unit>> GetAllUnitsAsync(CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            return await db.Units
                .AsNoTracking()
                .OrderBy(u => u.Name)
                .ToListAsync(ct);
        }


        // ***************** Ingredients *****************
        public async Task<List<IngredientListItem>> GetAllIngredientsForListAsync(CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            return await db.Ingredients
                .AsNoTracking()
                .OrderBy(i => i.Name)
                .Select(i => new IngredientListItem(
                    i.Id,
                    i.Name,
                    i.DefaultUnit != null ? i.DefaultUnit.Name : null,
                    i.CountForOverlap
                ))
                .ToListAsync(ct);
        }

        public async Task<List<IngredientComboItem>> GetAllIngredientsForComboAsync(CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            return await db.Ingredients
                .AsNoTracking()
                .OrderBy(i => i.Name)
                .Select(i => new IngredientComboItem(
                    i.Id,
                    i.Name
                ))
                .ToListAsync(ct);
        }

        public async Task<Ingredient?> GetIngredientByIdAsync(int id, CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            return await db.Ingredients
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id, ct);
        }

        public async Task<int> AddIngredientAsync(string name, int defaultUnitId, bool countForOverlap, CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            var ingredient = new Ingredient { Name = name, DefaultUnitId = defaultUnitId, CountForOverlap = countForOverlap };
            db.Ingredients.Add(ingredient);
            await db.SaveChangesAsync(ct);

            return ingredient.Id;
        }
        public async Task UpdateIngredientAsync(int id, string name, int defaultUnitId, bool countForOverlap, CancellationToken ct = default) {

            await using var db = _factory.CreateDbContext();

            var ingredient = await db.Ingredients.FindAsync([id], ct);
            if (ingredient is null)
                throw new InvalidOperationException($"Ingredient with id {id} not available in DB");

            ingredient.Name = name;
            ingredient.DefaultUnitId = defaultUnitId;
            ingredient.CountForOverlap = countForOverlap;

            await db.SaveChangesAsync(ct);
        }
        public async Task DeleteIngredientAsync(int id, CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            var ingredient = await db.Ingredients.FindAsync([id], ct);
            if (ingredient is null)
                throw new InvalidOperationException($"Ingredient with id {id} not available in DB");

            db.Ingredients.Remove(ingredient);

            await db.SaveChangesAsync(ct);
        }

        // ***************** Recipes *****************
        public async Task<List<RecipeListItem>> GetAllRecipesAsync(CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            return await db.Recipes
                .AsNoTracking()
                .OrderBy(r => r.Name)
                .Select(r => new RecipeListItem(
                    r.Id,
                    r.Name,
                    r.Info,
                    r.PrepTime
            ))
            .ToListAsync(ct);
        }
        public async Task<Recipe?> GetRecipeByIdAsync(int id, CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            return await db.Recipes
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id, ct);
        }

        public async Task<int> AddRecipeAsync(string name, PrepTime preptime, string? info, CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            var recipe = new Recipe { Name = name, PrepTime = preptime, Info = info };

            db.Recipes.Add(recipe);
            await db.SaveChangesAsync(ct);

            return recipe.Id;
        }
        public async Task UpdateRecipeAsync(int id, string name, PrepTime preptime, string? info, CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            var recipe = await db.Recipes.FindAsync([id], ct);
            if (recipe is null)
                throw new InvalidOperationException($"Recipe with id {id} not available in DB");

            recipe.Name = name;
            recipe.PrepTime = preptime;
            recipe.Info = info;

            await db.SaveChangesAsync(ct);
        }
        public async Task DeleteRecipeAsync(int id, CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            var recipe = await db.Recipes.FindAsync([id], ct);
            if (recipe is null)
                throw new InvalidOperationException($"Recipe with id {id} not available in DB");

            db.Recipes.Remove(recipe);

            await db.SaveChangesAsync(ct);
        }

        public async Task<List<RecipeSource>> GetRecipeSourcesForPlanningAsync(CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            //All Recipes
            var recipes = await db.Recipes
                .AsNoTracking()
                .Select(r => new { r.Id, r.Name, r.Info })
                .ToListAsync(ct);

            //All used ingrediënts
            var all = await db.RecipeIngredients
                .AsNoTracking()
                .Select(ri => new { ri.RecipeId, ri.IngredientId, IngredientName = ri.Ingredient.Name })
                .Distinct()
                .ToListAsync(ct);

            var allByRecipeId = all
                .GroupBy(x => x.RecipeId)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => new CountedIngredient(x.IngredientId, x.IngredientName))
                          .Distinct()
                          .OrderBy(x => x.Name)
                          .ToList()
                );

            //Ingredients that count for overlap
            var counted = await db.RecipeIngredients
                .AsNoTracking()
                .Where(ri => ri.Ingredient.CountForOverlap)
                .Select(ri => new { ri.RecipeId, ri.IngredientId, IngredientName = ri.Ingredient.Name })
                .Distinct()
                .ToListAsync(ct);

            var countedByRecipeId = counted
                .GroupBy(x => x.RecipeId)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => new CountedIngredient(x.IngredientId, x.IngredientName))
                          .Distinct()
                          .OrderBy(x => x.Name)
                          .ToList()
                );

            // 3) Samenvoegen naar RecipeSource
            return recipes
                .Select(r => new RecipeSource(
                    r.Id,
                    r.Name,
                    r.Info,
                    countedByRecipeId.TryGetValue(r.Id, out var list) ? list : new List<CountedIngredient>(),
                    allByRecipeId.TryGetValue(r.Id, out var allList) ? allList : new List<CountedIngredient>()
                ))
                .ToList();
        }

        // ***************** RecipeIngredients *****************

        public async Task<List<RecipeIngredientListItem>> GetAllRecipeIngredientsAsync(
            int recipeId,
            CancellationToken ct = default
        ) {
            await using var db = _factory.CreateDbContext();

            return await db.RecipeIngredients
                .AsNoTracking()
                .Where(ri => ri.RecipeId == recipeId)
                .OrderBy(ri => ri.Ingredient.Name)
                .Select(ri => new RecipeIngredientListItem(
                    ri.Id,
                    ri.IngredientId,
                    ri.Ingredient.Name,
                    ri.UnitId,
                    ri.Unit.Name,
                    ri.Quantity
                ))
                .ToListAsync(ct);
        }

        public async Task<int> AddRecipeIngredientAsync(
            int recipeId,
            int ingredientId,
            int unitId,
            decimal quantity,
            CancellationToken ct = default
        ) {
            await using var db = _factory.CreateDbContext();

            var recipeIngredient = new RecipeIngredient {
                RecipeId = recipeId,
                IngredientId = ingredientId,
                UnitId = unitId,
                Quantity = quantity
            };

            db.RecipeIngredients.Add(recipeIngredient);
            await db.SaveChangesAsync(ct);

            return recipeIngredient.Id;
        }
        public async Task UpdateRecipeIngredientAsync(
            int recipeIngredientId,
            int ingredientId,
            int unitId,
            decimal quantity,
            CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            var recipeIngredient = await db.RecipeIngredients.FindAsync([recipeIngredientId], ct);
            if (recipeIngredient is null)
                throw new InvalidOperationException($"RecipeIngredient with id {recipeIngredientId} not available in DB");

            recipeIngredient.IngredientId = ingredientId;
            recipeIngredient.UnitId = unitId;
            recipeIngredient.Quantity = quantity;

            await db.SaveChangesAsync(ct);
        }

        public async Task DeleteRecipeIngredientAsync(int recipeIngredientId, CancellationToken ct = default) {
            await using var db = _factory.CreateDbContext();

            var recipeIngredient = await db.RecipeIngredients.FindAsync([recipeIngredientId], ct);
            if (recipeIngredient is null)
                throw new InvalidOperationException($"RecipeIngredient with id {recipeIngredientId} not available in DB");

            db.RecipeIngredients.Remove(recipeIngredient);
            await db.SaveChangesAsync(ct);
        }



        // ***************** Seed Data *****************
        public async Task SaveSeedDataAsync(CancellationToken ct = default) {

            await using var db = _factory.CreateDbContext();

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
                    Quantity = 300 // of 300m als decimal
                },
                new RecipeIngredient {
                    RecipeId = r1.Id,
                    IngredientId = rijst.Id,
                    UnitId = gram.Id,
                    Quantity = 200
                },
                new RecipeIngredient {
                    RecipeId = r2.Id,
                    IngredientId = rijst.Id,
                    UnitId = gram.Id,
                    Quantity = 200
                },
                new RecipeIngredient {
                    RecipeId = r2.Id,
                    IngredientId = ui.Id,
                    UnitId = stuk.Id,
                    Quantity = 1
                }
            );

            await db.SaveChangesAsync();

        }

    }
}
