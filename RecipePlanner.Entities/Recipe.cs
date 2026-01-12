using RecipePlanner.Contracts.Recipe;

namespace RecipePlanner.Entities {
    public class Recipe {
        public int Id { get; set; }
        public required string Name { get; set; }
        public PrepTime PrepTime { get; set; }
        public string? Info { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; } = [];

    }
}
