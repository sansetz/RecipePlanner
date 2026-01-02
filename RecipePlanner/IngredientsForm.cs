using RecipePlanner.App;
using RecipePlanner.Contracts.Ingredient;
using RecipePlanner.Data;
using RecipePlanner.Entities;

namespace RecipePlanner.UI {
    public partial class IngredientsForm : Form {
        private readonly IRecipePlannerDbContextFactory _dbFactory;
        private readonly RecipePlannerService _recipePlannerService;
        private Ingredient _currentIngredient;

        public IngredientsForm(IRecipePlannerDbContextFactory dbFactory, RecipePlannerService recipePlannerService) {
            InitializeComponent();
            _dbFactory = dbFactory;
            _recipePlannerService = recipePlannerService;
        }

        private async void IngredientsForm_LoadAsync(object sender, EventArgs e) {
            IngredientsGrid.AutoGenerateColumns = true;
            IngredientsGrid.ReadOnly = true;

            var units = await _recipePlannerService.GetAllUnitsAsync();
            UnitSelector.DisplayMember = nameof(Unit.Name);
            UnitSelector.ValueMember = nameof(Unit.Id);
            UnitSelector.DataSource = units;
            UnitSelector.SelectedIndex = -1;
            UnitSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            UnitSelector.AutoCompleteSource = AutoCompleteSource.ListItems;
            UnitSelector.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            var ingredients = await _recipePlannerService.GetAllIngredientsAsync();
            IngredientsGrid.DataSource = ingredients;
            IngredientsGrid.Rows[0].Selected = true;

            var idColumn = IngredientsGrid.Columns["Id"];
            if (idColumn != null)
                idColumn.Visible = false;
        }

        private void NewIngredient_Click(object sender, EventArgs e) {
            IngredientsGrid.Enabled = false;
            IngredientsGrid.ClearSelection();
            _currentIngredient = new Ingredient { Name = "" };
            IngredientName.Clear();
            IngredientName.Focus();
            UnitSelector.SelectedIndex = -1;
        }

        private async void SaveIngredient_ClickAsync(object sender, EventArgs e) {
            if (_currentIngredient is null) return;

            _currentIngredient.Name = IngredientName.Text.Trim();

            if (string.IsNullOrWhiteSpace(_currentIngredient.Name))
                return; // of toon messagebox

            if (_currentIngredient.Id == 0) {
                await _recipePlannerService.CreateIngredientAsync(
                    _currentIngredient.Name,
                    _currentIngredient.DefaultUnitId
                );
            }
            else {
                await _recipePlannerService.UpdateIngredientAsync(
                    _currentIngredient.Id,
                    _currentIngredient.Name,
                    _currentIngredient.DefaultUnitId
                );
            }

            // refresh grid (en eventueel selectie herstellen)
            IngredientsGrid.Enabled = true;

            var ingredients = await _recipePlannerService.GetAllIngredientsAsync();
            IngredientsGrid.DataSource = ingredients;
        }

        private async void IngredientsGrid_SelectionChangedAsync(object sender, EventArgs e) {
            if (IngredientsGrid.CurrentRow?.DataBoundItem is not IngredientListItem row)
                return;

            // Haal de entity op via service (of storage via service)
            _currentIngredient = await _recipePlannerService.GetIngredientByIdAsync(row.Id);

            if (_currentIngredient is null)
                return;

            IngredientName.Text = _currentIngredient.Name;

            UnitSelector.SelectedValue = _currentIngredient.DefaultUnitId;

        }

        private void IngredientName_TextChanged(object sender, EventArgs e) {
            _currentIngredient.Name = IngredientName.Text;
        }

        private void UnitSelector_SelectedIndexChanged(object sender, EventArgs e) {
            if (_currentIngredient == null) return;

            _currentIngredient.DefaultUnitId = UnitSelector.SelectedValue as int?;

        }
    }
}
