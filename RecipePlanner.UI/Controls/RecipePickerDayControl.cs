using RecipePlanner.App;
using RecipePlanner.Contracts.Filters;
using RecipePlanner.Contracts.Ingredient;
using RecipePlanner.Contracts.PlannedDay;

namespace RecipePlanner.UI.Controls {
    public partial class RecipePickerDayControl : UserControl {

        private int? _selectedRecipeId = null;
        private bool _isBinding = false;
        private bool _isFilterBinding = false;

        private DayContext? _dayContext;

        private bool _pendingDeselect;
        private int _pendingRowIndex = -1;

        private static readonly IngredientComboItem _allIngredientsItem = new(null, "<alle ingrediënten>");

        public int? SelectedRecipeId { get => _selectedRecipeId; }

        public event EventHandler? SelectedRecipeChanged;




        public event FilterChangedEventHandler? FilterChanged;


        public RecipePickerDayControl() {
            InitializeComponent();
            InitialConfig();
        }

        private void RecipesSelector_SelectionChanged(object sender, EventArgs e) {
            if (_isBinding)
                return;

            SetSelectedRecipeId();
            SelectedRecipeChanged?.Invoke(this, EventArgs.Empty);
        }

        private void RecipesSelector_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
            //kleuren hier zetten?
        }

        private void RecipesSelector_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {

            if (RecipesSelector.DataSource is not List<RecipeChoiceItem> items) {
                _isBinding = false;
                return;
            }

            SetGridColors(items);
            SetCorrectSelection(items);

            BeginInvoke(new Action(() => _isBinding = false));
        }
        private void SetCorrectSelection(List<RecipeChoiceItem> items) {
            if (_dayContext == null) return;

            if (_dayContext.SelectedRecipeId.HasValue) {
                var selectedId = _dayContext.SelectedRecipeId.Value;

                // zoek index in de bound lijst
                var index = items.FindIndex(x => x.Id == selectedId);

                if (index >= 0 && index < RecipesSelector.Rows.Count) {
                    var row = RecipesSelector.Rows[index];

                    RecipesSelector.ClearSelection();

                    if (row.Visible && row.Cells.Count > 0 && row.Cells[0].Visible) {
                        RecipesSelector.CurrentCell = row.Cells[0];
                    }

                    row.Selected = true;

                    // update jouw eigen state/label
                    _selectedRecipeId = selectedId;
                    SelectedRecipe.Text = "Gekozen recept: " + items[index].Name;
                }
                else {
                    ClearSelection();
                }
            }
            else {
                ClearSelection();
            }
        }
        private void SetGridColors(List<RecipeChoiceItem> items) {
            for (int i = 0; i < items.Count && i < RecipesSelector.Rows.Count; i++) {
                var item = items[i];
                var row = RecipesSelector.Rows[i];

                if (item.HasOverlap && !item.UsedInOtherDays) {
                    row.DefaultCellStyle.BackColor = Color.LightSteelBlue;
                    row.DefaultCellStyle.ForeColor = Color.Empty;
                }
                else if (item.UsedInOtherDays) {
                    row.DefaultCellStyle.BackColor = Color.DarkGray;
                    row.DefaultCellStyle.ForeColor = Color.DimGray;
                }
                else {
                    row.DefaultCellStyle.BackColor = Color.Empty;
                    row.DefaultCellStyle.ForeColor = Color.Empty;
                }
            }
        }

        public void SetContext(DayContext daycontext) {
            _isBinding = true;
            _dayContext = daycontext;

            DayTitle.Text = WeekDayHelpers.GetDayName(WeekDayHelpers.ToDayOfWeek(daycontext.DayIndex));
            LoadRecipes();
            ConfigFilter();
        }

        private void ConfigFilter() {
            if (_dayContext == null) return;

            _isFilterBinding = true;
            try {
                var previous = IngredientsFilter.SelectedValue;

                var comboItems = new List<IngredientComboItem>(_dayContext.FilterIngredients.Count + 1)
                {
                    _allIngredientsItem
                };
                comboItems.AddRange(_dayContext.FilterIngredients);

                IngredientsFilter.DisplayMember = nameof(IngredientComboItem.Name);
                IngredientsFilter.ValueMember = nameof(IngredientComboItem.Id);
                IngredientsFilter.DataSource = comboItems;

                // probeer vorige keuze te behouden

                if (previous == null)
                    IngredientsFilter.SelectedIndex = 0;
                else
                    IngredientsFilter.SelectedValue = previous;

                if (IngredientsFilter.SelectedIndex < 0)
                    IngredientsFilter.SelectedIndex = 0;
            }
            finally {
                _isFilterBinding = false;
            }
        }



        private void LoadRecipes() {
            if (_dayContext == null) return;

            RecipesSelector.DataSource = _dayContext.Recipes;
        }

        private void InitialConfig() {
            this.Dock = DockStyle.Fill;
            SelectedRecipe.Text = String.Empty;

            InitialConfigGrid();
        }

        private void InitialConfigGrid() {
            //ListItemsGrid.ReadOnly = true;
            RecipesSelector.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            RecipesSelector.MultiSelect = false;
            RecipesSelector.AllowUserToResizeColumns = false;
            RecipesSelector.AllowUserToAddRows = false;
            RecipesSelector.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            RecipesSelector.AutoGenerateColumns = false;
            RecipesSelector.ReadOnly = true;

            RecipesSelector.ShowCellToolTips = true;

            RecipesSelector.EnableHeadersVisualStyles = false;
            RecipesSelector.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                RecipesSelector.ColumnHeadersDefaultCellStyle.BackColor;

            RecipesSelector.ColumnHeadersDefaultCellStyle.SelectionForeColor =
                RecipesSelector.ColumnHeadersDefaultCellStyle.ForeColor;

            RecipesSelector.DefaultCellStyle.SelectionBackColor = Color.Black;
            RecipesSelector.DefaultCellStyle.SelectionForeColor = Color.White;

            RecipesSelector.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "RecipeName",
                DataPropertyName = "Name",
                HeaderText = "Recept",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            var recipeColumn = RecipesSelector.Columns["RecipeName"];
            if (recipeColumn != null) {
                recipeColumn.DefaultCellStyle.BackColor = Color.Empty;
            }
        }

        private void RecipesSelector_CellToolTipTextNeeded(object? sender, DataGridViewCellToolTipTextNeededEventArgs e) {
            if (e.RowIndex < 0) return;

            var row = RecipesSelector.Rows[e.RowIndex];
            if (row.DataBoundItem is not RecipeChoiceItem item) return;

            // Grijze rij: al gekozen
            if (item.UsedInOtherDays && !string.IsNullOrWhiteSpace(item.UsedInDayName)) {
                e.ToolTipText = $"Is al gekozen op {item.UsedInDayName}";
                return;
            }

            // Blauwe rij: overlap
            if (item.HasOverlap && !string.IsNullOrWhiteSpace(item.OverlapIngredientsText)) {
                e.ToolTipText = "Overlap door: " + item.OverlapIngredientsText;
            }

            if (RecipesSelector.SelectedRows.Count > 0 && row == RecipesSelector.SelectedRows[0])
                e.ToolTipText = "Info: " + item.InfoText;
        }

        private void ClearSelection() {
            RecipesSelector.ClearSelection();
            RecipesSelector.CurrentCell = null;
            _selectedRecipeId = null;
            SelectedRecipe.Text = string.Empty;
        }
        public void SetSelectedRecipeId() {
            if (RecipesSelector.SelectedRows.Count == 0) {
                _selectedRecipeId = null;
                SelectedRecipe.Text = string.Empty;
                return;
            }

            var row = RecipesSelector.SelectedRows[0];
            if (row.DataBoundItem is not RecipeChoiceItem item) {
                _selectedRecipeId = null;
                SelectedRecipe.Text = string.Empty;
                return;
            }

            _selectedRecipeId = item.Id;
            SelectedRecipe.Text = "Gekozen recept: " + item.Name;
        }
        private void RecipesSelector_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
            if (e.RowIndex < 0) return; // header
            if (_isBinding) return;

            // Als de user klikt op een rij die nu al geselecteerd is:
            if (RecipesSelector.Rows[e.RowIndex].Selected) {
                _pendingDeselect = true;
                _pendingRowIndex = e.RowIndex;
            }
            else {
                _pendingDeselect = false;
                _pendingRowIndex = -1;
            }
        }

        private void RecipesSelector_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e) {
            if (!_pendingDeselect) return;
            if (e.RowIndex != _pendingRowIndex) return; // muis verplaatst -> negeer
            if (_isBinding) return;

            _pendingDeselect = false;
            _pendingRowIndex = -1;

            RecipesSelector.ClearSelection();
            RecipesSelector.CurrentCell = null;

            // reset jouw eigen state + label
            _selectedRecipeId = null;
            SelectedRecipe.Text = string.Empty;

            SelectedRecipeChanged?.Invoke(this, EventArgs.Empty);
        }

        private void IngredientsFilter_SelectionChangeCommitted(object sender, EventArgs e) {
            FilterChangedInternal();
        }

        private void NoFreshIngredientsFilter_CheckedChanged(object sender, EventArgs e) {
            FilterChangedInternal();
        }

        private void FilterChangedInternal() {
            if (_isBinding || _isFilterBinding)
                return;

            var selectedId = IngredientsFilter.SelectedValue as int?;
            var noFreshIngredients = NoFreshIngredientsFilter.Checked;
            FilterChanged?.Invoke(this, new FilterChangedEventArgs(selectedId, noFreshIngredients));
        }

    }
}