namespace RecipePlanner.Contracts.RecipeIngredient {
    public sealed class RecipeIngredientGridRow {
        public Guid UiId { get; init; }
        public string IngredientName { get; init; } = "";
        public string UnitName { get; init; } = "";
        public decimal Quantity { get; init; }
    }
}
