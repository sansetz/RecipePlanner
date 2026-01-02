namespace RecipePlanner.UI {
    partial class frmRecipes {
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
            ((System.ComponentModel.ISupportInitialize)gridRecipes).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1018, 175);
            panel1.TabIndex = 0;
            // 
            // gridRecipes
            // 
            gridRecipes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridRecipes.Dock = DockStyle.Fill;
            gridRecipes.Location = new Point(0, 175);
            gridRecipes.Name = "gridRecipes";
            gridRecipes.RowHeadersWidth = 82;
            gridRecipes.Size = new Size(1018, 472);
            gridRecipes.TabIndex = 1;
            // 
            // frmRecipes
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1018, 647);
            Controls.Add(gridRecipes);
            Controls.Add(panel1);
            Name = "frmRecipes";
            Text = "frmRecipe";
            Load += frmRecipes_LoadAsync;
            ((System.ComponentModel.ISupportInitialize)gridRecipes).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private DataGridView gridRecipes;
    }
}