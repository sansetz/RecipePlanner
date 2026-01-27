using RecipePlanner.Data;

namespace RecipePlanner.App {
    public class RecipePlannerService {
        private readonly IRecipePlannerStorage _storage;

        public RecipePlannerService(IRecipePlannerStorage storage) {
            _storage = storage;
        }

    }
}
