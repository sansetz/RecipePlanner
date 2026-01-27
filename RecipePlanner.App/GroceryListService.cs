using RecipePlanner.Contracts.GroceryList;
using RecipePlanner.Data;

namespace RecipePlanner.App {
    public class GroceryListService {
        private readonly IRecipePlannerStorage _storage;

        public GroceryListService(IRecipePlannerStorage storage) {
            _storage = storage;
        }

        public async Task<List<GroceryListItem>> GetGroceryListItemsForWeekAsync(
            DateOnly weekStartDate,
            CancellationToken ct = default
        ) {
            var recipeIds = await _storage.GetRecipeIdsForWeekAsync(weekStartDate, ct);
            if (recipeIds.Count == 0)
                return [];

            return await _storage.GetGroceryListItemsForRecipesAsync(recipeIds, ct);
        }

    }
}
