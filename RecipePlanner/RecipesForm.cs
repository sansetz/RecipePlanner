using RecipePlanner.App;
using RecipePlanner.Data;

namespace RecipePlanner.UI {

    public partial class RecipesForm : Form {
        private readonly IRecipePlannerDbContextFactory _dbFactory;
        private readonly RecipePlannerService _recipePlannerService;

        public RecipesForm(IRecipePlannerDbContextFactory dbFactory, RecipePlannerService recipePlannerService) {
            InitializeComponent();
            _dbFactory = dbFactory;
            _recipePlannerService = recipePlannerService;
        }

        private async void frmRecipes_LoadAsync(object sender, EventArgs e) {
            gridRecipes.AutoGenerateColumns = true;
            gridRecipes.ReadOnly = true;

            var recipes = await _recipePlannerService.GetAllRecipesAsync();
            gridRecipes.DataSource = recipes;
        }
    }
}
