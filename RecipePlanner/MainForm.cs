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

        private List<RecipeChoiceItem> _allRecipes = new List<RecipeChoiceItem>();
        private Dictionary<int, int?> _selectedRecipeIdByDay = new Dictionary<int, int?>();

        public MainForm(IServiceScopeFactory scopeFactory, IRecipePlannerDbContextFactory dbFactory, RecipePlannerService recipePlannerService) {
            InitializeComponent();
            _scopeFactory = scopeFactory;
            _dbFactory = dbFactory;
            _recipePlannerService = recipePlannerService;
        }

        private void MainForm_Load(object sender, EventArgs e) {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            LoadRecipes();
            FillRecipePickersList();
            InitRecipePickers();
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

                var dayContext = new DayContext {
                    Recipes = _allRecipes.ToList(),
                    DayIndex = i,
                    SelectedRecipeId = GetSelectedRecipeForDay(i)

                };
                _recipePickers[i].SetContext(dayContext);
            }
        }
        private int? GetSelectedRecipeForDay(int dayIndex) {
            if (_selectedRecipeIdByDay.TryGetValue(dayIndex, out var recipeId)) {
                // Use recipeId as needed
            }
            return recipeId;
        }

        private void LoadRecipes() {
            _allRecipes = new List<RecipeChoiceItem> {
                new RecipeChoiceItem (5, "Boerenkool", true, 2, false),
                new RecipeChoiceItem (4, "Zuurkool", true, 1, false),
                new RecipeChoiceItem (1, "Spaghetti Bolognese", false, 0, false),
                new RecipeChoiceItem (2, "Macaroni", false, 0, false),
                new RecipeChoiceItem (3, "Nasi", false, 0, false)
            };

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


        private void RecipePicker_SelectedRecipeChanged(object? sender, EventArgs e) {

            var recipePicker = sender as RecipePickerDayControl;
            if (recipePicker == null)
                return;

            int dayIndex = _recipePickers.IndexOf(recipePicker);


            _selectedRecipeIdByDay[dayIndex] = recipePicker.SelectedRecipeId;

        }

        public static DayOfWeek ToDayOfWeek(int dayIndex) {
            if (dayIndex < 0 || dayIndex > 6)
                throw new ArgumentOutOfRangeException(nameof(dayIndex));

            // DayOfWeek: Sunday = 0, Monday = 1, ...
            // Planner:   Monday = 0, Tuesday = 1, ...
            return (DayOfWeek)(((int)DayOfWeek.Monday + dayIndex) % 7);
        }


        private void RefreshDay(int dayIndex) {
            var picker = _recipePickers[dayIndex];

            int? selectedRecipeId = null;
            if (_selectedRecipeIdByDay.TryGetValue(dayIndex, out var id))
                selectedRecipeId = id;

            var context = new DayContext {
                DayIndex = dayIndex,
                Recipes = _allRecipes.ToList(),
                SelectedRecipeId = selectedRecipeId
            };

            picker.SetContext(context);
        }

        private void button1_Click(object sender, EventArgs e) {
            _selectedRecipeIdByDay[0] = 4;

            RefreshDay(0);
        }

        private void Exit_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
