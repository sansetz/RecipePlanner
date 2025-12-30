namespace RecipePlanner.Domain {
    public class Ingredient {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int DefaultUnitId { get; set; }
        public Unit DefaultUnit { get; set; } = null!;
        public List<RecipeIngredient> RecipeIngredients { get; set; } = [];

    }
}
