using RecipePlanner.App;
using RecipePlanner.Data;

namespace RecipePlanner.UI {
    public partial class frmIngredients : Form {
        private readonly IRecipePlannerDbContextFactory _dbFactory;
        private readonly RecipePlannerService _recipePlannerService;

        public frmIngredients(IRecipePlannerDbContextFactory dbFactory, RecipePlannerService recipePlannerService) {
            InitializeComponent();
            _dbFactory = dbFactory;
            _recipePlannerService = recipePlannerService;
        }

        private async void frmIngredients_LoadAsync(object sender, EventArgs e) {
            gridIngredients.AutoGenerateColumns = true;
            gridIngredients.ReadOnly = true;

            var ingredients = await _recipePlannerService.GetAllIngredientsAsync();
            gridIngredients.DataSource = ingredients;
        }
    }
}
