namespace RecipePlanner.Domain {
    public class Ingredient {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required Unit DefaultUnit { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; } = [];

    }
}
