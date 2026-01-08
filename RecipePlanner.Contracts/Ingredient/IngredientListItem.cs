namespace RecipePlanner.Contracts.Ingredient {
    public sealed record IngredientListItem(
        int Id,
        string Name,
        string? DefaultUnitName,
        bool CountForOverlap
    );

}

