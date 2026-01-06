namespace RecipePlanner.UI {
    partial class RecipeIngredientEditForm {
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
            SaveIngredient = new Button();
            IngredientSelector = new ComboBox();
            label2 = new Label();
            UnitSelector = new ComboBox();
            label1 = new Label();
            label3 = new Label();
            Quantity = new TextBox();
            SuspendLayout();
            // 
            // Cancel
            // 
            Cancel.DialogResult = DialogResult.Cancel;
            Cancel.Location = new Point(215, 242);
            Cancel.Name = "Cancel";
            Cancel.Size = new Size(236, 59);
            Cancel.TabIndex = 7;
            Cancel.Text = "Annuleren";
            Cancel.UseVisualStyleBackColor = true;
            Cancel.Click += Cancel_Click;
            // 
            // SaveIngredient
            // 
            SaveIngredient.DialogResult = DialogResult.OK;
            SaveIngredient.Location = new Point(457, 242);
            SaveIngredient.Name = "SaveIngredient";
            SaveIngredient.Size = new Size(236, 59);
            SaveIngredient.TabIndex = 6;
            SaveIngredient.Text = "Opslaan";
            SaveIngredient.UseVisualStyleBackColor = true;
            SaveIngredient.Click += SaveIngredient_Click;
            // 
            // IngredientSelector
            // 
            IngredientSelector.FormattingEnabled = true;
            IngredientSelector.Location = new Point(231, 41);
            IngredientSelector.Name = "IngredientSelector";
            IngredientSelector.Size = new Size(462, 40);
            IngredientSelector.TabIndex = 8;
            IngredientSelector.SelectedIndexChanged += IngredientSelector_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 103);
            label2.Name = "label2";
            label2.Size = new Size(201, 32);
            label2.TabIndex = 9;
            label2.Text = "Eenheid in recept";
            // 
            // UnitSelector
            // 
            UnitSelector.FormattingEnabled = true;
            UnitSelector.Location = new Point(231, 103);
            UnitSelector.Name = "UnitSelector";
            UnitSelector.Size = new Size(306, 40);
            UnitSelector.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 41);
            label1.Name = "label1";
            label1.Size = new Size(124, 32);
            label1.TabIndex = 11;
            label1.Text = "Ingredient";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(19, 170);
            label3.Name = "label3";
            label3.Size = new Size(196, 32);
            label3.TabIndex = 12;
            label3.Text = "Aantal eenheden";
            // 
            // Quantity
            // 
            Quantity.Location = new Point(231, 163);
            Quantity.Name = "Quantity";
            Quantity.Size = new Size(170, 39);
            Quantity.TabIndex = 13;
            // 
            // RecipeIngredientEditForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(727, 326);
            Controls.Add(Quantity);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(UnitSelector);
            Controls.Add(IngredientSelector);
            Controls.Add(Cancel);
            Controls.Add(SaveIngredient);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "RecipeIngredientEditForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "RecipeIngredientEditForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Cancel;
        private Button SaveIngredient;
        private ComboBox IngredientSelector;
        private Label label2;
        private ComboBox UnitSelector;
        private Label label1;
        private Label label3;
        private TextBox Quantity;
    }
}