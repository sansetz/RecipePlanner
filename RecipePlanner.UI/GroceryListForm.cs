using Microsoft.Extensions.DependencyInjection;
using RecipePlanner.App;
using RecipePlanner.Contracts.GroceryList;

namespace RecipePlanner.UI {
    public partial class GroceryListForm : Form {

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly GroceryListService _groceryListService;
        private DateOnly _currentWeekStartDate;

        public GroceryListForm(
            IServiceScopeFactory scopeFactory,
            GroceryListService groceryListService
        ) {
            InitializeComponent();
            _scopeFactory = scopeFactory;
            _groceryListService = groceryListService;
        }

        public async Task ShowDialogAsync(DateOnly currentWeekStartDate, IWin32Window? owner = null) {
            _currentWeekStartDate = currentWeekStartDate;

            await LoadGroceryListAsync();

            base.ShowDialog(owner);
        }

        private async Task LoadGroceryListAsync() {

            var items = await _groceryListService
                .GetGroceryListItemsForWeekAsync(_currentWeekStartDate);

            GroceryList.Clear();

            bool printedSeparator = false;

            foreach (var item in items) {
                if (!printedSeparator && !item.CountForOverlap) {
                    GroceryList.AppendText(Environment.NewLine + "----" + Environment.NewLine);
                    printedSeparator = true;
                }

                GroceryList.AppendText(
                    FormatGroceryItem(item) + Environment.NewLine
                );
            }
        }

        private static string FormatGroceryItem(GroceryListItem item) {
            return $"{item.TotalQuantity} {item.UnitName} {item.IngredientName}";
        }
    }
}
