namespace RecipePlanner.UI {
    partial class frmIngredients {
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
            panel1 = new Panel();
            btnSave = new Button();
            label2 = new Label();
            cboUnit = new ComboBox();
            label1 = new Label();
            txtName = new TextBox();
            gridIngredients = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridIngredients).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnSave);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(cboUnit);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtName);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(996, 163);
            panel1.TabIndex = 0;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(674, 45);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(139, 59);
            btnSave.TabIndex = 5;
            btnSave.Text = "button1";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 82);
            label2.Name = "label2";
            label2.Size = new Size(143, 32);
            label2.TabIndex = 4;
            label2.Text = "Default Unit";
            // 
            // cboUnit
            // 
            cboUnit.FormattingEnabled = true;
            cboUnit.Location = new Point(206, 79);
            cboUnit.Name = "cboUnit";
            cboUnit.Size = new Size(360, 40);
            cboUnit.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 30);
            label1.Name = "label1";
            label1.Size = new Size(78, 32);
            label1.TabIndex = 2;
            label1.Text = "Name";
            // 
            // txtName
            // 
            txtName.Location = new Point(206, 23);
            txtName.Name = "txtName";
            txtName.Size = new Size(360, 39);
            txtName.TabIndex = 0;
            // 
            // gridIngredients
            // 
            gridIngredients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridIngredients.Dock = DockStyle.Fill;
            gridIngredients.Location = new Point(0, 163);
            gridIngredients.Name = "gridIngredients";
            gridIngredients.RowHeadersWidth = 82;
            gridIngredients.Size = new Size(996, 461);
            gridIngredients.TabIndex = 1;
            // 
            // frmIngredients
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(996, 624);
            Controls.Add(gridIngredients);
            Controls.Add(panel1);
            Name = "frmIngredients";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmIngredients";
            Load += frmIngredients_LoadAsync;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridIngredients).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TextBox textBox2;
        private TextBox txtName;
        private DataGridView gridIngredients;
        private Label label2;
        private ComboBox cboUnit;
        private Label label1;
        private Button btnSave;
    }
}