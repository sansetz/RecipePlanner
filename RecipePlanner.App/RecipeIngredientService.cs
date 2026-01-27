using RecipePlanner.Contracts.RecipeIngredient;
using RecipePlanner.Data;

namespace RecipePlanner.App {
    public class RecipeIngredientService {
        private readonly IRecipePlannerStorage _storage;

        public RecipeIngredientService(IRecipePlannerStorage storage) {
            _storage = storage;
        }

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
    }
}
