namespace RecipePlanner.Domain {
    public class RecipeIngredient {

        public required int RecipeId { get; set; }
        public required int IngredientId { get; set; }
        public required Unit Unit { get; set; }
        public required int NumberOfUnits { get; set; }

    }
}
