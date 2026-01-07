namespace RecipePlanner.UI {
    public partial class EntityListViewControl : UserControl {

        public event EventHandler? AddClicked;
        public event EventHandler? UpdateClicked;
        public event EventHandler? DeleteClicked;

        private Action<DataGridView>? _configureColumns;

        public void SetColumnConfiguration(Action<DataGridView> config) {
            _configureColumns = config;
        }

        public EntityListViewControl() {
            InitializeComponent();
            InitialConfig();
        }

        public object? SelectedItem {
            get => ListItemsGrid.CurrentRow?.DataBoundItem;
        }

        private void InitialConfig() {
            this.Dock = DockStyle.Fill;
            InitialConfigButtons();
            InitialConfigGrid();
        }

        private void InitialConfigButtons() {
            AddItem.Enabled = true;
            UpdateItem.Enabled = false;
            DeleteItem.Enabled = false;
        }

        private void InitialConfigGrid() {
            //ListItemsGrid.ReadOnly = true;
            ListItemsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ListItemsGrid.MultiSelect = false;
            ListItemsGrid.AllowUserToResizeColumns = false;
            ListItemsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            ListItemsGrid.AutoGenerateColumns = true;
            ListItemsGrid.ReadOnly = true;
            ListItemsGrid.Enabled = false;
        }

        private void DataSpecificConfigGrid() {

            ListItemsGrid.Enabled = true;

            var idColumn = ListItemsGrid.Columns["Id"];
            if (idColumn != null)
                idColumn.Visible = false;

            if (ListItemsGrid.Rows.Count > 0) {
                var firstRow = ListItemsGrid.Rows[0];

                // find first visible column
                DataGridViewColumn? firstVisibleCol = null;
                foreach (DataGridViewColumn col in ListItemsGrid.Columns) {
                    if (col.Visible) {
                        firstVisibleCol = col;
                        break;
                    }
                }

                if (firstVisibleCol != null) {
                    ListItemsGrid.CurrentCell = firstRow.Cells[firstVisibleCol.Index];
                    firstRow.Selected = true;
                }
            }
        }

        private void UpdateButtonsState() {
            UpdateItem.Enabled = SelectedItem != null;
            DeleteItem.Enabled = SelectedItem != null;
        }

        private void AddItem_Click(object sender, EventArgs e) {
            AddClicked?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateItem_Click(object sender, EventArgs e) {
            UpdateClicked?.Invoke(this, EventArgs.Empty);
        }

        private void DeleteItem_Click(object sender, EventArgs e) {
            DeleteClicked?.Invoke(this, EventArgs.Empty);
        }

        private void ListItemsGrid_SelectionChanged(object sender, EventArgs e) {
            UpdateButtonsState();
        }

        public void BindData(System.Collections.IEnumerable dataSource) {
            ListItemsGrid.DataSource = dataSource;
            DataSpecificConfigGrid();
            _configureColumns?.Invoke(ListItemsGrid);

        }

    }
}
