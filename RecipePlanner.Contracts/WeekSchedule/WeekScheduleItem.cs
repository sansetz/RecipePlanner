namespace RecipePlanner.Contracts.WeekSchedule {
    public sealed record WeekScheduleItem(
        int RecipeId,
        string RecipeName,
        string? Info,
        DateOnly Date
    );
}
