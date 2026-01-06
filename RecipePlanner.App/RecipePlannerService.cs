using RecipePlanner.Contracts.Ingredient;
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
                    r.DefaultUnitName
                ))
                .ToList();
        }

        public async Task<Ingredient?> GetIngredientByIdAsync(int id, CancellationToken ct = default) {
            return await _storage.GetIngredientByIdAsync(id, ct);
        }
        public async Task<int> CreateIngredientAsync(
            string name,
            int defaultUnitId,
            CancellationToken ct = default
        ) {
            name = name.Trim();
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required", nameof(name));

            return await _storage.AddIngredientAsync(name, defaultUnitId, ct);
        }

        public async Task UpdateIngredientAsync(
            int id, string name,
            int defaultUnitId,
            CancellationToken ct = default
        ) {
            name = name.Trim();
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            await _storage.UpdateIngredientAsync(id, name, defaultUnitId, ct);
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
                r.PrepTime
            )).ToList();
        }

        public async Task<Recipe?> GetRecipeByIdAsync(int id, CancellationToken ct = default) {
            return await _storage.GetRecipeByIdAsync(id, ct);
        }

        public async Task<int> CreateRecipeAsync(
            string name,
            PrepTime preptime,
            CancellationToken ct = default
        ) {
            name = name.Trim();
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required", nameof(name));

            return await _storage.AddRecipeAsync(name, preptime, ct);
        }

        public async Task UpdateRecipeAsync(
            int id,
            string name,
            PrepTime preptime,
            CancellationToken ct = default
        ) {
            name = name.Trim();
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            await _storage.UpdateRecipeAsync(id, name, preptime, ct);
        }

        public async Task DeleteRecipeAsync(int id, CancellationToken ct = default) {
            await _storage.DeleteRecipeAsync(id, ct);
        }

        public async Task<List<RecipeListItem>> GetOverlapRecipes(int recipeId) {

            return await _storage.GetAllRecipesAsync();

            //todo: dit is niet meer goed, moet aangepast worden, maar nu nog niet nodig

            //var selectedIngredientIds = allRecipes
            //    .Where(r => r.Id == recipeId)
            //    .SelectMany(r => r.RecipeIngredients)
            //    .Select(ri => ri.IngredientId)
            //    .ToHashSet();

            //return allRecipes
            //    .Where(r => r.Id != recipeId) // niet met zichzelf vergelijken
            //    .Where(r => r.RecipeIngredients
            //        .Any(ri => selectedIngredientIds.Contains(ri.IngredientId)))
            //    .ToList();

        }

        //***************** Recipe Ingredients *************
        public async Task<List<RecipeIngredientListItem>> GetAllRecipeIngredientsAsync(int recipeId) {
            var rows = await _storage.GetAllRecipeIngredientsAsync(recipeId);

            return rows.Select(r => new RecipeIngredientListItem(
                r.IngredientId,
                r.IngredientName,
                r.IngredientId, //load from DB so oldIngredientId is same as IngredientId
                r.UnitId,
                r.UnitName,
                r.Quantity
            )).ToList();
        }

        public async Task<int> CreateRecipeIngredientAsync(
            int recipeId,
            int ingredientId,
            int unitId,
            decimal quantity,
            CancellationToken ct = default
        ) {
            return await _storage.AddRecipeIngredientAsync(recipeId, ingredientId, unitId, quantity, ct);
        }

        public async Task UpdateRecipeIngredientAsync(
            int recipeId,
            int oldingredientId,
            int newingredientId,
            int unitId,
            decimal quantity,
            CancellationToken ct = default
        ) {
            await _storage.UpdateRecipeIngredientAsync(
                recipeId,
                oldingredientId,
                newingredientId,
                unitId,
                quantity,
                ct
             );
        }

        public async Task DeleteRecipeIngredientAsync(
            int recipeId,
            int ingredientId,
            CancellationToken ct = default
        ) {
            await _storage.DeleteRecipeIngredientAsync(recipeId, ingredientId, ct);
        }


        //***************** Seed Data **********************
        public async Task SaveSeedDataAsync() {
            await _storage.SaveSeedDataAsync();
        }

    }
}
