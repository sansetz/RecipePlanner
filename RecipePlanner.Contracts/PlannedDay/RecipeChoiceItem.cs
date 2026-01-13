namespace RecipePlanner.Contracts.PlannedDay {
    public sealed record RecipeChoiceItem(
        int Id,
        string Name,
        bool HasOverlap,
        int OverlapCount,
        bool UsedInOtherDays,
        string? OverlapIngredientsText,
        string? UsedInDayName,
        string? InfoText
    );

}
