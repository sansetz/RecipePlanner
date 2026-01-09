using RecipePlanner.Contracts.PlannedDay;
using System.Globalization;

namespace RecipePlanner.UI.Controls {
    public partial class RecipePickerDayControl : UserControl {

        private int? _selectedRecipeId = null;
        private bool _isBinding = false;
        private DayContext? _dayContext;

        private bool _pendingDeselect;
        private int _pendingRowIndex = -1;

        public int? SelectedRecipeId { get => _selectedRecipeId; }

        public event EventHandler? SelectedRecipeChanged;

        public RecipePickerDayControl() {
            InitializeComponent();
            InitialConfig();
        }
        public void SetContext(DayContext daycontext) {
            _isBinding = true;
            _dayContext = daycontext;

            DayTitle.Text = GetDayName(ToDayOfWeek(daycontext.DayIndex));
            LoadRecipes();
        }

        private string GetDayName(DayOfWeek dayOfWeek) {
            var cultureInfo = new CultureInfo("nl-NL");
            var dateTimeInfo = cultureInfo.DateTimeFormat;

            return dateTimeInfo.GetDayName(dayOfWeek);
        }

        private void LoadRecipes() {
            if (_dayContext == null) return;

            RecipesSelector.DataSource = _dayContext.Recipes;
        }

        private void InitialConfig() {
            this.Dock = DockStyle.Fill;
            InitialConfigGrid();
            SelectedRecipe.Text = String.Empty;
        }

        private void InitialConfigGrid() {
            //ListItemsGrid.ReadOnly = true;
            RecipesSelector.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            RecipesSelector.MultiSelect = false;
            RecipesSelector.AllowUserToResizeColumns = false;
            RecipesSelector.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            RecipesSelector.AutoGenerateColumns = false;
            RecipesSelector.ReadOnly = true;

            RecipesSelector.EnableHeadersVisualStyles = false;
            RecipesSelector.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                RecipesSelector.ColumnHeadersDefaultCellStyle.BackColor;

            RecipesSelector.ColumnHeadersDefaultCellStyle.SelectionForeColor =
                RecipesSelector.ColumnHeadersDefaultCellStyle.ForeColor;

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

        private void RecipesSelector_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
            if (e.RowIndex < 0) return; // header

            var row = RecipesSelector.Rows[e.RowIndex];
            if (row.DataBoundItem is not RecipeChoiceItem item) return;

            if (item.HasOverlap) {
                // Normale kleur
                e.CellStyle.BackColor = Color.LightSteelBlue;

                // Zodat de kleur niet "verdwijnt" bij selectie
                e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
            }
        }

        public void SetSelectedRecipeId() {
            var row = RecipesSelector.CurrentRow;

            if ((row == null) || (row.DataBoundItem is not RecipeChoiceItem item)) {
                _selectedRecipeId = null;
                return;
            }

            _selectedRecipeId = item.Id;
            SelectedRecipe.Text = "Gekozen recept: " + item.Name;
        }

        private void RecipesSelector_SelectionChanged(object sender, EventArgs e) {
            if (_isBinding)
                return;

            SetSelectedRecipeId();
            SelectedRecipeChanged?.Invoke(this, EventArgs.Empty);
        }
        private static DayOfWeek ToDayOfWeek(int dayIndex) {
            if (dayIndex < 0 || dayIndex > 6)
                throw new ArgumentOutOfRangeException(nameof(dayIndex));

            // DayOfWeek: Sunday = 0, Monday = 1, ...
            // Planner:   Monday = 0, Tuesday = 1, ...
            return (DayOfWeek)(((int)DayOfWeek.Monday + dayIndex) % 7);
        }

        private void RecipesSelector_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {

            if (_dayContext == null)
                return;

            if (_dayContext.SelectedRecipeId.HasValue) {
                foreach (DataGridViewRow row in RecipesSelector.Rows) {
                    if (row.DataBoundItem is RecipeChoiceItem item) {
                        if (item.Id == _dayContext.SelectedRecipeId.Value) {
                            row.Selected = true;
                            RecipesSelector.CurrentCell = row.Cells[0];
                            break;
                        }
                    }
                }
                SetSelectedRecipeId();
            }
            else
                ClearSelection();


            _isBinding = false; ;
        }

        private void ClearSelection() {
            RecipesSelector.ClearSelection();
            _selectedRecipeId = null;
            SelectedRecipe.Text = string.Empty;
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
    }

}