using Microsoft.Extensions.DependencyInjection;
using RecipePlanner.App;
using RecipePlanner.Contracts.Recipe;

namespace RecipePlanner.UI {

    public partial class RecipesForm : Form {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly RecipePlannerService _recipePlannerService;

        public RecipesForm(IServiceScopeFactory scopeFactory, RecipePlannerService recipePlannerService) {
            InitializeComponent();
            _scopeFactory = scopeFactory;
            _recipePlannerService = recipePlannerService;
        }

        private async void frmRecipes_LoadAsync(object sender, EventArgs e) {
            RecipesListView.SetColumnConfiguration(ExtraGridConfig);

            await LoadRecipesAsync();

            RecipesListView.AddClicked += RecipesListView_AddClickedAsync;
            RecipesListView.UpdateClicked += RecipesListView_UpdateClickedAsync;
            RecipesListView.DeleteClicked += RecipesListView_DeleteClickedAsync;

        }

        private async void RecipesListView_AddClickedAsync(object? sender, EventArgs e) {
            try {
                using var scope = _scopeFactory.CreateScope();
                var frm = scope.ServiceProvider.GetRequiredService<RecipeEditForm>();
                frm.ShowDialogForCreate(this);
                await LoadRecipesAsync();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void RecipesListView_UpdateClickedAsync(object? sender, EventArgs e) {
            try {
                if (sender is not EntityListViewControl list) {
                    throw new InvalidOperationException("Event sender is not EntityListViewControl.");
                }

                var recipeId = GetSelectedRecipeId(list);

                if (recipeId == null)
                    return;

                using var scope = _scopeFactory.CreateScope();
                var frm = scope.ServiceProvider.GetRequiredService<RecipeEditForm>();
                await frm.ShowDialogForUpdateAsync(recipeId.Value, this);

                await LoadRecipesAsync();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void RecipesListView_DeleteClickedAsync(object? sender, EventArgs e) {
            try {
                if (sender is not EntityListViewControl list) {
                    throw new InvalidOperationException("Event sender is not EntityListViewControl.");
                }

                var recipeId = GetSelectedRecipeId(list);
                if (recipeId == null)
                    return;

                await _recipePlannerService.DeleteRecipeAsync(recipeId.Value);
                await LoadRecipesAsync();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int? GetSelectedRecipeId(EntityListViewControl list) {

            var selected = list.SelectedItem;

            if (selected is not RecipeListItem item) {
                MessageBox.Show("Er is geen recept geselecteerd");
                return null;
            }

            return item.Id;
        }

        private async Task LoadRecipesAsync() {
            var recipes = await _recipePlannerService.GetAllRecipesAsync();
            RecipesListView.BindData(recipes);
        }
        private void ExtraGridConfig(DataGridView grid) {

            var infoColumn = grid.Columns["Info"];
            if (infoColumn != null) {
                infoColumn.Width = 500;
                infoColumn.HeaderText = "Info";
            }

            var defUnitColumn = grid.Columns["Preptime"];
            if (defUnitColumn != null) {
                defUnitColumn.HeaderText = "Bereidingstijd";
            }
            var nameColumn = grid.Columns["Name"];
            if (nameColumn != null) {
                nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                nameColumn.HeaderText = "Naam";
            }
        }
    }
}
