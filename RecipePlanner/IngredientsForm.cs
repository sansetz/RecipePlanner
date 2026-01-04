using Microsoft.Extensions.DependencyInjection;
using RecipePlanner.App;
using RecipePlanner.Contracts.Ingredient;
using RecipePlanner.Data;

namespace RecipePlanner.UI {
    public partial class IngredientsForm : Form {
        private readonly IRecipePlannerDbContextFactory _dbFactory;
        private readonly RecipePlannerService _recipePlannerService;

        public IngredientsForm(IRecipePlannerDbContextFactory dbFactory, RecipePlannerService recipePlannerService) {
            InitializeComponent();
            _dbFactory = dbFactory;
            _recipePlannerService = recipePlannerService;
        }

        private async void IngredientsForm_LoadAsync(object sender, EventArgs e) {
            IngredientsGrid.AutoGenerateColumns = true;
            IngredientsGrid.ReadOnly = true;

            UpdateIngredient.Enabled = false;
            await LoadIngredients();
            ConfigIngredientsGrid();
        }

        private async Task LoadIngredients() {
            var ingredients = await _recipePlannerService.GetAllIngredientsAsync();
            IngredientsGrid.DataSource = ingredients;
        }
        private void ConfigIngredientsGrid() {
            IngredientsGrid.Rows[0].Selected = true;

            var idColumn = IngredientsGrid.Columns["Id"];
            if (idColumn != null)
                idColumn.Visible = false;

        }

        private async void NewIngredient_ClickAsync(object sender, EventArgs e) {
            using var scope = Program.ServiceProvider.CreateScope();
            var frm = scope.ServiceProvider.GetRequiredService<IngredientEditForm>();
            frm.ShowDialogForCreate(this);
            await LoadIngredients();

        }

        private async void SaveIngredient_ClickAsync(object sender, EventArgs e) {
            var ingredients = await _recipePlannerService.GetAllIngredientsAsync();
            IngredientsGrid.DataSource = ingredients;
        }

        private async void IngredientsGrid_SelectionChangedAsync(object sender, EventArgs e) {
            if (IngredientsGrid.CurrentRow?.DataBoundItem is not IngredientListItem row)
                return;

            UpdateIngredient.Enabled = true;


        }


        private async void UpdateIngredient_ClickAsync(object sender, EventArgs e) {
            if (IngredientsGrid.CurrentRow?.DataBoundItem is not IngredientListItem row)
                return;

            using var scope = Program.ServiceProvider.CreateScope();
            var frm = scope.ServiceProvider.GetRequiredService<IngredientEditForm>();
            await frm.ShowDialogForUpdateAsync(row.Id, this);

            await LoadIngredients();

        }
    }
}
