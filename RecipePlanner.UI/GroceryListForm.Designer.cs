namespace RecipePlanner.UI {
    partial class GroceryListForm {
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
            GroceryList = new RichTextBox();
            SuspendLayout();
            // 
            // GroceryList
            // 
            GroceryList.Location = new Point(43, 35);
            GroceryList.Name = "GroceryList";
            GroceryList.Size = new Size(723, 386);
            GroceryList.TabIndex = 0;
            GroceryList.Text = "";
            // 
            // GroceryListForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(GroceryList);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "GroceryListForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ShoppingListForm";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox GroceryList;
    }
}