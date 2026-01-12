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
            panel2 = new Panel();
            panel3 = new Panel();
            Exit = new Button();
            panel4 = new Panel();
            BottomDaysPanel = new Panel();
            Sunday = new Panel();
            SundayRecipePicker = new RecipePlanner.UI.Controls.RecipePickerDayControl();
            Saturday = new Panel();
            SaturdayRecipePicker = new RecipePlanner.UI.Controls.RecipePickerDayControl();
            Friday = new Panel();
            FridayRecipePicker = new RecipePlanner.UI.Controls.RecipePickerDayControl();
            panel1 = new Panel();
            TopDaysPanel = new Panel();
            Thursday = new Panel();
            ThursdayRecipePicker = new RecipePlanner.UI.Controls.RecipePickerDayControl();
            Wednesday = new Panel();
            WednesdayRecipePicker = new RecipePlanner.UI.Controls.RecipePickerDayControl();
            Tuesday = new Panel();
            TuesdayRecipePicker = new RecipePlanner.UI.Controls.RecipePickerDayControl();
            Monday = new Panel();
            MondayRecipePicker = new RecipePlanner.UI.Controls.RecipePickerDayControl();
            MenuPanel = new Panel();
            StartDatePicker = new DateTimePicker();
            ShoppingList = new Button();
            RecipesButton = new Button();
            IngredientsButton = new Button();
            panel4.SuspendLayout();
            BottomDaysPanel.SuspendLayout();
            Sunday.SuspendLayout();
            Saturday.SuspendLayout();
            Friday.SuspendLayout();
            TopDaysPanel.SuspendLayout();
            Thursday.SuspendLayout();
            Wednesday.SuspendLayout();
            Tuesday.SuspendLayout();
            Monday.SuspendLayout();
            MenuPanel.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = Color.Black;
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(225, 1761);
            panel2.TabIndex = 7;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Black;
            panel3.Dock = DockStyle.Right;
            panel3.Location = new Point(2598, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(282, 1761);
            panel3.TabIndex = 8;
            // 
            // Exit
            // 
            Exit.Location = new Point(2180, 17);
            Exit.Margin = new Padding(20);
            Exit.Name = "Exit";
            Exit.Size = new Size(153, 62);
            Exit.TabIndex = 7;
            Exit.Text = "Sluiten";
            Exit.UseVisualStyleBackColor = true;
            Exit.Click += Exit_Click;
            // 
            // panel4
            // 
            panel4.Controls.Add(BottomDaysPanel);
            panel4.Controls.Add(panel1);
            panel4.Controls.Add(TopDaysPanel);
            panel4.Controls.Add(MenuPanel);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(225, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(2373, 1761);
            panel4.TabIndex = 9;
            // 
            // BottomDaysPanel
            // 
            BottomDaysPanel.Controls.Add(Sunday);
            BottomDaysPanel.Controls.Add(Saturday);
            BottomDaysPanel.Controls.Add(Friday);
            BottomDaysPanel.Dock = DockStyle.Fill;
            BottomDaysPanel.Location = new Point(0, 935);
            BottomDaysPanel.Name = "BottomDaysPanel";
            BottomDaysPanel.Padding = new Padding(20, 0, 20, 20);
            BottomDaysPanel.Size = new Size(2373, 734);
            BottomDaysPanel.TabIndex = 14;
            // 
            // Sunday
            // 
            Sunday.Controls.Add(SundayRecipePicker);
            Sunday.Dock = DockStyle.Left;
            Sunday.Location = new Point(1171, 0);
            Sunday.Name = "Sunday";
            Sunday.Size = new Size(599, 714);
            Sunday.TabIndex = 2;
            // 
            // SundayRecipePicker
            // 
            SundayRecipePicker.Dock = DockStyle.Fill;
            SundayRecipePicker.Location = new Point(0, 0);
            SundayRecipePicker.Name = "SundayRecipePicker";
            SundayRecipePicker.Size = new Size(599, 714);
            SundayRecipePicker.TabIndex = 0;
            // 
            // Saturday
            // 
            Saturday.Controls.Add(SaturdayRecipePicker);
            Saturday.Dock = DockStyle.Left;
            Saturday.Location = new Point(585, 0);
            Saturday.Name = "Saturday";
            Saturday.Size = new Size(586, 714);
            Saturday.TabIndex = 1;
            // 
            // SaturdayRecipePicker
            // 
            SaturdayRecipePicker.Dock = DockStyle.Fill;
            SaturdayRecipePicker.Location = new Point(0, 0);
            SaturdayRecipePicker.Name = "SaturdayRecipePicker";
            SaturdayRecipePicker.Size = new Size(586, 714);
            SaturdayRecipePicker.TabIndex = 0;
            // 
            // Friday
            // 
            Friday.Controls.Add(FridayRecipePicker);
            Friday.Dock = DockStyle.Left;
            Friday.Location = new Point(20, 0);
            Friday.Name = "Friday";
            Friday.Size = new Size(565, 714);
            Friday.TabIndex = 0;
            // 
            // FridayRecipePicker
            // 
            FridayRecipePicker.Dock = DockStyle.Fill;
            FridayRecipePicker.Location = new Point(0, 0);
            FridayRecipePicker.Name = "FridayRecipePicker";
            FridayRecipePicker.Size = new Size(565, 714);
            FridayRecipePicker.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 1669);
            panel1.Name = "panel1";
            panel1.Size = new Size(2373, 92);
            panel1.TabIndex = 13;
            // 
            // TopDaysPanel
            // 
            TopDaysPanel.Controls.Add(Thursday);
            TopDaysPanel.Controls.Add(Wednesday);
            TopDaysPanel.Controls.Add(Tuesday);
            TopDaysPanel.Controls.Add(Monday);
            TopDaysPanel.Dock = DockStyle.Top;
            TopDaysPanel.Location = new Point(0, 102);
            TopDaysPanel.Name = "TopDaysPanel";
            TopDaysPanel.Padding = new Padding(20, 0, 20, 0);
            TopDaysPanel.Size = new Size(2373, 833);
            TopDaysPanel.TabIndex = 11;
            // 
            // Thursday
            // 
            Thursday.Controls.Add(ThursdayRecipePicker);
            Thursday.Dock = DockStyle.Fill;
            Thursday.Location = new Point(1770, 0);
            Thursday.Name = "Thursday";
            Thursday.Size = new Size(583, 833);
            Thursday.TabIndex = 12;
            // 
            // ThursdayRecipePicker
            // 
            ThursdayRecipePicker.Dock = DockStyle.Fill;
            ThursdayRecipePicker.Location = new Point(0, 0);
            ThursdayRecipePicker.Name = "ThursdayRecipePicker";
            ThursdayRecipePicker.Size = new Size(583, 833);
            ThursdayRecipePicker.TabIndex = 0;
            // 
            // Wednesday
            // 
            Wednesday.Controls.Add(WednesdayRecipePicker);
            Wednesday.Dock = DockStyle.Left;
            Wednesday.Location = new Point(1171, 0);
            Wednesday.Name = "Wednesday";
            Wednesday.Size = new Size(599, 833);
            Wednesday.TabIndex = 11;
            // 
            // WednesdayRecipePicker
            // 
            WednesdayRecipePicker.Dock = DockStyle.Fill;
            WednesdayRecipePicker.Location = new Point(0, 0);
            WednesdayRecipePicker.Name = "WednesdayRecipePicker";
            WednesdayRecipePicker.Size = new Size(599, 833);
            WednesdayRecipePicker.TabIndex = 0;
            // 
            // Tuesday
            // 
            Tuesday.Controls.Add(TuesdayRecipePicker);
            Tuesday.Dock = DockStyle.Left;
            Tuesday.Location = new Point(585, 0);
            Tuesday.Name = "Tuesday";
            Tuesday.Size = new Size(586, 833);
            Tuesday.TabIndex = 10;
            // 
            // TuesdayRecipePicker
            // 
            TuesdayRecipePicker.Dock = DockStyle.Fill;
            TuesdayRecipePicker.Location = new Point(0, 0);
            TuesdayRecipePicker.Name = "TuesdayRecipePicker";
            TuesdayRecipePicker.Size = new Size(586, 833);
            TuesdayRecipePicker.TabIndex = 0;
            // 
            // Monday
            // 
            Monday.Controls.Add(MondayRecipePicker);
            Monday.Dock = DockStyle.Left;
            Monday.Location = new Point(20, 0);
            Monday.Name = "Monday";
            Monday.Size = new Size(565, 833);
            Monday.TabIndex = 9;
            // 
            // MondayRecipePicker
            // 
            MondayRecipePicker.Dock = DockStyle.Fill;
            MondayRecipePicker.Location = new Point(0, 0);
            MondayRecipePicker.Name = "MondayRecipePicker";
            MondayRecipePicker.Size = new Size(565, 833);
            MondayRecipePicker.TabIndex = 0;
            // 
            // MenuPanel
            // 
            MenuPanel.BackColor = Color.Black;
            MenuPanel.Controls.Add(StartDatePicker);
            MenuPanel.Controls.Add(ShoppingList);
            MenuPanel.Controls.Add(Exit);
            MenuPanel.Controls.Add(RecipesButton);
            MenuPanel.Controls.Add(IngredientsButton);
            MenuPanel.Dock = DockStyle.Top;
            MenuPanel.Location = new Point(0, 0);
            MenuPanel.Name = "MenuPanel";
            MenuPanel.Size = new Size(2373, 102);
            MenuPanel.TabIndex = 10;
            // 
            // StartDatePicker
            // 
            StartDatePicker.Location = new Point(34, 27);
            StartDatePicker.Name = "StartDatePicker";
            StartDatePicker.Size = new Size(458, 39);
            StartDatePicker.TabIndex = 10;
            StartDatePicker.ValueChanged += StartDatePicker_ValueChangedAsync;
            // 
            // ShoppingList
            // 
            ShoppingList.Location = new Point(1015, 19);
            ShoppingList.Name = "ShoppingList";
            ShoppingList.Size = new Size(270, 62);
            ShoppingList.TabIndex = 9;
            ShoppingList.Text = "Boodschappenlijstje";
            ShoppingList.UseVisualStyleBackColor = true;
            ShoppingList.Click += ShoppingList_ClickAsync;
            // 
            // RecipesButton
            // 
            RecipesButton.Location = new Point(1605, 19);
            RecipesButton.Name = "RecipesButton";
            RecipesButton.Size = new Size(248, 62);
            RecipesButton.TabIndex = 5;
            RecipesButton.Text = "Recepten beheren";
            RecipesButton.UseVisualStyleBackColor = true;
            RecipesButton.Click += RecipesButton_ClickAsync;
            // 
            // IngredientsButton
            // 
            IngredientsButton.Location = new Point(1318, 19);
            IngredientsButton.Name = "IngredientsButton";
            IngredientsButton.Size = new Size(281, 62);
            IngredientsButton.TabIndex = 4;
            IngredientsButton.Text = "Ingredienten beheren";
            IngredientsButton.UseVisualStyleBackColor = true;
            IngredientsButton.Click += IngredientsButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2880, 1761);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "My Recipe Planner";
            Load += MainForm_LoadAsync;
            panel4.ResumeLayout(false);
            BottomDaysPanel.ResumeLayout(false);
            Sunday.ResumeLayout(false);
            Saturday.ResumeLayout(false);
            Friday.ResumeLayout(false);
            TopDaysPanel.ResumeLayout(false);
            Thursday.ResumeLayout(false);
            Wednesday.ResumeLayout(false);
            Tuesday.ResumeLayout(false);
            Monday.ResumeLayout(false);
            MenuPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel TopDaysPanel;
        private Panel Thursday;
        private UI.Controls.RecipePickerDayControl ThursdayRecipePicker;
        private Panel Wednesday;
        private UI.Controls.RecipePickerDayControl WednesdayRecipePicker;
        private Panel Tuesday;
        private UI.Controls.RecipePickerDayControl TuesdayRecipePicker;
        private Panel Monday;
        private UI.Controls.RecipePickerDayControl MondayRecipePicker;
        private Panel MenuPanel;
        private Button Exit;
        private Button RecipesButton;
        private Button IngredientsButton;
        private Panel panel1;
        private Panel BottomDaysPanel;
        private Panel Sunday;
        private UI.Controls.RecipePickerDayControl SundayRecipePicker;
        private Panel Saturday;
        private UI.Controls.RecipePickerDayControl SaturdayRecipePicker;
        private Panel Friday;
        private UI.Controls.RecipePickerDayControl FridayRecipePicker;
        private Button ShoppingList;
        private DateTimePicker StartDatePicker;
    }
}
