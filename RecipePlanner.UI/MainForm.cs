using Microsoft.Extensions.DependencyInjection;
using RecipePlanner.App;
using RecipePlanner.Contracts.PlannedDay;
using RecipePlanner.Contracts.Recipe;
using RecipePlanner.UI;
using RecipePlanner.UI.Controls;

namespace RecipePlanner {
    public partial class MainForm : Form {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly WeekplanService _weekplanService;
        private readonly RecipeService _recipeService;

        private List<RecipePickerDayControl> _recipePickers = new List<RecipePickerDayControl>();

        private List<RecipeSource> _allRecipes = new List<RecipeSource>();
        private Dictionary<int, int?> _selectedRecipeIdByDay = new Dictionary<int, int?>();

        private Dictionary<int, RecipeSource> _recipeById = new();

        private bool _isRefreshingPickers = false;

        private DateOnly _currentWeekStartDate;

        private const PrepTime DUMMY_PREPTIME = PrepTime.Medium; //todo: replace with field in picker control (does not exist yet)

        public MainForm(
            IServiceScopeFactory scopeFactory,
            WeekplanService weekplanService,
            RecipeService recipeService
        ) {
            InitializeComponent();
            _scopeFactory = scopeFactory;
            _weekplanService = weekplanService;
            _recipeService = recipeService;
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

        private async void ShoppingList_ClickAsync(object sender, EventArgs e) {
            using var scope = _scopeFactory.CreateScope();
            var frm = scope.ServiceProvider.GetRequiredService<GroceryListForm>();
            await frm.ShowDialogForCreateAsync(_currentWeekStartDate, this);
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
                _recipePickers[i].SelectedRecipeChanged += RecipePicker_SelectedRecipeChangedAsync;

                var context = BuildDayContext(i);
                _recipePickers[i].SetContext(context);
            }
        }

        private async void RecipePicker_SelectedRecipeChangedAsync(object? sender, EventArgs e) {
            if (_isRefreshingPickers)
                return;

            var recipePicker = sender as RecipePickerDayControl;
            if (recipePicker == null)
                return;

            int dayIndex = _recipePickers.IndexOf(recipePicker);

            var date = _currentWeekStartDate.AddDays(dayIndex);
            var availablePrepTime = DUMMY_PREPTIME;

            await _weekplanService.SetPlannedDayAsync(
                _currentWeekStartDate,
                date,
                availablePrepTime,
                recipePicker.SelectedRecipeId,
                ct: default
            );
            await ReloadSelectedRecipesForCurrentWeekAsync();

            _selectedRecipeIdByDay[dayIndex] = recipePicker.SelectedRecipeId;

            RefreshPickers();
        }

        private async Task ReloadSelectedRecipesForCurrentWeekAsync(CancellationToken ct = default) {
            // 1) Haal weekplan + planned days op
            var weekplan = await _weekplanService.GetOrCreateWeekplanAsync(_currentWeekStartDate, ct);

            // 2) Reset de UI-state (7 dagen)
            for (int i = 0; i < 7; i++)
                _selectedRecipeIdByDay[i] = null;

            // 3) Zet geplande recepten terug op de juiste dagindex (0..6)
            foreach (var plannedDay in weekplan.PlannedDays) {
                int dayIndex = plannedDay.Date.DayNumber - _currentWeekStartDate.DayNumber;

                if (dayIndex < 0 || dayIndex > 6)
                    continue; // safety: hoort niet te gebeuren als week klopt

                _selectedRecipeIdByDay[dayIndex] = plannedDay.RecipeId;
            }

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
            _allRecipes = await _recipeService.GetRecipeSourcesForPlanningAsync();
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

                foreach (var ci in recipe.CountedIngredients)
                    used.Add(ci.Id);
            }

            return used;
        }

        private DayContext BuildDayContext(int dayIndex) {
            var chosenElsewhere = GetChosenRecipeIdsExceptDay(dayIndex);
            var usedIngredients = GetUsedIngredientIdsExceptDay(dayIndex);

            int? selectedId = _selectedRecipeIdByDay.TryGetValue(dayIndex, out var id)
                ? id
                : null;


            var usedRecipeDayNameById = _selectedRecipeIdByDay
                .Where(kv => kv.Key != dayIndex && kv.Value.HasValue)
                .GroupBy(kv => kv.Value!.Value)
                .ToDictionary(
                    g => g.Key,
                    g => WeekDayHelpers.GetDayName(WeekDayHelpers.ToDayOfWeek(g.First().Key))
                );

            var recipesForDay = _allRecipes
                .Select(r => {
                    var overlapList = r.CountedIngredients
                        .Where(ci => usedIngredients.Contains(ci.Id))
                        .Select(ci => ci.Name)
                        .Distinct()
                        .OrderBy(n => n)
                        .ToList();

                    var overlapText = overlapList.Count == 0 ? null : string.Join(", ", overlapList);

                    usedRecipeDayNameById.TryGetValue(r.Id, out var usedDayName);

                    return new RecipeChoiceItem(
                        r.Id,
                        r.Name,
                        overlapList.Count > 0,
                        overlapList.Count,
                        chosenElsewhere.Contains(r.Id),
                        overlapText,
                        usedDayName,
                        r.InfoText
                    );
                })
                .OrderByDescending(r => r.UsedInOtherDays)
                .ThenByDescending(r => r.OverlapCount)
                .ThenBy(r => r.Name)
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

        private async void StartDatePicker_ValueChangedAsync(object sender, EventArgs e) {
            var chosen = DateOnly.FromDateTime(StartDatePicker.Value);
            _currentWeekStartDate = WeekDayHelpers.GetWeekStart(chosen);

            await ReloadSelectedRecipesForCurrentWeekAsync();
        }
    }
}
