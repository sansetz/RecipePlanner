namespace RecipePlanner.Entities {
    public class Weekplan {
        public int Id { get; set; }
        public required DateOnly WeekStartDate { get; set; }

        public List<PlannedDay> PlannedDays { get; set; } = new();
    }
}
