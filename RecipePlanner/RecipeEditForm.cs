using RecipePlanner.App;
using RecipePlanner.Contracts.Recipe;

namespace RecipePlanner.UI {
    public partial class RecipeEditForm : Form {
        private readonly RecipePlannerService _recipePlannerService;
        private int? _recipeId = null;

        public RecipeEditForm(
            RecipePlannerService recipePlannerService
        ) {
            InitializeComponent();
            _recipePlannerService = recipePlannerService;
        }

        public void ShowDialogForCreate(IWin32Window? owner = null) {
            _recipeId = null;
            RecipeName.Clear();

            FillPreptimes();
            PrepTimeSelector.SelectedIndex = -1;

            this.Text = "Nieuw recept aanmaken";

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
                await _recipePlannerService.CreateRecipeAsync(
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
        }
        private void Cancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
