namespace RecipePlanner.UI {
    partial class IngredientEditForm {
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
            SaveIngredient = new Button();
            label2 = new Label();
            UnitSelector = new ComboBox();
            label1 = new Label();
            IngredientName = new TextBox();
            Cancel = new Button();
            SuspendLayout();
            // 
            // SaveIngredient
            // 
            SaveIngredient.DialogResult = DialogResult.OK;
            SaveIngredient.Location = new Point(599, 231);
            SaveIngredient.Name = "SaveIngredient";
            SaveIngredient.Size = new Size(236, 59);
            SaveIngredient.TabIndex = 4;
            SaveIngredient.Text = "Opslaan";
            SaveIngredient.UseVisualStyleBackColor = true;
            SaveIngredient.Click += SaveIngredient_ClickAsync;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 97);
            label2.Name = "label2";
            label2.Size = new Size(100, 32);
            label2.TabIndex = 2;
            label2.Text = "Eenheid";
            // 
            // UnitSelector
            // 
            UnitSelector.FormattingEnabled = true;
            UnitSelector.Location = new Point(206, 97);
            UnitSelector.Name = "UnitSelector";
            UnitSelector.Size = new Size(306, 40);
            UnitSelector.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 45);
            label1.Name = "label1";
            label1.Size = new Size(77, 32);
            label1.TabIndex = 0;
            label1.Text = "Naam";
            // 
            // IngredientName
            // 
            IngredientName.Location = new Point(206, 38);
            IngredientName.Name = "IngredientName";
            IngredientName.Size = new Size(472, 39);
            IngredientName.TabIndex = 1;
            // 
            // Cancel
            // 
            Cancel.DialogResult = DialogResult.Cancel;
            Cancel.Location = new Point(357, 231);
            Cancel.Name = "Cancel";
            Cancel.Size = new Size(236, 59);
            Cancel.TabIndex = 5;
            Cancel.Text = "Annuleren";
            Cancel.UseVisualStyleBackColor = true;
            Cancel.Click += Cancel_Click;
            // 
            // IngredientEditForm
            // 
            AcceptButton = SaveIngredient;
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = Cancel;
            ClientSize = new Size(866, 313);
            Controls.Add(Cancel);
            Controls.Add(SaveIngredient);
            Controls.Add(label2);
            Controls.Add(UnitSelector);
            Controls.Add(label1);
            Controls.Add(IngredientName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "IngredientEditForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Bewerk ingredient";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SaveIngredient;
        private Label label2;
        private ComboBox UnitSelector;
        private Label label1;
        private TextBox IngredientName;
        private Button Cancel;
    }
}