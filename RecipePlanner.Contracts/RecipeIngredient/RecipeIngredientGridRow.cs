namespace RecipePlanner.Contracts.RecipeIngredient {
    public sealed record RecipeIngredientGridRow(
        Guid UiId,
        string IngredientName,
        string UnitName,
        decimal Quantity
    );


}
