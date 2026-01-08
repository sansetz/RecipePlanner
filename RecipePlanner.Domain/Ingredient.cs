namespace RecipePlanner.Entities {
    public class Ingredient {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int DefaultUnitId { get; set; }
        public Unit DefaultUnit { get; set; } = null!;
        public bool CountForOverlap { get; set; } = false;
        public List<RecipeIngredient> RecipeIngredients { get; set; } = [];

    }
}
