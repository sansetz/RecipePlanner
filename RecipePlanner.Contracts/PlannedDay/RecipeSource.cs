namespace RecipePlanner.Contracts.PlannedDay {

    public sealed record CountedIngredient(int Id, string Name);

    public sealed record RecipeSource(
        int Id,
        string Name,
        List<CountedIngredient> CountedIngredients
    );
}
