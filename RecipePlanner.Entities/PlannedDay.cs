using RecipePlanner.Contracts.Recipe;

namespace RecipePlanner.Entities {
    public class PlannedDay {
        public int Id { get; set; }
        public required DateOnly Date { get; set; }
        public int WeekplanId { get; set; }
        public Weekplan Weekplan { get; set; } = null!;
        public required PrepTime AvailablePrepTime { get; set; }
        public int? RecipeId { get; set; }
        public Recipe? Recipe { get; set; }


    }
}
