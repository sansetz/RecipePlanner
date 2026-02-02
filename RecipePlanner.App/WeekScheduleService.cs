using RecipePlanner.Contracts.WeekSchedule;
using RecipePlanner.Data;
using System.Text;

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

        public async Task ExportWeekScheduleCsvAsync(int weekplanId, string filename, CancellationToken ct = default) {
            if (string.IsNullOrWhiteSpace(filename))
                throw new ArgumentException("Filename is required.", nameof(filename));

            var items = await GetWeekScheduleItemsAsync(weekplanId);

            var csv = BuildCsv(items);

            await File.WriteAllTextAsync(filename, csv, Encoding.UTF8, ct);
        }

        private static string BuildCsv(IReadOnlyList<WeekScheduleItem> items) {
            var sb = new StringBuilder();
            sb.AppendLine("Recipe Name;Info");

            foreach (var item in items)
                sb.AppendLine(ToCsvLine(item));

            return sb.ToString();
        }

        private static string ToCsvLine(WeekScheduleItem item)
            => $"{Escape(item.RecipeName)};{Escape(item.Info)}";

        private static string Escape(string? value) {
            value ??= string.Empty;

            var mustQuote =
                value.Contains(';') ||
                value.Contains('"') ||
                value.Contains('\n') ||
                value.Contains('\r');

            if (!mustQuote)
                return value;

            return $"\"{value.Replace("\"", "\"\"")}\"";
        }
    }

}

