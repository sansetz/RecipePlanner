using Microsoft.Extensions.DependencyInjection;
using RecipePlanner.App;

namespace RecipePlanner.UI {
    public partial class ShoppingListForm : Form {

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly RecipePlannerService _recipePlannerService;

        public ShoppingListForm(
            IServiceScopeFactory scopeFactory,
            RecipePlannerService recipePlannerService
        ) {
            InitializeComponent();
            _scopeFactory = scopeFactory;
            _recipePlannerService = recipePlannerService;
        }
    }
}
