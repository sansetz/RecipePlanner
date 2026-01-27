using Microsoft.Extensions.DependencyInjection;
using RecipePlanner.App;
using RecipePlanner.Contracts.Ingredient;

namespace RecipePlanner.UI {
    public partial class IngredientsForm : Form {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IngredientService _ingredientService;

        public IngredientsForm(IServiceScopeFactory scopeFactory, IngredientService ingredientService) {
            InitializeComponent();
            _scopeFactory = scopeFactory;
            _ingredientService = ingredientService;
        }
        private async void IngredientsForm_LoadAsync(object sender, EventArgs e) {
            IngredientsListView.SetColumnConfiguration(ExtraGridConfig);

            await LoadIngredientsAsync();

            IngredientsListView.AddClicked += IngredientsListView_AddClickedAsync;
            IngredientsListView.UpdateClicked += IngredientsListView_UpdateClickedAsync;
            IngredientsListView.DeleteClicked += IngredientsListView_DeleteClickedAsync;
        }

        private async void IngredientsListView_AddClickedAsync(object? sender, EventArgs e) {
            try {
                using var scope = _scopeFactory.CreateScope();
                var frm = scope.ServiceProvider.GetRequiredService<IngredientEditForm>();
                await frm.ShowDialogForCreateAsync(this);
                await LoadIngredientsAsync();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void IngredientsListView_UpdateClickedAsync(object? sender, EventArgs e) {
            try {
                if (sender is not EntityListViewControl list) {
                    throw new InvalidOperationException("Event sender is not EntityListViewControl.");
                }

                var ingredientId = GetSelectedIngredientId(list);

                if (ingredientId == null)
                    return;

                using var scope = _scopeFactory.CreateScope();
                var frm = scope.ServiceProvider.GetRequiredService<IngredientEditForm>();
                await frm.ShowDialogForUpdateAsync(ingredientId.Value, this);

                await LoadIngredientsAsync();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void IngredientsListView_DeleteClickedAsync(object? sender, EventArgs e) {
            try {
                if (sender is not EntityListViewControl list) {
                    throw new InvalidOperationException("Event sender is not EntityListViewControl.");
                }

                var ingredientId = GetSelectedIngredientId(list);
                if (ingredientId == null)
                    return;

                await _ingredientService.DeleteIngredientAsync(ingredientId.Value);
                await LoadIngredientsAsync();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int? GetSelectedIngredientId(EntityListViewControl list) {

            var selected = list.SelectedItem;

            if (selected is not IngredientListItem item) {
                MessageBox.Show("Er is geen ingredient geselecteerd");
                return null;
            }

            return item.Id;
        }

        private async Task LoadIngredientsAsync() {
            var ingredients = await _ingredientService.GetAllIngredientsAsync();
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
