using RecipePlanner.Contracts.Ingredient;
using RecipePlanner.Data;
using RecipePlanner.Entities;

namespace RecipePlanner.App {
    public class IngredientService {
        private readonly IRecipePlannerStorage _storage;

        public IngredientService(IRecipePlannerStorage storage) {
            _storage = storage;
        }
        public async Task<List<IngredientListItem>> GetAllIngredientsForListAsync(CancellationToken ct = default) {
            var rows = await _storage.GetAllIngredientsForListAsync();

            return rows
                .Select(r => new IngredientListItem(
                    r.Id,
                    r.Name,
                    r.DefaultUnitName,
                    r.CountForOverlap
                ))
                .ToList();
        }

        public async Task<List<IngredientComboItem>> GetAllIngredientsForComboAsync(CancellationToken ct = default) {
            var rows = await _storage.GetAllIngredientsForListAsync();

            return rows
                .Select(r => new IngredientComboItem(
                    r.Id,
                    r.Name
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
    }
}
