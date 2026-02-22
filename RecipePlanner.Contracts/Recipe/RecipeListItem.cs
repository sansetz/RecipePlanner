namespace RecipePlanner.Contracts.Recipe {
    public enum PrepTime {
        Short,
        Medium,
        Large
    }

    public sealed record RecipeListItem(
        int Id,
        string Name,
        string? Info,
        PrepTime? PrepTime,
        bool NoFreshIngredients
    );
}
