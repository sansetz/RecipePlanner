namespace RecipePlanner.Contracts.RecipeIngredient {
    public class RecipeIngredientListItem(
        int recipeIngredientId,
        int ingredientId,
        string ingredientName,
        int unitId,
        string unitName,
        decimal quantity
    ) {
        public int RecipeIngredientId { get; set; } = recipeIngredientId;

        public int IngredientId { get; set; } = ingredientId;
        public string IngredientName { get; set; } = ingredientName;

        public int UnitId { get; set; } = unitId;
        public string UnitName { get; set; } = unitName;

        public decimal Quantity { get; set; } = quantity;
    }

}
