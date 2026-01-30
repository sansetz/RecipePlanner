namespace RecipePlanner.UI.Controls {
    partial class RecipePickerDayControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            ControlPanel = new Panel();
            RecipesSelector = new DataGridView();
            InfoPanel = new Panel();
            SelectedRecipe = new Label();
            ToolbarPanel = new Panel();
            label1 = new Label();
            IngredientsFilter = new ComboBox();
            TitlePanel = new Panel();
            DayTitle = new Label();
            ControlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RecipesSelector).BeginInit();
            InfoPanel.SuspendLayout();
            ToolbarPanel.SuspendLayout();
            TitlePanel.SuspendLayout();
            SuspendLayout();
            // 
            // ControlPanel
            // 
            ControlPanel.Controls.Add(RecipesSelector);
            ControlPanel.Controls.Add(InfoPanel);
            ControlPanel.Controls.Add(ToolbarPanel);
            ControlPanel.Controls.Add(TitlePanel);
            ControlPanel.Dock = DockStyle.Fill;
            ControlPanel.Location = new Point(0, 0);
            ControlPanel.Name = "ControlPanel";
            ControlPanel.Padding = new Padding(10);
            ControlPanel.Size = new Size(603, 735);
            ControlPanel.TabIndex = 0;
            // 
            // RecipesSelector
            // 
            RecipesSelector.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            RecipesSelector.ColumnHeadersVisible = false;
            RecipesSelector.Dock = DockStyle.Fill;
            RecipesSelector.EnableHeadersVisualStyles = false;
            RecipesSelector.Location = new Point(10, 152);
            RecipesSelector.Name = "RecipesSelector";
            RecipesSelector.RowHeadersVisible = false;
            RecipesSelector.RowHeadersWidth = 82;
            RecipesSelector.Size = new Size(583, 530);
            RecipesSelector.TabIndex = 5;
            RecipesSelector.CellFormatting += RecipesSelector_CellFormatting;
            RecipesSelector.CellMouseDown += RecipesSelector_CellMouseDown;
            RecipesSelector.CellMouseUp += RecipesSelector_CellMouseUp;
            RecipesSelector.CellToolTipTextNeeded += RecipesSelector_CellToolTipTextNeeded;
            RecipesSelector.DataBindingComplete += RecipesSelector_DataBindingComplete;
            RecipesSelector.SelectionChanged += RecipesSelector_SelectionChanged;
            // 
            // InfoPanel
            // 
            InfoPanel.BackColor = Color.SteelBlue;
            InfoPanel.BorderStyle = BorderStyle.FixedSingle;
            InfoPanel.Controls.Add(SelectedRecipe);
            InfoPanel.Dock = DockStyle.Bottom;
            InfoPanel.ForeColor = Color.White;
            InfoPanel.Location = new Point(10, 682);
            InfoPanel.Name = "InfoPanel";
            InfoPanel.Size = new Size(583, 43);
            InfoPanel.TabIndex = 4;
            // 
            // SelectedRecipe
            // 
            SelectedRecipe.AutoSize = true;
            SelectedRecipe.Location = new Point(3, 0);
            SelectedRecipe.Name = "SelectedRecipe";
            SelectedRecipe.Size = new Size(78, 32);
            SelectedRecipe.TabIndex = 0;
            SelectedRecipe.Text = "label2";
            // 
            // ToolbarPanel
            // 
            ToolbarPanel.BackColor = Color.Black;
            ToolbarPanel.Controls.Add(label1);
            ToolbarPanel.Controls.Add(IngredientsFilter);
            ToolbarPanel.Dock = DockStyle.Top;
            ToolbarPanel.Location = new Point(10, 92);
            ToolbarPanel.Name = "ToolbarPanel";
            ToolbarPanel.Padding = new Padding(10);
            ToolbarPanel.Size = new Size(583, 60);
            ToolbarPanel.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Left;
            label1.ForeColor = Color.White;
            label1.Location = new Point(10, 10);
            label1.Name = "label1";
            label1.Size = new Size(184, 32);
            label1.TabIndex = 1;
            label1.Text = "Filter Ingredient";
            // 
            // IngredientsFilter
            // 
            IngredientsFilter.Dock = DockStyle.Right;
            IngredientsFilter.FormattingEnabled = true;
            IngredientsFilter.Location = new Point(207, 10);
            IngredientsFilter.Margin = new Padding(10, 50, 10, 10);
            IngredientsFilter.Name = "IngredientsFilter";
            IngredientsFilter.Size = new Size(366, 40);
            IngredientsFilter.TabIndex = 0;
            IngredientsFilter.SelectionChangeCommitted += IngredientsFilter_SelectionChangeCommitted;
            // 
            // TitlePanel
            // 
            TitlePanel.BackColor = Color.Black;
            TitlePanel.Controls.Add(DayTitle);
            TitlePanel.Dock = DockStyle.Top;
            TitlePanel.Location = new Point(10, 10);
            TitlePanel.Name = "TitlePanel";
            TitlePanel.Size = new Size(583, 82);
            TitlePanel.TabIndex = 0;
            // 
            // DayTitle
            // 
            DayTitle.BackColor = Color.Transparent;
            DayTitle.Dock = DockStyle.Fill;
            DayTitle.Font = new Font("Bernard MT Condensed", 25.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DayTitle.ForeColor = Color.SteelBlue;
            DayTitle.Location = new Point(0, 0);
            DayTitle.Name = "DayTitle";
            DayTitle.Size = new Size(583, 82);
            DayTitle.TabIndex = 0;
            DayTitle.Text = "Maandag";
            DayTitle.TextAlign = ContentAlignment.BottomCenter;
            // 
            // RecipePickerDayControl
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ControlPanel);
            Name = "RecipePickerDayControl";
            Size = new Size(603, 735);
            ControlPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)RecipesSelector).EndInit();
            InfoPanel.ResumeLayout(false);
            InfoPanel.PerformLayout();
            ToolbarPanel.ResumeLayout(false);
            ToolbarPanel.PerformLayout();
            TitlePanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel ControlPanel;
        private Panel ToolbarPanel;
        private Panel TitlePanel;
        private Label DayTitle;
        private Panel InfoPanel;
        private Label SelectedRecipe;
        private DataGridView RecipesSelector;
        private Label label1;
        private ComboBox IngredientsFilter;
    }
}
