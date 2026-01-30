using RecipePlanner.App;
using RecipePlanner.Contracts.Ingredient;
using RecipePlanner.Contracts.RecipeIngredient;
using RecipePlanner.Entities;

namespace RecipePlanner.UI {
    public partial class RecipeIngredientEditForm : Form {

        private readonly IngredientService _ingredientService;
        private readonly UnitService _unitService;
        private List<RecipeIngredientEditItem>? _recipeIngredients;
        private Guid? _uiId = null; // null = create, value = update

        public RecipeIngredientEditForm(
            IngredientService ingredientService,
            UnitService unitService
        ) {
            InitializeComponent();
            _ingredientService = ingredientService;
            _unitService = unitService;
        }

        //adds new ingredient to the list of ingredients
        public async Task ShowDialogForCreateAsync(
            List<RecipeIngredientEditItem> recipeIngredients,
            IWin32Window? owner = null
        ) {
            _recipeIngredients = recipeIngredients;
            _uiId = null;

            Text = "Nieuw ingredient aan recept toevoegen";

            await LoadIngredientAsync();
            await LoadUnitsAsync();

            IngredientSelector.SelectedIndex = -1;
            UnitSelector.SelectedIndex = -1;
            Quantity.Text = "";

            base.ShowDialog(owner);
        }

        //updates ingredient with IngredientId in the list of ingredients (replaces
        public async Task ShowDialogForUpdateAsync(
            List<RecipeIngredientEditItem> recipeIngredients,
            Guid uiId,
            IWin32Window? owner = null
        ) {
            _recipeIngredients = recipeIngredients;
            _uiId = uiId;

            Text = "Ingredient van recept bewerken";

            await LoadIngredientAsync();
            await LoadUnitsAsync();

            var item = _recipeIngredients.First(x => x.UiId == uiId);

            IngredientSelector.SelectedValue = item.IngredientId;
            UnitSelector.SelectedValue = item.UnitId;
            Quantity.Text = item.Quantity.ToString();

            base.ShowDialog(owner);
        }

        private void SaveIngredient_Click(object sender, EventArgs e) {
            try {
                if (_recipeIngredients == null)
                    throw new InvalidOperationException("Recipe ingredients list is null.");

                if (!ValidateForm())
                    return;

                var ingredientId = (int)IngredientSelector.SelectedValue!;
                var ingredientName = (IngredientSelector.SelectedItem as IngredientListItem)?.Name ?? "";
                var unitId = (int)UnitSelector.SelectedValue!;
                var unitName = (UnitSelector.SelectedItem as Unit)?.Name ?? "";

                if (!decimal.TryParse(Quantity.Text, out var quantity)) {
                    MessageBox.Show("Aantal is geen geldig getal.", "Fout");
                    Quantity.Focus();
                    return;
                }

                if (_uiId == null) {
                    // CREATE -> add new edit item
                    _recipeIngredients.Add(new RecipeIngredientEditItem {
                        RecipeIngredientId = null,
                        IngredientId = ingredientId,
                        IngredientName = ingredientName,
                        UnitId = unitId,
                        UnitName = unitName,
                        Quantity = quantity,
                        State = EditState.Added
                    });
                }
                else {
                    // UPDATE -> find by UiId and update
                    var item = _recipeIngredients.First(x => x.UiId == _uiId.Value);

                    item.IngredientId = ingredientId;
                    item.IngredientName = ingredientName;
                    item.UnitId = unitId;
                    item.UnitName = unitName;
                    item.Quantity = quantity;

                    if (item.State == EditState.Unchanged)
                        item.State = EditState.Modified;
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool ValidateForm() {

            if (IngredientSelector.SelectedValue is not int) {
                MessageBox.Show("Er is geen ingredient geselecteerd.", "Fout");
                IngredientSelector.Focus();
                return false;
            }
            if (UnitSelector.SelectedValue is not int) {
                MessageBox.Show("Er is geen eenheid geselecteerd.", "Fout");
                UnitSelector.Focus();
                return false;
            }

            return true;
        }

        private void Cancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            this.Close();

        }

        private async Task LoadIngredientAsync() {
            var ingredients = await _ingredientService.GetAllIngredientsForListAsync();
            IngredientSelector.DisplayMember = nameof(IngredientListItem.Name);
            IngredientSelector.ValueMember = nameof(IngredientListItem.Id);
            IngredientSelector.DataSource = ingredients;
            IngredientSelector.DropDownStyle = ComboBoxStyle.DropDown;
            IngredientSelector.AutoCompleteSource = AutoCompleteSource.ListItems;
            IngredientSelector.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }

        private async Task LoadUnitsAsync() {

            var units = await _unitService.GetAllUnitsAsync();
            UnitSelector.DisplayMember = nameof(Unit.Name);
            UnitSelector.ValueMember = nameof(Unit.Id);
            UnitSelector.DataSource = units;
            UnitSelector.DropDownStyle = ComboBoxStyle.DropDown;
            UnitSelector.AutoCompleteSource = AutoCompleteSource.ListItems;
            UnitSelector.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

        }

        private void IngredientSelector_SelectedIndexChanged(object sender, EventArgs e) {
            var selectedIngredient = IngredientSelector.SelectedItem as IngredientListItem;

            if (selectedIngredient == null)
                return;

            if (string.IsNullOrEmpty(selectedIngredient.DefaultUnitName))
                return;

            UnitSelector.SelectedIndex = UnitSelector.FindStringExact(selectedIngredient.DefaultUnitName);
            Quantity.Focus();

        }

        private void RecipeIngredientEditForm_Load(object sender, EventArgs e) {
            IngredientSelector.Focus();
        }
    }
}
