using RecipePlanner.Contracts.Ingredient;
using RecipePlanner.Contracts.Recipe;
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
            int? defaultUnitId,
            CancellationToken ct = default
        ) {
            name = name.Trim();
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            var ingredient = new Ingredient {
                Name = name,
                DefaultUnitId = defaultUnitId
            };

            return await _storage.AddIngredientAsync(ingredient, ct);
        }

        public async Task UpdateIngredientAsync(int id, string name, int? defaultUnitId, CancellationToken ct = default) {
            name = name.Trim();
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            //for update ingredient should exist
            var ingredient = await _storage.GetIngredientByIdAsync(id, ct) ??
                throw new ArgumentException("Ingredient not found.", nameof(id));

            ingredient.Name = name;
            ingredient.DefaultUnitId = defaultUnitId;
            await _storage.UpdateIngredientAsync(ingredient, ct);
        }

        public async Task DeleteIngredientAsync(int id, CancellationToken ct = default) {
            //for delete ingredient should exist
            var ingredient = await _storage.GetIngredientByIdAsync(id, ct) ??
                throw new ArgumentException("Ingredient not found.", nameof(id));

            await _storage.DeleteIngredientAsync(ingredient, ct);
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


        public async Task<List<RecipeListItem>> GetOverlapRecipes(int recipeId) {

            return await _storage.GetAllRecipesAsync();

            //todo: dit is niet goed, moet aangepast worden

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

        //***************** Seed Data **********************
        public async Task SaveSeedDataAsync() {
            await _storage.SaveSeedDataAsync();
        }

    }
}
