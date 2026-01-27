using RecipePlanner.Data;
using RecipePlanner.Entities;

namespace RecipePlanner.App {
    public class UnitService {
        private readonly IRecipePlannerStorage _storage;

        public UnitService(IRecipePlannerStorage storage) {
            _storage = storage;
        }

        public async Task<List<Unit>> GetAllUnitsAsync() {
            return await _storage.GetAllUnitsAsync();
        }
    }
}
