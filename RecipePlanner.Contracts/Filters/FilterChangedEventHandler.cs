namespace RecipePlanner.Contracts.Filters {
    public class FilterChangedEventArgs : EventArgs {
        public FilterChangedEventArgs(int? ingredientId) {
            IngredientId = ingredientId;
        }
        public int? IngredientId { get; }
    }

    public delegate void FilterChangedEventHandler(object sender, FilterChangedEventArgs e);
}
