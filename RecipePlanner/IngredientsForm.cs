using Microsoft.Extensions.DependencyInjection;
using RecipePlanner.App;
using RecipePlanner.Contracts.Ingredient;
using RecipePlanner.Data;

namespace RecipePlanner.UI {
    public partial class IngredientsForm : Form {
        private readonly RecipePlannerService _recipePlannerService;

        public IngredientsForm(IRecipePlannerDbContextFactory dbFactory, RecipePlannerService recipePlannerService) {
            InitializeComponent();
            _recipePlannerService = recipePlannerService;
        }

        private async void IngredientsForm_LoadAsync(object sender, EventArgs e) {
            await LoadIngredients();

            IngredientsListView.SetColumnConfiguration(ExtraGridConfig);

            IngredientsListView.AddClicked += IngredientsListView_AddClickedAsync;
            IngredientsListView.UpdateClicked += IngredientsListView_UpdateClickedAsync;
            IngredientsListView.DeleteClicked += IngredientsListView_DeleteClickedAsync;
        }

        private async void IngredientsListView_AddClickedAsync(object? sender, EventArgs e) {
            using var scope = Program.ServiceProvider.CreateScope();
            var frm = scope.ServiceProvider.GetRequiredService<IngredientEditForm>();
            await frm.ShowDialogForCreateAsync(this);
            await LoadIngredients();
        }
        private async void IngredientsListView_UpdateClickedAsync(object? sender, EventArgs e) {
            var ingredientId = GetSelectedIngredientId(sender);

            using var scope = Program.ServiceProvider.CreateScope();
            var frm = scope.ServiceProvider.GetRequiredService<IngredientEditForm>();
            await frm.ShowDialogForUpdateAsync(ingredientId, this);

            await LoadIngredients();
        }

        private int GetSelectedIngredientId(object? ingredient) {
            if (ingredient is null)
                throw new InvalidOperationException("No ingredient selected.");

            var selected = ((EntityListViewControl)ingredient).SelectedItem;

            if (selected == null || selected is not IngredientListItem item)
                throw new InvalidOperationException("No ingredient selected.");

            return item.Id;
        }

        private async void IngredientsListView_DeleteClickedAsync(object? sender, EventArgs e) {
            var ingredientId = GetSelectedIngredientId(sender);

            await _recipePlannerService.DeleteIngredientAsync(ingredientId);
            await LoadIngredients();


        }

        private async Task LoadIngredients() {
            var ingredients = await _recipePlannerService.GetAllIngredientsAsync();
            IngredientsListView.BindData(ingredients);
        }
        private void ExtraGridConfig(DataGridView grid) {

            var nameColumn = grid.Columns["Name"];
            if (nameColumn != null) {
                nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                nameColumn.HeaderText = "Naam";
            }

            var defUnitColumn = grid.Columns["DefaultUnitName"];
            if (defUnitColumn != null) {
                defUnitColumn.HeaderText = "Eenheid";
            }

        }



    }
}
