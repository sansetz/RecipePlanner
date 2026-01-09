namespace RecipePlanner.Contracts.PlannedDay {
    public sealed class DayContext {
        public List<RecipeChoiceItem> Recipes { get; set; } = new List<RecipeChoiceItem>();
        public int DayIndex { get; set; }
        public int? SelectedRecipeId { get; set; } = null;


        //later kijken of dit handig is
        //public DateOnly Day { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        //public int? PreSelectedRecipeId { get; set; } = null;
    }
}
