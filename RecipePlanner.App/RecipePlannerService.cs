using RecipePlanner.Data;
using RecipePlanner.Domain;

namespace RecipePlanner.App {
    public class RecipePlannerService {
        private readonly IRecipePlannerStorage _storage;

        public RecipePlannerService(IRecipePlannerStorage storage) {
            _storage = storage;
        }
        public async Task SaveSeedDataAsync() {
            await _storage.SaveSeedDataAsync();
        }
        public async Task<List<Recipe>> GetAllRecipesAsync() {
            return await _storage.GetAllRecipesAsync();
        }

        public async Task<List<Recipe>> GetOverlapRecipes(int recipeId) {

            var allRecipes = await _storage.GetAllRecipesAsync();

            var selectedIngredientIds = allRecipes
                .Where(r => r.Id == recipeId)
                .SelectMany(r => r.RecipeIngredients)
                .Select(ri => ri.IngredientId)
                .ToHashSet();

            return allRecipes
                .Where(r => r.Id != recipeId) // niet met zichzelf vergelijken
                .Where(r => r.RecipeIngredients
                    .Any(ri => selectedIngredientIds.Contains(ri.IngredientId)))
                .ToList();

        }
    }
}
