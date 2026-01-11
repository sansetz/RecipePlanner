using System.Globalization;

namespace RecipePlanner.App {
    public static class WeekDayHelper {

        public static string GetDayName(DayOfWeek dayOfWeek) {
            var cultureInfo = new CultureInfo("nl-NL");
            var dateTimeInfo = cultureInfo.DateTimeFormat;

            return dateTimeInfo.GetDayName(dayOfWeek);
        }
        public static DayOfWeek ToDayOfWeek(int dayIndex) {
            if (dayIndex < 0 || dayIndex > 6)
                throw new ArgumentOutOfRangeException(nameof(dayIndex));

            // DayOfWeek: Sunday = 0, Monday = 1, ...
            // Planner:   Monday = 0, Tuesday = 1, ...
            return (DayOfWeek)(((int)DayOfWeek.Monday + dayIndex) % 7);
        }

    }
}
