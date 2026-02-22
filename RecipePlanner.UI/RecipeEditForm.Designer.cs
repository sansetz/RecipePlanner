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
            panel1 = new Panel();
            IngredientsListView = new EntityListViewControl();
            label3 = new Label();
            label4 = new Label();
            RecipeInfo = new TextBox();
            NoFreshIngredients = new CheckBox();
            label5 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // Cancel
            // 
            Cancel.DialogResult = DialogResult.Cancel;
            Cancel.Location = new Point(192, 372);
            Cancel.Margin = new Padding(2, 1, 2, 1);
            Cancel.Name = "Cancel";
            Cancel.Size = new Size(127, 28);
            Cancel.TabIndex = 9;
            Cancel.Text = "Annuleren";
            Cancel.UseVisualStyleBackColor = true;
            Cancel.Click += Cancel_Click;
            // 
            // SaveRecipe
            // 
            SaveRecipe.DialogResult = DialogResult.OK;
            SaveRecipe.Location = new Point(314, 372);
            SaveRecipe.Margin = new Padding(2, 1, 2, 1);
            SaveRecipe.Name = "SaveRecipe";
            SaveRecipe.Size = new Size(127, 28);
            SaveRecipe.TabIndex = 8;
            SaveRecipe.Text = "Opslaan";
            SaveRecipe.UseVisualStyleBackColor = true;
            SaveRecipe.Click += SaveRecipe_ClickAsync;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 12);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 6;
            label1.Text = "Naam";
            // 
            // RecipeName
            // 
            RecipeName.Location = new Point(163, 12);
            RecipeName.Margin = new Padding(2, 1, 2, 1);
            RecipeName.Name = "RecipeName";
            RecipeName.Size = new Size(279, 23);
            RecipeName.TabIndex = 7;
            // 
            // PrepTimeSelector
            // 
            PrepTimeSelector.FormattingEnabled = true;
            PrepTimeSelector.Location = new Point(163, 42);
            PrepTimeSelector.Margin = new Padding(2, 1, 2, 1);
            PrepTimeSelector.Name = "PrepTimeSelector";
            PrepTimeSelector.Size = new Size(84, 23);
            PrepTimeSelector.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 42);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(79, 15);
            label2.TabIndex = 11;
            label2.Text = "Bereidingstijd";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(IngredientsListView);
            panel1.Location = new Point(19, 153);
            panel1.Margin = new Padding(2, 1, 2, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(423, 206);
            panel1.TabIndex = 12;
            // 
            // IngredientsListView
            // 
            IngredientsListView.Dock = DockStyle.Fill;
            IngredientsListView.Location = new Point(0, 0);
            IngredientsListView.Margin = new Padding(1, 0, 1, 0);
            IngredientsListView.Name = "IngredientsListView";
            IngredientsListView.Size = new Size(421, 204);
            IngredientsListView.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(19, 131);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(159, 15);
            label3.TabIndex = 13;
            label3.Text = "Aan te schaffen ingredienten";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(19, 70);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(67, 15);
            label4.TabIndex = 14;
            label4.Text = "Recept info";
            // 
            // RecipeInfo
            // 
            RecipeInfo.Location = new Point(163, 70);
            RecipeInfo.Margin = new Padding(2, 1, 2, 1);
            RecipeInfo.Name = "RecipeInfo";
            RecipeInfo.Size = new Size(279, 23);
            RecipeInfo.TabIndex = 15;
            // 
            // NoFreshIngredients
            // 
            NoFreshIngredients.AutoSize = true;
            NoFreshIngredients.Location = new Point(163, 97);
            NoFreshIngredients.Name = "NoFreshIngredients";
            NoFreshIngredients.Size = new Size(15, 14);
            NoFreshIngredients.TabIndex = 16;
            NoFreshIngredients.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(21, 98);
            label5.Name = "label5";
            label5.Size = new Size(134, 15);
            label5.TabIndex = 17;
            label5.Text = "Geen verse ingredienten";
            // 
            // RecipeEditForm
            // 
            AcceptButton = SaveRecipe;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = Cancel;
            ClientSize = new Size(457, 414);
            Controls.Add(label5);
            Controls.Add(NoFreshIngredients);
            Controls.Add(RecipeInfo);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(panel1);
            Controls.Add(label2);
            Controls.Add(PrepTimeSelector);
            Controls.Add(Cancel);
            Controls.Add(SaveRecipe);
            Controls.Add(label1);
            Controls.Add(RecipeName);
            Margin = new Padding(2, 1, 2, 1);
            Name = "RecipeEditForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Bewerk Recept";
            Load += RecipeEditForm_Load;
            panel1.ResumeLayout(false);
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
        private Panel panel1;
        private EntityListViewControl IngredientsListView;
        private Label label3;
        private Label label4;
        private TextBox RecipeInfo;
        private CheckBox NoFreshIngredients;
        private Label label5;
    }
}