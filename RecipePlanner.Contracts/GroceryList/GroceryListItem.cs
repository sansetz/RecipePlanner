namespace RecipePlanner.Contracts.GroceryList {
    public sealed record GroceryListItem(
        int IngredientId,
        string IngredientName,
        decimal TotalQuantity,
        string UnitName,
        bool CountForOverlap
    );
}
