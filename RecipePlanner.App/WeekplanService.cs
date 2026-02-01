using RecipePlanner.Contracts.Recipe;
using RecipePlanner.Data;
using RecipePlanner.Entities;

namespace RecipePlanner.App {

    public class WeekplanService {
        private readonly IRecipePlannerStorage _storage;

        public WeekplanService(IRecipePlannerStorage storage) {
            _storage = storage;
        }

        public async Task<Weekplan> GetOrCreateWeekplanAsync(DateOnly weekStartDate, CancellationToken ct = default) {
            var weekplan = await _storage.GetWeekplanWithDaysByStartdateAsync(weekStartDate, ct);

            if (weekplan != null)
                return weekplan;


            var weekplanId = await _storage.AddWeekplanAsync(weekStartDate, ct);
            return (await _storage.GetWeekplanWithDaysByIdAsync(weekplanId))!;
        }

        public async Task<int?> GetWeekplanIdForDate(DateOnly date, CancellationToken ct = default) {

            var weekplan = await _storage.GetWeekplanWithDaysByStartdateAsync(date, ct);


            return weekplan == null ? null : weekplan.Id;
        }

        public async Task SetPlannedDayAsync(
            DateOnly weekStartDate,
            DateOnly date,
            PrepTime availablePrepTime,
            int? recipeId,
            CancellationToken ct = default
        ) {
            var weekplan = await GetOrCreateWeekplanAsync(weekStartDate, ct);

            var existing = await _storage.GetPlannedDayByWeekplanAndDateAsync(weekplan.Id, date, ct);
            if (existing is null)
                await _storage.AddPlannedDayAsync(weekplan.Id, date, availablePrepTime, recipeId, ct);
            else
                await _storage.UpdatePlannedDayAsync(existing.Id, availablePrepTime, recipeId, ct);

        }
    }
}
