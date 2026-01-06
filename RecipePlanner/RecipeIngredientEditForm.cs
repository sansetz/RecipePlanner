using RecipePlanner.App;
using RecipePlanner.Contracts.Ingredient;
using RecipePlanner.Contracts.RecipeIngredient;
using RecipePlanner.Entities;

namespace RecipePlanner.UI {
    public partial class RecipeIngredientEditForm : Form {
        private readonly RecipePlannerService _recipePlannerService;
        private List<RecipeIngredientListItem>? _recipeIngredients;
        private int? _ingredientId = null;

        public RecipeIngredientEditForm(
            RecipePlannerService recipePlannerService
        ) {
            InitializeComponent();
            _recipePlannerService = recipePlannerService;
        }

        //adds new ingredient to the list of ingredients
        public async Task ShowDialogForCreateAsync(List<RecipeIngredientListItem> recipeIngredients, IWin32Window? owner = null) {
            _recipeIngredients = recipeIngredients;
            _ingredientId = null;

            this.Text = "Nieuw ingredient aan recept toevoegen";
            await LoadIngredientAsync();
            await LoadUnitsAsync();
            IngredientSelector.SelectedIndex = -1;
            UnitSelector.SelectedIndex = -1;

            base.ShowDialog(owner);

        }

        //updates ingredient with IngredientId in the list of ingredients (replaces
        public async Task ShowDialogForUpdateAsync(List<RecipeIngredientListItem> recipeIngredients, int ingredientId, IWin32Window? owner = null) {

            _recipeIngredients = recipeIngredients;
            _ingredientId = ingredientId;

            this.Text = "Ingredient van recept bewerken";
            await LoadIngredientAsync();
            await LoadUnitsAsync();

            IngredientSelector.SelectedValue = ingredientId;



            base.ShowDialog(owner);
        }

        private void SaveIngredient_Click(object sender, EventArgs e) {
            try {
                if (_recipeIngredients == null)
                    throw new InvalidOperationException("Recipe ingredients list is null.");


                if (ValidateForm()) {
                    var ingredientId = (int)IngredientSelector.SelectedValue!; //validate already checked for null
                    var ingredientName = (IngredientSelector.SelectedItem as IngredientListItem)?.Name ?? "";
                    var unitId = (int)UnitSelector.SelectedValue!; //validate already checked for null
                    var unitName = (UnitSelector.SelectedItem as Unit)?.Name ?? "";
                    var quantity = decimal.Parse(Quantity.Text); //validate already checked for null and integer

                    if (_ingredientId == null) {
                        _recipeIngredients.Add(
                            new RecipeIngredientListItem(
                                ingredientId,
                                ingredientName,
                                null,
                                unitId,
                                unitName,
                                quantity
                            )
                        );
                    }
                    else {
                        var recipeIngredient = _recipeIngredients.FirstOrDefault(
                            x => x.IngredientId == _ingredientId
                        ) ?? throw new InvalidOperationException("Recipe ingredient to edit not found in the list.");

                        //remember old ingredient id only for first update
                        if (recipeIngredient.OldIngredientId == null)
                            recipeIngredient.OldIngredientId = recipeIngredient.IngredientId;

                        recipeIngredient.IngredientId = ingredientId;
                        recipeIngredient.UnitId = unitId;
                        recipeIngredient.Quantity = quantity;
                    }

                    DialogResult = DialogResult.OK;
                    this.Close();
                }
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
            var ingredients = await _recipePlannerService.GetAllIngredientsAsync();
            IngredientSelector.DisplayMember = nameof(IngredientListItem.Name);
            IngredientSelector.ValueMember = nameof(IngredientListItem.Id);
            IngredientSelector.DataSource = ingredients;
            IngredientSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            IngredientSelector.AutoCompleteSource = AutoCompleteSource.ListItems;
            IngredientSelector.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
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

        private void IngredientSelector_SelectedIndexChanged(object sender, EventArgs e) {
            var selectedIngredient = IngredientSelector.SelectedItem as IngredientListItem;

            if (selectedIngredient == null)
                return;

            if (string.IsNullOrEmpty(selectedIngredient.DefaultUnitName))
                return;

            UnitSelector.SelectedIndex = UnitSelector.FindStringExact(selectedIngredient.DefaultUnitName);
            Quantity.Focus();

        }
    }
}
