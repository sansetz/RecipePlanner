using RecipePlanner.Contracts.PlannedDay;
using RecipePlanner.Contracts.Recipe;
using RecipePlanner.Data;
using RecipePlanner.Entities;

namespace RecipePlanner.App {
    public class RecipeService {
        private readonly IRecipePlannerStorage _storage;

        public RecipeService(IRecipePlannerStorage storage) {
            _storage = storage;
        }

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

    }
}
