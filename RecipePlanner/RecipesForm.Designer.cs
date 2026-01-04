namespace RecipePlanner.UI {
    partial class RecipesForm {
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
            gridRecipes = new DataGridView();
            DeleteIngredient = new Button();
            UpdateIngredient = new Button();
            NewIngredient = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridRecipes).BeginInit();
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
            panel1.Size = new Size(771, 83);
            panel1.TabIndex = 0;
            // 
            // gridRecipes
            // 
            gridRecipes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridRecipes.Dock = DockStyle.Fill;
            gridRecipes.Location = new Point(0, 83);
            gridRecipes.Name = "gridRecipes";
            gridRecipes.RowHeadersWidth = 82;
            gridRecipes.Size = new Size(771, 564);
            gridRecipes.TabIndex = 1;
            // 
            // DeleteIngredient
            // 
            DeleteIngredient.Location = new Point(515, 12);
            DeleteIngredient.Name = "DeleteIngredient";
            DeleteIngredient.Size = new Size(236, 57);
            DeleteIngredient.TabIndex = 11;
            DeleteIngredient.Text = "Delete";
            DeleteIngredient.UseVisualStyleBackColor = true;
            // 
            // UpdateIngredient
            // 
            UpdateIngredient.Location = new Point(264, 12);
            UpdateIngredient.Name = "UpdateIngredient";
            UpdateIngredient.Size = new Size(236, 57);
            UpdateIngredient.TabIndex = 10;
            UpdateIngredient.Text = "Bewerk";
            UpdateIngredient.UseVisualStyleBackColor = true;
            // 
            // NewIngredient
            // 
            NewIngredient.Location = new Point(10, 12);
            NewIngredient.Name = "NewIngredient";
            NewIngredient.Size = new Size(236, 57);
            NewIngredient.TabIndex = 9;
            NewIngredient.Text = "Nieuw";
            NewIngredient.UseVisualStyleBackColor = true;
            // 
            // RecipesForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(771, 647);
            Controls.Add(gridRecipes);
            Controls.Add(panel1);
            Name = "RecipesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmRecipe";
            Load += frmRecipes_LoadAsync;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridRecipes).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private DataGridView gridRecipes;
        private Button DeleteIngredient;
        private Button UpdateIngredient;
        private Button NewIngredient;
    }
}