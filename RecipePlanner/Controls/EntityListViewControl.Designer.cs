namespace RecipePlanner.UI {
    partial class EntityListViewControl {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            Buttons = new Panel();
            DeleteItem = new Button();
            UpdateItem = new Button();
            AddItem = new Button();
            ListItemsGrid = new DataGridView();
            Buttons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ListItemsGrid).BeginInit();
            SuspendLayout();
            // 
            // Buttons
            // 
            Buttons.Controls.Add(DeleteItem);
            Buttons.Controls.Add(UpdateItem);
            Buttons.Controls.Add(AddItem);
            Buttons.Dock = DockStyle.Top;
            Buttons.Location = new Point(0, 0);
            Buttons.Name = "Buttons";
            Buttons.Size = new Size(778, 94);
            Buttons.TabIndex = 0;
            // 
            // DeleteItem
            // 
            DeleteItem.Location = new Point(523, 16);
            DeleteItem.Name = "DeleteItem";
            DeleteItem.Size = new Size(236, 57);
            DeleteItem.TabIndex = 14;
            DeleteItem.Text = "Verwijderen";
            DeleteItem.UseVisualStyleBackColor = true;
            DeleteItem.Click += DeleteItem_Click;
            // 
            // UpdateItem
            // 
            UpdateItem.Location = new Point(272, 16);
            UpdateItem.Name = "UpdateItem";
            UpdateItem.Size = new Size(236, 57);
            UpdateItem.TabIndex = 13;
            UpdateItem.Text = "Aanpassen";
            UpdateItem.UseVisualStyleBackColor = true;
            UpdateItem.Click += UpdateItem_Click;
            // 
            // AddItem
            // 
            AddItem.Location = new Point(18, 16);
            AddItem.Name = "AddItem";
            AddItem.Size = new Size(236, 57);
            AddItem.TabIndex = 12;
            AddItem.Text = "Toevoegen";
            AddItem.UseVisualStyleBackColor = true;
            AddItem.Click += AddItem_Click;
            // 
            // ListItemsGrid
            // 
            ListItemsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ListItemsGrid.Dock = DockStyle.Fill;
            ListItemsGrid.Location = new Point(0, 94);
            ListItemsGrid.Name = "ListItemsGrid";
            ListItemsGrid.RowHeadersVisible = false;
            ListItemsGrid.RowHeadersWidth = 82;
            ListItemsGrid.Size = new Size(778, 744);
            ListItemsGrid.TabIndex = 1;
            ListItemsGrid.SelectionChanged += ListItemsGrid_SelectionChanged;
            // 
            // EntityListViewControl
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ListItemsGrid);
            Controls.Add(Buttons);
            Name = "EntityListViewControl";
            Size = new Size(778, 838);
            Buttons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ListItemsGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel Buttons;
        private DataGridView ListItemsGrid;
        private Button DeleteItem;
        private Button UpdateItem;
        private Button AddItem;
    }
}
