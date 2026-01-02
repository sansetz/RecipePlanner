namespace RecipePlanner
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            IngredientsButton = new Button();
            RecipesButton = new Button();
            SuspendLayout();
            // 
            // IngredientsButton
            // 
            IngredientsButton.Location = new Point(153, 82);
            IngredientsButton.Name = "IngredientsButton";
            IngredientsButton.Size = new Size(248, 71);
            IngredientsButton.TabIndex = 2;
            IngredientsButton.Text = "Ingredients";
            IngredientsButton.UseVisualStyleBackColor = true;
            IngredientsButton.Click += IngredientsButton_Click;
            // 
            // RecipesButton
            // 
            RecipesButton.Location = new Point(153, 172);
            RecipesButton.Name = "RecipesButton";
            RecipesButton.Size = new Size(248, 68);
            RecipesButton.TabIndex = 3;
            RecipesButton.Text = "Recipes";
            RecipesButton.UseVisualStyleBackColor = true;
            RecipesButton.Click += RecipesButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1050, 605);
            Controls.Add(RecipesButton);
            Controls.Add(IngredientsButton);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "My Recipe Planner";
            ResumeLayout(false);
        }

        #endregion
        private Button IngredientsButton;
        private Button RecipesButton;
    }
}
