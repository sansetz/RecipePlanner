namespace RecipePlanner.Contracts.Recipe {
    public sealed record RecipeListItem(
        int Id,
        string Name,
        PrepTime? PrepTime
    );
}
