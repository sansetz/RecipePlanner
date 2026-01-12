using RecipePlanner.Contracts.GroceryList;
using RecipePlanner.Contracts.Ingredient;
using RecipePlanner.Contracts.PlannedDay;
using RecipePlanner.Contracts.Recipe;
using RecipePlanner.Contracts.RecipeIngredient;
using RecipePlanner.Data;
using RecipePlanner.Entities;

namespace RecipePlanner.App {
    public class RecipePlannerService {
        private readonly IRecipePlannerStorage _storage;

        public RecipePlannerService(IRecipePlannerStorage storage) {
            _storage = storage;
        }

        // **************** Grocery List *************

        public async Task<List<GroceryListItem>> GetGroceryListItemsForWeekAsync(
            DateOnly weekStartDate,
            CancellationToken ct = default
        ) {
            var recipeIds = await _storage.GetRecipeIdsForWeekAsync(weekStartDate, ct);
            if (recipeIds.Count == 0)
                return [];

            return await _storage.GetGroceryListItemsForRecipesAsync(recipeIds, ct);
        }



        // **************** Weekplan *****************

        public async Task<Weekplan> GetOrCreateWeekplanAsync(DateOnly weekStartDate, CancellationToken ct = default) {
            var weekplan = await _storage.GetWeekplanWithDaysByStartdateAsync(weekStartDate, ct);

            if (weekplan != null)
                return weekplan;


            var weekplanId = await _storage.AddWeekplanAsync(weekStartDate, ct);
            return (await _storage.GetWeekplanWithDaysByIdAsync(weekplanId))!;
        }

        public async Task SetPlannedDayAsync(
            DateOnly weekStartDate,
            DateOnly date,
            PrepTime availablePrepTime,
            int? recipeId,
            CancellationToken ct = default
        ) {
            var weekplan = await GetOrCreateWeekplanAsync(weekStartDate, ct);

            var existing = await _storage.GetPlannedDayByWeekplanAndDateAsync(weekplan.Id, date, ct);
            if (existing is null)
                await _storage.AddPlannedDayAsync(weekplan.Id, date, availablePrepTime, recipeId, ct);
            else
                await _storage.UpdatePlannedDayAsync(existing.Id, availablePrepTime, recipeId, ct);

        }

        //***************** Units *****************

        public async Task<List<Unit>> GetAllUnitsAsync() {
            return await _storage.GetAllUnitsAsync();
        }

        //***************** Ingredients *****************

        public async Task<List<IngredientListItem>> GetAllIngredientsAsync() {
            var rows = await _storage.GetAllIngredientsAsync();

            return rows
                .Select(r => new IngredientListItem(
                    r.Id,
                    r.Name,
                    r.DefaultUnitName,
                    r.CountForOverlap
                ))
                .ToList();
        }

        public async Task<Ingredient?> GetIngredientByIdAsync(int id, CancellationToken ct = default) {
            return await _storage.GetIngredientByIdAsync(id, ct);
        }
        public async Task<int> CreateIngredientAsync(
            string name,
            int defaultUnitId,
            bool countForOverlap = false,
            CancellationToken ct = default
        ) {
            name = name.Trim();
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required", nameof(name));

            return await _storage.AddIngredientAsync(name, defaultUnitId, countForOverlap, ct);
        }

        public async Task UpdateIngredientAsync(
            int id, string name,
            int defaultUnitId,
            bool countForOverlap = false,
            CancellationToken ct = default
        ) {
            name = name.Trim();
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            await _storage.UpdateIngredientAsync(id, name, defaultUnitId, countForOverlap, ct);
        }

        public async Task DeleteIngredientAsync(int id, CancellationToken ct = default) {
            await _storage.DeleteIngredientAsync(id, ct);
        }

        //***************** Recipes **********************

        public async Task<List<RecipeListItem>> GetAllRecipesAsync() {
            var rows = await _storage.GetAllRecipesAsync();

            return rows.Select(r => new RecipeListItem(
                r.Id,
                r.Name,
                r.Info,
                r.PrepTime
            )).ToList();
        }

        public async Task<Recipe?> GetRecipeByIdAsync(int id, CancellationToken ct = default) {
            return await _storage.GetRecipeByIdAsync(id, ct);
        }

        public async Task<int> CreateRecipeAsync(
            string name,
            PrepTime preptime,
            string info,
            CancellationToken ct = default
        ) {
            name = name.Trim();
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required", nameof(name));

            if (info == String.Empty)
                return await _storage.AddRecipeAsync(name, preptime, null, ct);

            return await _storage.AddRecipeAsync(name, preptime, info, ct);

        }

        public async Task UpdateRecipeAsync(
            int id,
            string name,
            PrepTime preptime,
            string info,
            CancellationToken ct = default
        ) {
            name = name.Trim();
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            await _storage.UpdateRecipeAsync(id, name, preptime, info, ct);
        }

        public async Task DeleteRecipeAsync(int id, CancellationToken ct = default) {
            await _storage.DeleteRecipeAsync(id, ct);
        }

        public async Task<List<RecipeSource>> GetRecipeSourcesForPlanningAsync(CancellationToken ct = default) {
            return await _storage.GetRecipeSourcesForPlanningAsync(ct);
        }

        //***************** Recipe Ingredients *************

        public async Task<List<RecipeIngredientListItem>> GetAllRecipeIngredientsAsync(int recipeId) {
            return await _storage.GetAllRecipeIngredientsAsync(recipeId);
        }

        public async Task<int> CreateRecipeIngredientAsync(
            int recipeId,
            int ingredientId,
            int unitId,
            decimal quantity,
            CancellationToken ct = default
        ) {
            return await _storage.AddRecipeIngredientAsync(
                recipeId,
                ingredientId,
                unitId,
                quantity,
                ct
            );
        }

        public async Task UpdateRecipeIngredientAsync(
            int recipeIngredientId,
            int ingredientId,
            int unitId,
            decimal quantity,
            CancellationToken ct = default
        ) {
            await _storage.UpdateRecipeIngredientAsync(
                recipeIngredientId,
                ingredientId,
                unitId,
                quantity,
                ct
            );
        }

        public async Task DeleteRecipeIngredientAsync(
            int recipeIngredientId,
            CancellationToken ct = default
        ) {
            await _storage.DeleteRecipeIngredientAsync(recipeIngredientId, ct);
        }

        //***************** Seed Data **********************
        public async Task SaveSeedDataAsync() {
            await _storage.SaveSeedDataAsync();
        }

    }
}
