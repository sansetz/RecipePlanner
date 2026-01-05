namespace RecipePlanner.UI {
    partial class RecipeEditForm {
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
            Cancel = new Button();
            SaveRecipe = new Button();
            label1 = new Label();
            RecipeName = new TextBox();
            PrepTimeSelector = new ComboBox();
            label2 = new Label();
            SuspendLayout();
            // 
            // Cancel
            // 
            Cancel.DialogResult = DialogResult.Cancel;
            Cancel.Location = new Point(374, 362);
            Cancel.Name = "Cancel";
            Cancel.Size = new Size(236, 59);
            Cancel.TabIndex = 9;
            Cancel.Text = "Annuleren";
            Cancel.UseVisualStyleBackColor = true;
            Cancel.Click += Cancel_Click;
            // 
            // SaveRecipe
            // 
            SaveRecipe.DialogResult = DialogResult.OK;
            SaveRecipe.Location = new Point(616, 362);
            SaveRecipe.Name = "SaveRecipe";
            SaveRecipe.Size = new Size(236, 59);
            SaveRecipe.TabIndex = 8;
            SaveRecipe.Text = "Opslaan";
            SaveRecipe.UseVisualStyleBackColor = true;
            SaveRecipe.Click += SaveRecipe_ClickAsync;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(36, 33);
            label1.Name = "label1";
            label1.Size = new Size(77, 32);
            label1.TabIndex = 6;
            label1.Text = "Naam";
            // 
            // RecipeName
            // 
            RecipeName.Location = new Point(212, 26);
            RecipeName.Name = "RecipeName";
            RecipeName.Size = new Size(472, 39);
            RecipeName.TabIndex = 7;
            // 
            // PrepTimeSelector
            // 
            PrepTimeSelector.FormattingEnabled = true;
            PrepTimeSelector.Location = new Point(212, 89);
            PrepTimeSelector.Name = "PrepTimeSelector";
            PrepTimeSelector.Size = new Size(152, 40);
            PrepTimeSelector.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(36, 97);
            label2.Name = "label2";
            label2.Size = new Size(160, 32);
            label2.TabIndex = 11;
            label2.Text = "Bereidingstijd";
            // 
            // RecipeEditForm
            // 
            AcceptButton = SaveRecipe;
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = Cancel;
            ClientSize = new Size(882, 450);
            Controls.Add(label2);
            Controls.Add(PrepTimeSelector);
            Controls.Add(Cancel);
            Controls.Add(SaveRecipe);
            Controls.Add(label1);
            Controls.Add(RecipeName);
            Name = "RecipeEditForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Bewerk Recept";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Cancel;
        private Button SaveRecipe;
        private Label label1;
        private TextBox RecipeName;
        private ComboBox PrepTimeSelector;
        private Label label2;
    }
}