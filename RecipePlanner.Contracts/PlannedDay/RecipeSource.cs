namespace RecipePlanner.Contracts.PlannedDay {
    public sealed record RecipeSource(int Id, string Name, List<int> CountedIngredientIds);
}
