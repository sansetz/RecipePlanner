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
            DeleteIngredient = new Button();
            UpdateIngredient = new Button();
            NewIngredient = new Button();
            IngredientsGrid = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)IngredientsGrid).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(DeleteIngredient);
            panel1.Controls.Add(UpdateIngredient);
            panel1.Controls.Add(NewIngredient);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(799, 98);
            panel1.TabIndex = 0;
            // 
            // DeleteIngredient
            // 
            DeleteIngredient.Location = new Point(531, 21);
            DeleteIngredient.Name = "DeleteIngredient";
            DeleteIngredient.Size = new Size(236, 57);
            DeleteIngredient.TabIndex = 8;
            DeleteIngredient.Text = "Delete";
            DeleteIngredient.UseVisualStyleBackColor = true;
            // 
            // UpdateIngredient
            // 
            UpdateIngredient.Location = new Point(280, 21);
            UpdateIngredient.Name = "UpdateIngredient";
            UpdateIngredient.Size = new Size(236, 57);
            UpdateIngredient.TabIndex = 7;
            UpdateIngredient.Text = "Bewerk";
            UpdateIngredient.UseVisualStyleBackColor = true;
            UpdateIngredient.Click += UpdateIngredient_ClickAsync;
            // 
            // NewIngredient
            // 
            NewIngredient.Location = new Point(26, 21);
            NewIngredient.Name = "NewIngredient";
            NewIngredient.Size = new Size(236, 57);
            NewIngredient.TabIndex = 6;
            NewIngredient.Text = "Nieuw";
            NewIngredient.UseVisualStyleBackColor = true;
            NewIngredient.Click += NewIngredient_ClickAsync;
            // 
            // IngredientsGrid
            // 
            IngredientsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            IngredientsGrid.Dock = DockStyle.Fill;
            IngredientsGrid.Location = new Point(0, 98);
            IngredientsGrid.Name = "IngredientsGrid";
            IngredientsGrid.RowHeadersWidth = 82;
            IngredientsGrid.ScrollBars = ScrollBars.Vertical;
            IngredientsGrid.Size = new Size(799, 872);
            IngredientsGrid.TabIndex = 1;
            IngredientsGrid.SelectionChanged += IngredientsGrid_SelectionChanged;
            // 
            // IngredientsForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(799, 970);
            Controls.Add(IngredientsGrid);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "IngredientsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ingredienten";
            Load += IngredientsForm_LoadAsync;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)IngredientsGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TextBox textBox2;
        private DataGridView IngredientsGrid;
        private Button NewIngredient;
        private Button DeleteIngredient;
        private Button UpdateIngredient;
    }
}