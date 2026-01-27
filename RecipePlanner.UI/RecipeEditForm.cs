using Microsoft.Extensions.DependencyInjection;
using RecipePlanner.App;
using RecipePlanner.Contracts.Recipe;
using RecipePlanner.Contracts.RecipeIngredient;

namespace RecipePlanner.UI {
    public partial class RecipeEditForm : Form {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly RecipeService _recipeService;
        private readonly RecipeIngredientService _recipeIngredientService;

        private int? _recipeId = null;
        private List<RecipeIngredientEditItem>? _recipeIngredients;

        public RecipeEditForm(
            IServiceScopeFactory scopeFactory,
            RecipeService recipeService,
            RecipeIngredientService recipeIngredientService
        ) {
            InitializeComponent();
            _scopeFactory = scopeFactory;
            _recipeService = recipeService;
            _recipeIngredientService = recipeIngredientService;
        }

        private void RecipeEditForm_Load(object sender, EventArgs e) {
            IngredientsListView.AddClicked += IngredientsListView_AddClickedAsync;
            IngredientsListView.UpdateClicked += IngredientsListView_UpdateClickedAsync;
            IngredientsListView.DeleteClicked += IngredientsListView_DeleteClicked;

        }

        public void ShowDialogForCreate(IWin32Window? owner = null) {
            _recipeId = null;
            RecipeName.Clear();
            RecipeInfo.Clear();

            FillPreptimes();
            PrepTimeSelector.SelectedIndex = -1;

            this.Text = "Nieuw recept aanmaken";
            IngredientsListView.SetColumnConfiguration(ExtraGridConfig);

            _recipeIngredients = new List<RecipeIngredientEditItem>();

            base.ShowDialog(owner);
        }

        public async Task ShowDialogForUpdateAsync(int recipeId, IWin32Window? owner = null) {

            var recipe = await _recipeService.GetRecipeByIdAsync(recipeId);

            if (recipe == null)
                throw new InvalidOperationException("Recipe not found in DB");

            _recipeId = recipe.Id;
            RecipeName.Text = recipe.Name;
            RecipeInfo.Text = recipe.Info;

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

                await SaveRecipeToDB(RecipeName.Text, prepTime, RecipeInfo.Text);

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




        private async Task SaveRecipeToDB(string name, PrepTime preptime, string info) {
            //first create recipe
            if (_recipeId == null) {
                _recipeId = await _recipeService.CreateRecipeAsync(name, preptime, info);
            }
            else {
                await _recipeService.UpdateRecipeAsync(_recipeId.Value, name, preptime, info);
            }

            //sync ingredients
            if (_recipeIngredients == null)
                throw new InvalidOperationException("Recipe ingredients list is null.");

            // Deleted eerst (voorkomt unique conflicts bij ingredient switch)
            foreach (var item in _recipeIngredients.Where(x => x.State == EditState.Deleted)) {
                if (item.RecipeIngredientId != null) {
                    await _recipeIngredientService.DeleteRecipeIngredientAsync(item.RecipeIngredientId.Value);
                }
            }

            // Added
            foreach (var item in _recipeIngredients.Where(x => x.State == EditState.Added)) {
                var newId = await _recipeIngredientService.CreateRecipeIngredientAsync(
                    _recipeId.Value,
                    item.IngredientId,
                    item.UnitId,
                    item.Quantity);

                item.RecipeIngredientId = newId;
                item.State = EditState.Unchanged;
            }

            // Modified
            foreach (var item in _recipeIngredients.Where(x => x.State == EditState.Modified)) {
                if (item.RecipeIngredientId == null)
                    throw new InvalidOperationException("Modified item without RecipeIngredientId.");

                await _recipeIngredientService.UpdateRecipeIngredientAsync(
                    item.RecipeIngredientId.Value,
                    item.IngredientId,
                    item.UnitId,
                    item.Quantity);

                item.State = EditState.Unchanged;
            }

            //remove all deleted ingredients from list
            _recipeIngredients.RemoveAll(x => x.State == EditState.Deleted);
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

                if (frm.DialogResult != DialogResult.OK)
                    return;

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

                var uiId = GetSelectedRecipeIngredientUiId(list);
                if (uiId == null)
                    return;

                using var scope = _scopeFactory.CreateScope();
                var frm = scope.ServiceProvider.GetRequiredService<RecipeIngredientEditForm>();
                await frm.ShowDialogForUpdateAsync(_recipeIngredients!, uiId.Value, this);

                if (frm.DialogResult != DialogResult.OK)
                    return;

                BindRecipeIngredients();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IngredientsListView_DeleteClicked(object? sender, EventArgs e) {
            try {
                if (sender is not EntityListViewControl list)
                    throw new InvalidOperationException("Event sender is not EntityListViewControl.");

                var uiId = GetSelectedRecipeIngredientUiId(list);
                if (uiId == null)
                    return;

                var item = _recipeIngredients!.First(x => x.UiId == uiId.Value);

                if (item.State == EditState.Added) {
                    _recipeIngredients!.Remove(item);
                }
                else {
                    item.State = EditState.Deleted;
                }

                BindRecipeIngredients();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Guid? GetSelectedRecipeIngredientUiId(EntityListViewControl list) {
            if (list.SelectedItem is not RecipeIngredientGridRow row) {
                MessageBox.Show("Er is geen ingredient geselecteerd");
                return null;
            }

            return row.UiId;
        }


        private async Task LoadRecipeIngredientsAsync() {
            if (_recipeId == null)
                throw new InvalidOperationException("Recipe ID is null.");

            var rows = await _recipeIngredientService.GetAllRecipeIngredientsAsync(_recipeId.Value);

            _recipeIngredients = rows.Select(r => new RecipeIngredientEditItem {
                RecipeIngredientId = r.RecipeIngredientId,
                IngredientId = r.IngredientId,
                IngredientName = r.IngredientName,
                UnitId = r.UnitId,
                UnitName = r.UnitName,
                Quantity = r.Quantity,
                State = EditState.Unchanged
            }).ToList();



            BindRecipeIngredients();
        }



        private void BindRecipeIngredients() {
            if (_recipeIngredients == null)
                throw new InvalidOperationException("Recipe ingredients list is null.");

            var view = _recipeIngredients
                .Where(x => x.State != EditState.Deleted)
                .Select(x => new RecipeIngredientGridRow(
                    x.UiId,
                    x.IngredientName,
                    x.UnitName,
                    x.Quantity
                ))
                .ToList();

            IngredientsListView.BindData(view);
        }

        private void ExtraGridConfig(DataGridView grid) {

            //hide columns

            var uiIdColumn = grid.Columns["UiId"];
            if (uiIdColumn != null)
                uiIdColumn.Visible = false;

            //other column configs

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
