namespace RecipePlanner.Contracts.RecipeIngredient {
    public sealed record RecipeIngredientListItem(
        int RecipeIngredientId,
        int IngredientId,
        string IngredientName,
        int UnitId,
        string UnitName,
        decimal Quantity
    );

}
