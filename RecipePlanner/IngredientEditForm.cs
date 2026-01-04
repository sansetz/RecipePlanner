using RecipePlanner.App;
using RecipePlanner.Data;
using RecipePlanner.Entities;

namespace RecipePlanner.UI {
    public partial class IngredientEditForm : Form {

        private readonly RecipePlannerService _recipePlannerService;
        //private Ingredient _ingredient = null!;
        private int _ingredientId = -1;

        public IngredientEditForm(
            IRecipePlannerDbContextFactory dbFactory,
            RecipePlannerService recipePlannerService
        ) {
            InitializeComponent();
            _recipePlannerService = recipePlannerService;
        }

        public async Task ShowDialogForCreateAsync(IWin32Window? owner = null) {
            IngredientName.Clear();

            await LoadUnitsAsync();
            UnitSelector.SelectedIndex = -1;

            base.ShowDialog(owner);
        }

        public async Task ShowDialogForUpdateAsync(int IngredientId, IWin32Window? owner = null) {
            if (_recipePlannerService == null)
                throw new InvalidOperationException("RecipePlannerService is not initialized.");

            var ingredient = await _recipePlannerService.GetIngredientByIdAsync(IngredientId);

            if (ingredient == null)
                throw new InvalidOperationException("Ingredient to update not found");

            _ingredientId = ingredient.Id;
            IngredientName.Text = ingredient.Name;

            await LoadUnitsAsync();

            if (ingredient.DefaultUnitId == null)
                UnitSelector.SelectedIndex = -1;
            else
                UnitSelector.SelectedValue = ingredient.DefaultUnitId;

            base.ShowDialog(owner);

        }

        private async Task LoadUnitsAsync() {

            var units = await _recipePlannerService.GetAllUnitsAsync();
            UnitSelector.DisplayMember = nameof(Unit.Name);
            UnitSelector.ValueMember = nameof(Unit.Id);
            UnitSelector.DataSource = units;
            UnitSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            UnitSelector.AutoCompleteSource = AutoCompleteSource.ListItems;
            UnitSelector.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

        }

        private async void SaveIngredient_ClickAsync(object sender, EventArgs e) {
            await SaveIngredientToDB();
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private async Task SaveIngredientToDB() {
            var unitId = UnitSelector.SelectedValue is int id ? (int?)id : null;

            if (_ingredientId == -1) {
                await _recipePlannerService.CreateIngredientAsync(
                    IngredientName.Text,
                    unitId
                );
            }
            else {
                await _recipePlannerService.UpdateIngredientAsync(
                    _ingredientId,
                    IngredientName.Text,
                    unitId
                );
            }

        }

        private void Cancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
