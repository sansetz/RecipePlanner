namespace RecipePlanner.Data {
    public sealed record RecipeRow(
    int Id,
    string Name,
    int? PrepTimeCategory,   // of string/enum, wat jij hebt
    List<RecipeIngredientRow> Ingredients);


}
