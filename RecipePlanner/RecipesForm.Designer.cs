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
            RecipesListView = new EntityListViewControl();
            SuspendLayout();
            // 
            // RecipesListView
            // 
            RecipesListView.Dock = DockStyle.Fill;
            RecipesListView.Location = new Point(0, 0);
            RecipesListView.Name = "RecipesListView";
            RecipesListView.Size = new Size(771, 647);
            RecipesListView.TabIndex = 0;
            // 
            // RecipesForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(771, 647);
            Controls.Add(RecipesListView);
            Name = "RecipesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Recepten";
            Load += frmRecipes_LoadAsync;
            ResumeLayout(false);
        }

        #endregion

        private EntityListViewControl RecipesListView;
    }
}