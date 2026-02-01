using RecipePlanner.Contracts.WeekSchedule;
using RecipePlanner.Data;

namespace RecipePlanner.App {

    public class WeekScheduleService {
        private readonly IRecipePlannerStorage _storage;

        public WeekScheduleService(IRecipePlannerStorage storage) {
            _storage = storage;
        }

        public async Task<List<WeekScheduleItem>> GetWeekScheduleItemsAsync(
           int WeekPlanId,
           CancellationToken ct = default
       ) {
            return await _storage.GetWeekScheduleItemsAsync(WeekPlanId, ct);

        }
    }
}
