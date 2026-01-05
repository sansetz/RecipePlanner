using Microsoft.Extensions.DependencyInjection;
using RecipePlanner.App;
using RecipePlanner.Data;
using RecipePlanner.UI;

namespace RecipePlanner {
    public partial class MainForm : Form {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IRecipePlannerDbContextFactory _dbFactory;
        private readonly RecipePlannerService _recipePlannerService;

        public MainForm(IServiceScopeFactory scopeFactory, IRecipePlannerDbContextFactory dbFactory, RecipePlannerService recipePlannerService) {
            InitializeComponent();
            _scopeFactory = scopeFactory;
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

        private void IngredientsButton_Click(object sender, EventArgs e) {
            using var scope = _scopeFactory.CreateScope();
            var frm = scope.ServiceProvider.GetRequiredService<IngredientsForm>();
            frm.ShowDialog(this);
        }

        private void RecipesButton_Click(object sender, EventArgs e) {
            using var scope = _scopeFactory.CreateScope();
            var frm = scope.ServiceProvider.GetRequiredService<RecipesForm>();
            frm.ShowDialog(this);
        }
    }
}
