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
            IngredientsListView = new EntityListViewControl();
            SuspendLayout();
            // 
            // IngredientsListView
            // 
            IngredientsListView.Dock = DockStyle.Fill;
            IngredientsListView.Location = new Point(0, 0);
            IngredientsListView.Name = "IngredientsListView";
            IngredientsListView.Size = new Size(1258, 970);
            IngredientsListView.TabIndex = 0;
            // 
            // IngredientsForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1258, 970);
            Controls.Add(IngredientsListView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "IngredientsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ingredienten";
            Load += IngredientsForm_LoadAsync;
            ResumeLayout(false);
        }

        #endregion
        private EntityListViewControl IngredientsListView;
    }
}