namespace RecipePlanner.Domain {
    public class RecipeIngredient {

        public int RecipeId { get; set; }
        public required Recipe Recipe { get; set; }
        public int IngredientId { get; set; }
        public required Ingredient Ingredient { get; set; }
        public int UnitId { get; set; }
        public required Unit Unit { get; set; }
        public decimal NumberOfUnits { get; set; }

    }
}
