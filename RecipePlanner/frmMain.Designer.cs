namespace RecipePlanner
{
    partial class frmMain
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
            btnIngredients = new Button();
            btnRecipes = new Button();
            SuspendLayout();
            // 
            // btnIngredients
            // 
            btnIngredients.Location = new Point(153, 82);
            btnIngredients.Name = "btnIngredients";
            btnIngredients.Size = new Size(248, 71);
            btnIngredients.TabIndex = 2;
            btnIngredients.Text = "Ingredients";
            btnIngredients.UseVisualStyleBackColor = true;
            btnIngredients.Click += btnIngredients_Click;
            // 
            // btnRecipes
            // 
            btnRecipes.Location = new Point(153, 172);
            btnRecipes.Name = "btnRecipes";
            btnRecipes.Size = new Size(248, 68);
            btnRecipes.TabIndex = 3;
            btnRecipes.Text = "Recipes";
            btnRecipes.UseVisualStyleBackColor = true;
            btnRecipes.Click += btnRecipes_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnRecipes);
            Controls.Add(btnIngredients);
            Name = "frmMain";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion
        private Button btnIngredients;
        private Button btnRecipes;
    }
}
