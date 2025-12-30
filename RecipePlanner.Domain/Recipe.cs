namespace RecipePlanner.Domain {
    public class Recipe {
        public int Id { get; set; }
        public required string Name { get; set; }
        public PrepTime PrepTime { get; set; }

        public List<RecipeIngredient> RecipeIngredients { get; set; } = [];

    }
}
