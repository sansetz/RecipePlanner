using RecipePlanner.App;
using RecipePlanner.Entities;

namespace RecipePlanner.UI {
    public partial class IngredientEditForm : Form {

        private readonly RecipePlannerService _recipePlannerService;
        private int? _ingredientId = null;

        public IngredientEditForm(
            RecipePlannerService recipePlannerService
        ) {
            InitializeComponent();
            _recipePlannerService = recipePlannerService;
        }

        public async Task ShowDialogForCreateAsync(IWin32Window? owner = null) {
            _ingredientId = null;
            IngredientName.Clear();

            await LoadUnitsAsync();
            UnitSelector.SelectedIndex = -1;

            base.ShowDialog(owner);
        }

        public async Task ShowDialogForUpdateAsync(int ingredientId, IWin32Window? owner = null) {

            var ingredient = await _recipePlannerService.GetIngredientByIdAsync(ingredientId);

            if (ingredient == null)
                throw new InvalidOperationException("Ingredient not found in DB");

            _ingredientId = ingredient.Id;
            IngredientName.Text = ingredient.Name;

            await LoadUnitsAsync();

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
            try {
                if (!ValidateForm())
                    return;

                var unitId = (int)UnitSelector.SelectedValue!; //validate already checked for null

                await SaveIngredientToDB(IngredientName.Text, unitId);

                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) {
                MessageBox.Show(
                    ex.Message,
                    "Fout",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private bool ValidateForm() {
            if (string.IsNullOrWhiteSpace(IngredientName.Text)) {
                MessageBox.Show("Er is geen ingrediëntnaam ingevuld.", "Fout");
                IngredientName.Focus();
                return false;
            }

            if (UnitSelector.SelectedValue is not int) {
                MessageBox.Show("Er is geen eenheid geselecteerd.", "Fout");
                UnitSelector.Focus();
                return false;
            }

            return true;
        }
        private async Task SaveIngredientToDB(string name, int unitId) {

            if (_ingredientId == null) {
                await _recipePlannerService.CreateIngredientAsync(
                    name,
                    unitId
                );
            }
            else {
                await _recipePlannerService.UpdateIngredientAsync(
                    _ingredientId.Value,
                    name,
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
