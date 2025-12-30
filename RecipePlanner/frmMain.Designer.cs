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
            btnSeedTest = new Button();
            btnTest = new Button();
            btnIngredients = new Button();
            SuspendLayout();
            // 
            // btnSeedTest
            // 
            btnSeedTest.Location = new Point(68, 36);
            btnSeedTest.Name = "btnSeedTest";
            btnSeedTest.Size = new Size(143, 162);
            btnSeedTest.TabIndex = 0;
            btnSeedTest.Text = "Seed Test";
            btnSeedTest.UseVisualStyleBackColor = true;
            btnSeedTest.Click += btnSeedTest_ClickAsync;
            // 
            // btnTest
            // 
            btnTest.Location = new Point(475, 112);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(150, 46);
            btnTest.TabIndex = 1;
            btnTest.Text = "Test";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click += btnTest_ClickAsync;
            // 
            // btnIngredients
            // 
            btnIngredients.Location = new Point(604, 232);
            btnIngredients.Name = "btnIngredients";
            btnIngredients.Size = new Size(123, 39);
            btnIngredients.TabIndex = 2;
            btnIngredients.Text = "Ingredients";
            btnIngredients.UseVisualStyleBackColor = true;
            btnIngredients.Click += btnIngredients_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnIngredients);
            Controls.Add(btnTest);
            Controls.Add(btnSeedTest);
            Name = "frmMain";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnSeedTest;
        private Button btnTest;
        private Button btnIngredients;
    }
}
