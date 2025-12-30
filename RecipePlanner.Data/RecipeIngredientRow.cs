namespace RecipePlanner.Data {
    public sealed record RecipeIngredientRow(
        int IngredientId,
        string IngredientName,
        decimal Amount,
        string UnitName);
}
