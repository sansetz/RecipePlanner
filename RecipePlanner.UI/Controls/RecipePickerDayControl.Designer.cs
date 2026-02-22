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
            NoFreshIngredientsFilter = new CheckBox();
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
            ControlPanel.Margin = new Padding(2, 1, 2, 1);
            ControlPanel.Name = "ControlPanel";
            ControlPanel.Padding = new Padding(5);
            ControlPanel.Size = new Size(325, 345);
            ControlPanel.TabIndex = 0;
            // 
            // RecipesSelector
            // 
            RecipesSelector.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            RecipesSelector.ColumnHeadersVisible = false;
            RecipesSelector.Dock = DockStyle.Fill;
            RecipesSelector.EnableHeadersVisualStyles = false;
            RecipesSelector.Location = new Point(5, 71);
            RecipesSelector.Margin = new Padding(2, 1, 2, 1);
            RecipesSelector.Name = "RecipesSelector";
            RecipesSelector.RowHeadersVisible = false;
            RecipesSelector.RowHeadersWidth = 82;
            RecipesSelector.Size = new Size(315, 248);
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
            InfoPanel.Location = new Point(5, 319);
            InfoPanel.Margin = new Padding(2, 1, 2, 1);
            InfoPanel.Name = "InfoPanel";
            InfoPanel.Size = new Size(315, 21);
            InfoPanel.TabIndex = 4;
            // 
            // SelectedRecipe
            // 
            SelectedRecipe.AutoSize = true;
            SelectedRecipe.Location = new Point(2, 0);
            SelectedRecipe.Margin = new Padding(2, 0, 2, 0);
            SelectedRecipe.Name = "SelectedRecipe";
            SelectedRecipe.Size = new Size(38, 15);
            SelectedRecipe.TabIndex = 0;
            SelectedRecipe.Text = "label2";
            // 
            // ToolbarPanel
            // 
            ToolbarPanel.BackColor = Color.Black;
            ToolbarPanel.Controls.Add(NoFreshIngredientsFilter);
            ToolbarPanel.Controls.Add(label1);
            ToolbarPanel.Controls.Add(IngredientsFilter);
            ToolbarPanel.Dock = DockStyle.Top;
            ToolbarPanel.Location = new Point(5, 43);
            ToolbarPanel.Margin = new Padding(2, 1, 2, 1);
            ToolbarPanel.Name = "ToolbarPanel";
            ToolbarPanel.Padding = new Padding(5);
            ToolbarPanel.Size = new Size(315, 28);
            ToolbarPanel.TabIndex = 2;
            // 
            // NoFreshIngredientsFilter
            // 
            NoFreshIngredientsFilter.AutoSize = true;
            NoFreshIngredientsFilter.Location = new Point(128, 7);
            NoFreshIngredientsFilter.Name = "NoFreshIngredientsFilter";
            NoFreshIngredientsFilter.Size = new Size(15, 14);
            NoFreshIngredientsFilter.TabIndex = 2;
            NoFreshIngredientsFilter.UseVisualStyleBackColor = true;
            NoFreshIngredientsFilter.CheckedChanged += NoFreshIngredientsFilter_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Left;
            label1.ForeColor = Color.White;
            label1.Location = new Point(5, 5);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(33, 15);
            label1.TabIndex = 1;
            label1.Text = "Filter";
            // 
            // IngredientsFilter
            // 
            IngredientsFilter.Dock = DockStyle.Right;
            IngredientsFilter.FormattingEnabled = true;
            IngredientsFilter.Location = new Point(189, 5);
            IngredientsFilter.Margin = new Padding(5, 23, 5, 5);
            IngredientsFilter.Name = "IngredientsFilter";
            IngredientsFilter.Size = new Size(121, 23);
            IngredientsFilter.TabIndex = 0;
            IngredientsFilter.SelectionChangeCommitted += IngredientsFilter_SelectionChangeCommitted;
            // 
            // TitlePanel
            // 
            TitlePanel.BackColor = Color.Black;
            TitlePanel.Controls.Add(DayTitle);
            TitlePanel.Dock = DockStyle.Top;
            TitlePanel.Location = new Point(5, 5);
            TitlePanel.Margin = new Padding(2, 1, 2, 1);
            TitlePanel.Name = "TitlePanel";
            TitlePanel.Size = new Size(315, 38);
            TitlePanel.TabIndex = 0;
            // 
            // DayTitle
            // 
            DayTitle.BackColor = Color.Transparent;
            DayTitle.Dock = DockStyle.Fill;
            DayTitle.Font = new Font("Bernard MT Condensed", 25.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DayTitle.ForeColor = Color.SteelBlue;
            DayTitle.Location = new Point(0, 0);
            DayTitle.Margin = new Padding(2, 0, 2, 0);
            DayTitle.Name = "DayTitle";
            DayTitle.Size = new Size(315, 38);
            DayTitle.TabIndex = 0;
            DayTitle.Text = "Maandag";
            DayTitle.TextAlign = ContentAlignment.BottomCenter;
            // 
            // RecipePickerDayControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ControlPanel);
            Margin = new Padding(2, 1, 2, 1);
            Name = "RecipePickerDayControl";
            Size = new Size(325, 345);
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
        private CheckBox NoFreshIngredientsFilter;
    }
}
