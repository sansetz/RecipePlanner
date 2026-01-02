using RecipePlanner.Contracts.Ingredient;
using RecipePlanner.Contracts.Recipe;
using RecipePlanner.Data;

namespace RecipePlanner.App {
    public class RecipePlannerService {
        private readonly IRecipePlannerStorage _storage;

        public RecipePlannerService(IRecipePlannerStorage storage) {
            _storage = storage;
        }
        public async Task SaveSeedDataAsync() {
            await _storage.SaveSeedDataAsync();
        }
        public async Task<List<RecipeListItem>> GetAllRecipesAsync() {
            var rows = await _storage.GetAllRecipesAsync();

            return rows.Select(r => new RecipeListItem(
                    r.Id,
                    r.Name,
                    r.PrepTime
            )).ToList();
        }
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
    }
}
