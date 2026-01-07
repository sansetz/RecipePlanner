namespace RecipePlanner.Contracts.RecipeIngredient {
    public enum EditState { Unchanged, Added, Modified, Deleted }

    public sealed class RecipeIngredientEditItem {

        public Guid UiId { get; } = Guid.NewGuid();
        public int? RecipeIngredientId { get; set; } // null = nog niet in DB (nieuw)
        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = "";
        public int UnitId { get; set; }
        public string UnitName { get; set; } = "";
        public decimal Quantity { get; set; }

        public EditState State { get; set; } = EditState.Added;
    }
}
