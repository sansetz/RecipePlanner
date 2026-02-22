namespace RecipePlanner.Contracts.Filters {
    public record Filter {
        public int? IngredientId { get; init; }
        public bool? NoFreshIngredients { get; init; }
        public Filter(int? ingredientId, bool? noFreshIngredients) {
            IngredientId = ingredientId;
            NoFreshIngredients = noFreshIngredients;
        }
        public Filter() {
            IngredientId = null;
            NoFreshIngredients = null;
        }
    }
}
