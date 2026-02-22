namespace RecipePlanner.Contracts.Filters {
    public class FilterChangedEventArgs : EventArgs {
        public FilterChangedEventArgs(int? ingredientId, bool? noFreshIngredients) {
            IngredientId = ingredientId;
            NoFreshIngredients = noFreshIngredients;
        }
        public int? IngredientId { get; }
        public bool? NoFreshIngredients { get; }
    }

    public delegate void FilterChangedEventHandler(object sender, FilterChangedEventArgs e);
}
