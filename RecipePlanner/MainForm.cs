using Microsoft.Extensions.DependencyInjection;
using RecipePlanner.App;
using RecipePlanner.Contracts.PlannedDay;
using RecipePlanner.Data;
using RecipePlanner.UI;
using RecipePlanner.UI.Controls;

namespace RecipePlanner {
    public partial class MainForm : Form {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IRecipePlannerDbContextFactory _dbFactory;
        private readonly RecipePlannerService _recipePlannerService;

        private List<RecipePickerDayControl> _recipePickers = new List<RecipePickerDayControl>();

        private List<RecipeSource> _allRecipes = new List<RecipeSource>();
        private Dictionary<int, int?> _selectedRecipeIdByDay = new Dictionary<int, int?>();

        private Dictionary<int, RecipeSource> _recipeById = new();

        private bool _isRefreshingPickers = false;


        public MainForm(IServiceScopeFactory scopeFactory, IRecipePlannerDbContextFactory dbFactory, RecipePlannerService recipePlannerService) {
            InitializeComponent();
            _scopeFactory = scopeFactory;
            _dbFactory = dbFactory;
            _recipePlannerService = recipePlannerService;
        }

        private async void MainForm_LoadAsync(object sender, EventArgs e) {
            //this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            await LoadRecipesAsync();
            FillRecipePickersList();
            InitRecipePickers();
        }

        private void IngredientsButton_Click(object sender, EventArgs e) {
            using var scope = _scopeFactory.CreateScope();
            var frm = scope.ServiceProvider.GetRequiredService<IngredientsForm>();
            frm.ShowDialog(this);
        }

        private async void RecipesButton_ClickAsync(object sender, EventArgs e) {
            using var scope = _scopeFactory.CreateScope();
            var frm = scope.ServiceProvider.GetRequiredService<RecipesForm>();
            frm.ShowDialog(this);
            await LoadRecipesAsync();
            RefreshPickers();
        }

        private void Exit_Click(object sender, EventArgs e) {
            Close();
        }

        private void FillRecipePickersList() {
            _recipePickers.Add(MondayRecipePicker);
            _recipePickers.Add(TuesdayRecipePicker);
            _recipePickers.Add(WednesdayRecipePicker);
            _recipePickers.Add(ThursdayRecipePicker);
            _recipePickers.Add(FridayRecipePicker);
            _recipePickers.Add(SaturdayRecipePicker);
            _recipePickers.Add(SundayRecipePicker);
        }

        private void InitRecipePickers() {
            for (int i = 0; i < 7; i++) {
                _recipePickers[i].SelectedRecipeChanged += RecipePicker_SelectedRecipeChanged;

                var context = BuildDayContext(i);
                _recipePickers[i].SetContext(context);
            }
        }

        private void RecipePicker_SelectedRecipeChanged(object? sender, EventArgs e) {
            if (_isRefreshingPickers)
                return;

            var recipePicker = sender as RecipePickerDayControl;
            if (recipePicker == null)
                return;

            int dayIndex = _recipePickers.IndexOf(recipePicker);

            _selectedRecipeIdByDay[dayIndex] = recipePicker.SelectedRecipeId;
            RefreshPickers();


        }

        private void RefreshPickers() {
            try {
                _isRefreshingPickers = true;

                for (int i = 0; i < 7; i++)
                    _recipePickers[i].SetContext(BuildDayContext(i));
            }
            finally {
                _isRefreshingPickers = false;
            }
        }

        private async Task LoadRecipesAsync() {
            _allRecipes = await _recipePlannerService.GetRecipeSourcesForPlanningAsync();
            _recipeById = _allRecipes.ToDictionary(r => r.Id);
        }

        public static DayOfWeek ToDayOfWeek(int dayIndex) {
            if (dayIndex < 0 || dayIndex > 6)
                throw new ArgumentOutOfRangeException(nameof(dayIndex));

            // DayOfWeek: Sunday = 0, Monday = 1, ...
            // Planner:   Monday = 0, Tuesday = 1, ...
            return (DayOfWeek)(((int)DayOfWeek.Monday + dayIndex) % 7);
        }

        private HashSet<int> GetUsedIngredientIdsExceptDay(int dayIndex) {
            var used = new HashSet<int>();

            foreach (var kv in _selectedRecipeIdByDay) {
                if (kv.Key == dayIndex) continue;
                if (!kv.Value.HasValue) continue;

                if (!_recipeById.TryGetValue(kv.Value.Value, out var recipe))
                    continue;

                foreach (var ingredientId in recipe.CountedIngredientIds)
                    used.Add(ingredientId);
            }

            return used;
        }

        private DayContext BuildDayContext(int dayIndex) {
            var chosenElsewhere = GetChosenRecipeIdsExceptDay(dayIndex);
            var usedIngredients = GetUsedIngredientIdsExceptDay(dayIndex);

            int? selectedId = _selectedRecipeIdByDay.TryGetValue(dayIndex, out var id)
                ? id
                : null;

            var recipesForDay = _allRecipes
                .Select(r => {
                    var overlapCount = r.CountedIngredientIds
                        .Count(id => usedIngredients.Contains(id));

                    return new RecipeChoiceItem(
                        r.Id,
                        r.Name,
                        overlapCount > 0,
                        overlapCount,
                        chosenElsewhere.Contains(r.Id)
                    );
                })
                .OrderByDescending(r => r.UsedInOtherDays)
                .ThenByDescending(r => r.OverlapCount)
                .ToList();

            return new DayContext {
                DayIndex = dayIndex,
                Recipes = recipesForDay,
                SelectedRecipeId = selectedId
            };
        }
        //var movies = _db.Movies.OrderBy(c => c.Category).ThenBy(n => n.Name)
        private HashSet<int> GetChosenRecipeIdsExceptDay(int dayIndex) {
            return _selectedRecipeIdByDay
                .Where(kv => kv.Key != dayIndex && kv.Value.HasValue)
                .Select(kv => kv.Value!.Value)
                .ToHashSet();
        }
    }
}
