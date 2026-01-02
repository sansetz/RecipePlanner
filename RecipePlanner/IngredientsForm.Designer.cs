namespace RecipePlanner.UI {
    partial class IngredientsForm {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            panel1 = new Panel();
            NewIngredient = new Button();
            SaveIngredient = new Button();
            label2 = new Label();
            UnitSelector = new ComboBox();
            label1 = new Label();
            IngredientName = new TextBox();
            IngredientsGrid = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)IngredientsGrid).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(NewIngredient);
            panel1.Controls.Add(SaveIngredient);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(UnitSelector);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(IngredientName);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(996, 163);
            panel1.TabIndex = 0;
            // 
            // NewIngredient
            // 
            NewIngredient.Location = new Point(715, 12);
            NewIngredient.Name = "NewIngredient";
            NewIngredient.Size = new Size(236, 57);
            NewIngredient.TabIndex = 6;
            NewIngredient.Text = "Nieuw ingredient";
            NewIngredient.UseVisualStyleBackColor = true;
            NewIngredient.Click += NewIngredient_Click;
            // 
            // SaveIngredient
            // 
            SaveIngredient.Location = new Point(715, 79);
            SaveIngredient.Name = "SaveIngredient";
            SaveIngredient.Size = new Size(236, 59);
            SaveIngredient.TabIndex = 5;
            SaveIngredient.Text = "Opslaan";
            SaveIngredient.UseVisualStyleBackColor = true;
            SaveIngredient.Click += SaveIngredient_ClickAsync;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 82);
            label2.Name = "label2";
            label2.Size = new Size(143, 32);
            label2.TabIndex = 4;
            label2.Text = "Default Unit";
            // 
            // UnitSelector
            // 
            UnitSelector.FormattingEnabled = true;
            UnitSelector.Location = new Point(206, 79);
            UnitSelector.Name = "UnitSelector";
            UnitSelector.Size = new Size(360, 40);
            UnitSelector.TabIndex = 3;
            UnitSelector.SelectedIndexChanged += UnitSelector_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 30);
            label1.Name = "label1";
            label1.Size = new Size(78, 32);
            label1.TabIndex = 2;
            label1.Text = "Name";
            // 
            // IngredientName
            // 
            IngredientName.Location = new Point(206, 23);
            IngredientName.Name = "IngredientName";
            IngredientName.Size = new Size(360, 39);
            IngredientName.TabIndex = 0;
            IngredientName.TextChanged += IngredientName_TextChanged;
            // 
            // IngredientsGrid
            // 
            IngredientsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            IngredientsGrid.Dock = DockStyle.Fill;
            IngredientsGrid.Location = new Point(0, 163);
            IngredientsGrid.Name = "IngredientsGrid";
            IngredientsGrid.RowHeadersWidth = 82;
            IngredientsGrid.Size = new Size(996, 461);
            IngredientsGrid.TabIndex = 1;
            IngredientsGrid.SelectionChanged += IngredientsGrid_SelectionChangedAsync;
            // 
            // IngredientsForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(996, 624);
            Controls.Add(IngredientsGrid);
            Controls.Add(panel1);
            Name = "IngredientsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ingredients Form";
            Load += IngredientsForm_LoadAsync;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)IngredientsGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TextBox textBox2;
        private TextBox IngredientName;
        private DataGridView IngredientsGrid;
        private Label label2;
        private ComboBox UnitSelector;
        private Label label1;
        private Button SaveIngredient;
        private Button NewIngredient;
    }
}