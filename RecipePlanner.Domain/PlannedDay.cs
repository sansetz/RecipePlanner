namespace RecipePlanner.Domain {
    public class PlannedDay {
        public int Id { get; set; }
        public required DateOnly Date { get; set; }
        public required Weekplan Weekplan { get; set; }
        public required PrepTime AvailablePrepTime { get; set; }
        public Recipe? Recipe { get; set; }


    }
}
