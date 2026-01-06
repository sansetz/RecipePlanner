namespace RecipePlanner.Contracts.RecipeIngredient {

    public class RecipeIngredientListItem(int ingredientId, string ingredientName, int? oldIngredientId, int unitId, string unitName, decimal quantity) {
        public int IngredientId { get; set; } = ingredientId;
        public string IngredientName { get; set; } = ingredientName;
        public int? OldIngredientId { get; set; } = oldIngredientId;
        public int UnitId { get; set; } = unitId;
        public string UnitName { get; set; } = unitName;
        public decimal Quantity { get; set; } = quantity;
    }

}
