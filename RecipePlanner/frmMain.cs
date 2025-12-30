using Microsoft.Extensions.DependencyInjection;
using RecipePlanner.App;
using RecipePlanner.Data;
using RecipePlanner.UI;

namespace RecipePlanner {
    public partial class frmMain : Form {
        private readonly IRecipePlannerDbContextFactory _dbFactory;
        private readonly RecipePlannerService _recipePlannerService;

        public frmMain(IRecipePlannerDbContextFactory dbFactory, RecipePlannerService recipePlannerService) {
            InitializeComponent();
            _dbFactory = dbFactory;
            _recipePlannerService = recipePlannerService;

        }

        private async void btnSeedTest_ClickAsync(object sender, EventArgs e) {
            await _recipePlannerService.SaveSeedDataAsync();
        }

        private async void btnTest_ClickAsync(object sender, EventArgs e) {

            var recipes = await _recipePlannerService.GetAllRecipesAsync();

            MessageBox.Show($"Recipes: {recipes.Count}");
            //MessageBox.Show($"First recipe ingredients: {recipes[0].RecipeIngredients.Count}");

            var selectedRecipeId = recipes.First().Id;
            var overlapRecipes = await _recipePlannerService.GetOverlapRecipes(selectedRecipeId);
            MessageBox.Show($"Overlap recipes with recipe {selectedRecipeId}: {overlapRecipes.Count}");

        }

        private void btnIngredients_Click(object sender, EventArgs e) {
            using var scope = Program.ServiceProvider.CreateScope();
            var frm = scope.ServiceProvider.GetRequiredService<frmIngredients>();
            frm.ShowDialog(this);
        }
    }
}
