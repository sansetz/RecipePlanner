namespace RecipePlanner.Entities {
    public class RecipeIngredient {

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;
        public int UnitId { get; set; }
        public Unit Unit { get; set; } = null!;
        public decimal NumberOfUnits { get; set; }

    }
}
