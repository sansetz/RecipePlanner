using RecipePlanner.Data;

namespace RecipePlanner.App {
    public class DevToolsService {
        private readonly IRecipePlannerStorage _storage;

        public DevToolsService(IRecipePlannerStorage storage) {
            _storage = storage;
        }

        public async Task SaveSeedDataAsync() {
            await _storage.SaveSeedDataAsync();
        }

    }
}
