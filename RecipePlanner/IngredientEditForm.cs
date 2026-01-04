using RecipePlanner.App;
using RecipePlanner.Data;
using RecipePlanner.Entities;

namespace RecipePlanner.UI {
    public partial class IngredientEditForm : Form {

        private readonly IRecipePlannerDbContextFactory _dbFactory;
        private readonly RecipePlannerService _recipePlannerService;
        private Ingredient _ingredient;

        public IngredientEditForm(
            IRecipePlannerDbContextFactory dbFactory,
            RecipePlannerService recipePlannerService
        ) {
            InitializeComponent();
            _dbFactory = dbFactory;
            _recipePlannerService = recipePlannerService;
        }

        public void ShowDialogForCreate(IWin32Window? owner = null) {
            _ingredient = new Ingredient { Name = "" };
            IngredientName.Clear();
            base.ShowDialog(owner);
        }

        public async Task ShowDialogForUpdateAsync(int IngredientId, IWin32Window? owner = null) {
            if (_recipePlannerService == null)
                throw new InvalidOperationException("RecipePlannerService is not initialized.");

            _ingredient = await _recipePlannerService.GetIngredientByIdAsync(IngredientId);

            if (_ingredient == null)
                throw new InvalidOperationException("Ingredient to update not found");

            IngredientName.Text = _ingredient.Name;
            UnitSelector.SelectedValue = _ingredient.DefaultUnitId;
            UnitSelector.SelectedText = _ingredient.DefaultUnit?.Name;
            UnitSelector.SelectedItem = _ingredient.DefaultUnit;


            base.ShowDialog(owner);

        }

        private async Task LoadUnitsAsync() {

            var units = await _recipePlannerService.GetAllUnitsAsync();
            UnitSelector.DisplayMember = nameof(Unit.Name);
            UnitSelector.ValueMember = nameof(Unit.Id);
            UnitSelector.DataSource = units;
            UnitSelector.SelectedIndex = -1;
            UnitSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            UnitSelector.AutoCompleteSource = AutoCompleteSource.ListItems;
            UnitSelector.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

        }

        private async void IngredientEditForm_LoadAsync(object sender, EventArgs e) {
            await LoadUnitsAsync();
        }

        private async void SaveIngredient_ClickAsync(object sender, EventArgs e) {
            await SaveIngredientToDB();
            this.Close();
        }

        private async Task SaveIngredientToDB() {
            if (_ingredient.Id == 0) {
                await _recipePlannerService.CreateIngredientAsync(
                    IngredientName.Text,
                    UnitSelector.SelectedValue as int?
                );
            }
            else {
                await _recipePlannerService.UpdateIngredientAsync(
                    _ingredient.Id,
                    IngredientName.Text,
                    UnitSelector.SelectedValue as int?
                );
            }

        }

        private void Cancel_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
