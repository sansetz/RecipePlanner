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
            var recipeCounts = await _storage.GetRecipeCountsForWeekAsync(weekStartDate, ct);
            if (recipeCounts.Count == 0)
                return [];

            return await _storage.GetGroceryListItemsForRecipesAsync(recipeCounts, ct);
        }
    }
}
