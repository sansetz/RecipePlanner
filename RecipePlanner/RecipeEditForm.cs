using Microsoft.Extensions.DependencyInjection;
using RecipePlanner.App;
using RecipePlanner.Contracts.Recipe;
using RecipePlanner.Contracts.RecipeIngredient;

namespace RecipePlanner.UI {
    public partial class RecipeEditForm : Form {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly RecipePlannerService _recipePlannerService;
        private int? _recipeId = null;
        private List<RecipeIngredientListItem>? _recipeIngredients;

        public RecipeEditForm(
            IServiceScopeFactory scopeFactory,
            RecipePlannerService recipePlannerService
        ) {
            InitializeComponent();
            _scopeFactory = scopeFactory;
            _recipePlannerService = recipePlannerService;
        }

        private void RecipeEditForm_Load(object sender, EventArgs e) {

            IngredientsListView.AddClicked += IngredientsListView_AddClickedAsync;
            IngredientsListView.UpdateClicked += IngredientsListView_UpdateClickedAsync;
            IngredientsListView.DeleteClicked += IngredientsListView_DeleteClickedAsync;

        }

        public void ShowDialogForCreate(IWin32Window? owner = null) {
            _recipeId = null;
            RecipeName.Clear();

            FillPreptimes();
            PrepTimeSelector.SelectedIndex = -1;

            this.Text = "Nieuw recept aanmaken";

            _recipeIngredients = new List<RecipeIngredientListItem>();

            base.ShowDialog(owner);
        }

        public async Task ShowDialogForUpdateAsync(int recipeId, IWin32Window? owner = null) {

            var recipe = await _recipePlannerService.GetRecipeByIdAsync(recipeId);

            if (recipe == null)
                throw new InvalidOperationException("Recipe not found in DB");

            _recipeId = recipe.Id;
            RecipeName.Text = recipe.Name;

            FillPreptimes();
            PrepTimeSelector.SelectedItem = recipe.PrepTime;
            this.Text = "Recept bewerken";
            IngredientsListView.SetColumnConfiguration(ExtraGridConfig);

            await LoadRecipeIngredientsAsync();

            base.ShowDialog(owner);
        }

        private void FillPreptimes() {
            PrepTimeSelector.DataSource = Enum.GetValues(typeof(PrepTime));
            PrepTimeSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            PrepTimeSelector.AutoCompleteSource = AutoCompleteSource.ListItems;
            PrepTimeSelector.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }

        private async void SaveRecipe_ClickAsync(object sender, EventArgs e) {
            try {
                if (!ValidateForm())
                    return;

                var prepTime = (PrepTime)PrepTimeSelector.SelectedValue!; //validate already checked for null

                await SaveRecipeToDB(RecipeName.Text, prepTime);

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
            if (string.IsNullOrWhiteSpace(RecipeName.Text)) {
                MessageBox.Show("Er is geen receptnaam ingevuld.", "Fout");
                RecipeName.Focus();
                return false;
            }

            if (PrepTimeSelector.SelectedValue is not PrepTime) {
                MessageBox.Show("Er is geen bereidingstijd geselecteerd.", "Fout");
                PrepTimeSelector.Focus();
                return false;
            }

            return true;
        }
        private async Task SaveRecipeToDB(string name, PrepTime preptime) {
            if (_recipeId == null) {
                _recipeId = await _recipePlannerService.CreateRecipeAsync(
                    name,
                    preptime
                );

            }
            else {
                await _recipePlannerService.UpdateRecipeAsync(
                    _recipeId.Value,
                    name,
                    preptime
                );
            }

            foreach (var ingredient in _recipeIngredients!) {
                if (ingredient.OldIngredientId != null) {
                    //ingredient is updated
                    await _recipePlannerService.UpdateRecipeIngredientAsync(
                        _recipeId.Value,
                        ingredient.OldIngredientId.Value,
                        ingredient.IngredientId,
                        ingredient.UnitId,
                        ingredient.Quantity
                    );
                }
                else {
                    //new ingredient
                    await _recipePlannerService.CreateRecipeIngredientAsync(
                        _recipeId.Value,
                        ingredient.IngredientId,
                        ingredient.UnitId,
                        ingredient.Quantity
                    );
                }
            }



        }
        private void Cancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void IngredientsListView_AddClickedAsync(object? sender, EventArgs e) {
            try {
                using var scope = _scopeFactory.CreateScope();
                var frm = scope.ServiceProvider.GetRequiredService<RecipeIngredientEditForm>();

                if (_recipeIngredients == null)
                    throw new InvalidOperationException("Recipe ingredients list is null.");

                await frm.ShowDialogForCreateAsync(_recipeIngredients, this);

                BindRecipeIngredients();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void IngredientsListView_UpdateClickedAsync(object? sender, EventArgs e) {
            try {
                if (sender is not EntityListViewControl list)
                    throw new InvalidOperationException("Event sender is not EntityListViewControl.");

                if (_recipeId == null)
                    throw new InvalidOperationException("Recipe ID is null.");

                if (_recipeIngredients == null)
                    throw new InvalidOperationException("Recipe ingredients list is null.");

                var ingredientId = GetSelectedIngredientId(list);
                if (ingredientId == null)
                    return;

                using var scope = _scopeFactory.CreateScope();
                var frm = scope.ServiceProvider.GetRequiredService<RecipeIngredientEditForm>();
                await frm.ShowDialogForUpdateAsync(_recipeIngredients, ingredientId.Value, this);

                BindRecipeIngredients();

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
                if (_recipeId == null)
                    throw new InvalidOperationException("Recipe ID is null.");

                var ingredientId = GetSelectedIngredientId(list);
                if (ingredientId == null)
                    return;

                await _recipePlannerService.DeleteRecipeIngredientAsync(_recipeId.Value, ingredientId.Value);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int? GetSelectedIngredientId(EntityListViewControl recipeingredient) {

            var selected = recipeingredient.SelectedItem;

            if (selected is not RecipeIngredientListItem item) {
                MessageBox.Show("Er is geen ingredient geselecteerd");
                return null;
            }

            return item.IngredientId;
        }

        private async Task LoadRecipeIngredientsAsync() {
            _recipeIngredients = new List<RecipeIngredientListItem>();

            if (_recipeId == null)
                throw new InvalidOperationException("Recipe ID is null.");
            _recipeIngredients = await _recipePlannerService.GetAllRecipeIngredientsAsync(_recipeId.Value);

            BindRecipeIngredients();
        }



        private void BindRecipeIngredients() {
            if (_recipeIngredients == null)
                throw new InvalidOperationException("Recipe ingredients list is null.");

            var recipeIngredients = _recipeIngredients.ToList();
            IngredientsListView.BindData(recipeIngredients);
        }

        private void ExtraGridConfig(DataGridView grid) {
            var idColumn = grid.Columns["OldIngredientId"];
            if (idColumn != null)
                idColumn.Visible = false;

            var unitIdColumn = grid.Columns["UnitId"];
            if (unitIdColumn != null)
                unitIdColumn.Visible = false;

            var ingredientIdColumn = grid.Columns["IngredientId"];
            if (ingredientIdColumn != null)
                ingredientIdColumn.Visible = false;

            var nameColumn = grid.Columns["IngredientName"];
            if (nameColumn != null) {
                nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                nameColumn.HeaderText = "Naam";
            }

            var defUnitColumn = grid.Columns["UnitName"];
            if (defUnitColumn != null) {
                defUnitColumn.HeaderText = "Eenheid";
            }
            var defQuantityColumn = grid.Columns["Quantity"];
            if (defQuantityColumn != null) {
                defQuantityColumn.HeaderText = "Aantal";
            }
        }
    }
}
