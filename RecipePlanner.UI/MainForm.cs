using Microsoft.Extensions.DependencyInjection;
using RecipePlanner.App;
using RecipePlanner.Contracts.Filters;
using RecipePlanner.Contracts.Ingredient;
using RecipePlanner.Contracts.PlannedDay;
using RecipePlanner.Contracts.Recipe;
using RecipePlanner.UI;
using RecipePlanner.UI.Controls;

namespace RecipePlanner {
    public partial class MainForm : Form {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly WeekplanService _weekplanService;
        private readonly RecipeService _recipeService;
        private readonly IngredientService _ingredientService;

        private List<RecipePickerDayControl> _recipePickers = new List<RecipePickerDayControl>();

        private List<RecipeSource> _allRecipes = new List<RecipeSource>();
        private List<IngredientComboItem> _filterIngredients = new List<IngredientComboItem>();
        private Dictionary<int, int?> _selectedRecipeIdByDay = new Dictionary<int, int?>();
        private Dictionary<int, int?> _filteredIngredientIdByDay = new Dictionary<int, int?>();

        private Dictionary<int, RecipeSource> _recipeById = new();

        private bool _isRefreshingPickers = false;
        private bool _normalizingStartDate = false;

        private DateOnly _currentWeekStartDate;

        private const PrepTime DUMMY_PREPTIME = PrepTime.Medium; //todo: replace with field in picker control (does not exist yet)

        public MainForm(
            IServiceScopeFactory scopeFactory,
            WeekplanService weekplanService,
            RecipeService recipeService,
            IngredientService ingredientService
        ) {
            InitializeComponent();
            _scopeFactory = scopeFactory;
            _weekplanService = weekplanService;
            _recipeService = recipeService;
            _ingredientService = ingredientService;
        }

        private async void MainForm_LoadAsync(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Maximized;

            LayoutConfig(this.Width, this.Height);
            await LoadRecipesAsync();
            await LoadFilterIngredientsAsync();
            FillRecipePickersList();
            InitRecipePickers();
            SetWeekToMonday();
        }

        private void LayoutConfig(int screenwidth, int screenheight) {
            var daywidth = (screenwidth - 20) / 4;
            var dayheight = (screenheight - 58) / 2;

            TopDaysPanel.Height = dayheight;
            BottomDaysPanel.Height = dayheight;

            Monday.Width = daywidth;
            Tuesday.Width = daywidth;
            Wednesday.Width = daywidth;
            Thursday.Width = daywidth;
            Friday.Width = daywidth;
            Saturday.Width = daywidth;
            Sunday.Width = daywidth;
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
            await frm.ShowDialogAsync(_currentWeekStartDate, this);
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
                _recipePickers[i].FilterChanged += RecipePicker_FilterChanged;

                var context = BuildDayContext(i);
                _recipePickers[i].SetContext(context);
            }
        }
        private void RecipePicker_FilterChanged(object? sender, FilterChangedEventArgs e) {
            if (_isRefreshingPickers)
                return;

            var recipePicker = sender as RecipePickerDayControl;
            if (recipePicker == null)
                return;

            int dayIndex = _recipePickers.IndexOf(recipePicker);

            _filteredIngredientIdByDay[dayIndex] = e.IngredientId;


            var dayContext = BuildDayContext(dayIndex);
            _recipePickers[dayIndex].SetContext(dayContext);


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

        private async Task LoadFilterIngredientsAsync() {
            _filterIngredients = await _ingredientService.GetAllIngredientsForComboAsync();
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

            int? selectedRecipeId = _selectedRecipeIdByDay.TryGetValue(dayIndex, out var recipeId)
                ? recipeId
                : null;

            int? filterIngredientId = _filteredIngredientIdByDay.TryGetValue(dayIndex, out var ingredientId)
                ? ingredientId
                : null;

            List<RecipeSource> filteredRecipes;
            if (filterIngredientId != null) {

                filteredRecipes = _allRecipes
                    .Where(r => r.AllIngredients.Any(i => i.Id == filterIngredientId.Value))
                    .ToList();
            }
            else {
                filteredRecipes = _allRecipes.ToList();
            }


            var usedRecipeDayNameById = _selectedRecipeIdByDay
                .Where(kv => kv.Key != dayIndex && kv.Value.HasValue)
                .GroupBy(kv => kv.Value!.Value)
                .ToDictionary(
                    g => g.Key,
                    g => WeekDayHelpers.GetDayName(WeekDayHelpers.ToDayOfWeek(g.First().Key))
                );

            var recipesForDay = filteredRecipes
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
                SelectedRecipeId = selectedRecipeId,
                FilterIngredients = _filterIngredients
            };
        }
        //var movies = _db.Movies.OrderBy(c => c.Category).ThenBy(n => n.Name)
        private HashSet<int> GetChosenRecipeIdsExceptDay(int dayIndex) {
            return _selectedRecipeIdByDay
                .Where(kv => kv.Key != dayIndex && kv.Value.HasValue)
                .Select(kv => kv.Value!.Value)
                .ToHashSet();
        }

        private void SetWeekToMonday() {
            if (_normalizingStartDate) return;

            var chosen = DateOnly.FromDateTime(StartDatePicker.Value);
            var monday = WeekDayHelpers.GetWeekStart(chosen);

            _currentWeekStartDate = monday;

            // Normaliseer de UI (datepicker) naar maandag
            var mondayDateTime = monday.ToDateTime(TimeOnly.MinValue);
            if (StartDatePicker.Value.Date != mondayDateTime.Date) {
                _normalizingStartDate = true;
                StartDatePicker.Value = mondayDateTime;
                _normalizingStartDate = false;
            }
        }

        private async void StartDatePicker_ValueChangedAsync(object sender, EventArgs e) {
            SetWeekToMonday();

            await ReloadSelectedRecipesForCurrentWeekAsync();
        }

        private async void WeekSchedule_ClickAsync(object sender, EventArgs e) {
            using var scope = _scopeFactory.CreateScope();
            var frm = scope.ServiceProvider.GetRequiredService<WeekScheduleForm>();

            var weekplanId = _weekplanService.GetWeekplanIdForDate(_currentWeekStartDate);
            if (weekplanId == null || weekplanId.Result == null) {
                MessageBox.Show(
                    this,
                    "Er is nog geen weekplanning voor deze week. Voeg eerst een recept toe aan een dag om de weekplanning te kunnen bekijken.",
                    "Geen weekplanning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            await frm.ShowDialogAsync((int)weekplanId.Result, this);
        }
    }
}
