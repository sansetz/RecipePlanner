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
            WeekSchedule = new Button();
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
            panel2.Margin = new Padding(2, 1, 2, 1);
            panel2.Name = "panel2";
            panel2.Size = new Size(10, 679);
            panel2.TabIndex = 7;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Black;
            panel3.Dock = DockStyle.Right;
            panel3.Location = new Point(1541, 0);
            panel3.Margin = new Padding(2, 1, 2, 1);
            panel3.Name = "panel3";
            panel3.Size = new Size(10, 679);
            panel3.TabIndex = 8;
            // 
            // panel4
            // 
            panel4.Controls.Add(BottomDaysPanel);
            panel4.Controls.Add(panel1);
            panel4.Controls.Add(TopDaysPanel);
            panel4.Controls.Add(MenuPanel);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(10, 0);
            panel4.Margin = new Padding(2, 1, 2, 1);
            panel4.Name = "panel4";
            panel4.Size = new Size(1531, 679);
            panel4.TabIndex = 9;
            // 
            // BottomDaysPanel
            // 
            BottomDaysPanel.Controls.Add(Sunday);
            BottomDaysPanel.Controls.Add(Saturday);
            BottomDaysPanel.Controls.Add(Friday);
            BottomDaysPanel.Dock = DockStyle.Fill;
            BottomDaysPanel.Location = new Point(0, 438);
            BottomDaysPanel.Margin = new Padding(2, 1, 2, 1);
            BottomDaysPanel.Name = "BottomDaysPanel";
            BottomDaysPanel.Padding = new Padding(11, 0, 11, 9);
            BottomDaysPanel.Size = new Size(1531, 231);
            BottomDaysPanel.TabIndex = 14;
            // 
            // Sunday
            // 
            Sunday.Controls.Add(SundayRecipePicker);
            Sunday.Dock = DockStyle.Left;
            Sunday.Location = new Point(657, 0);
            Sunday.Margin = new Padding(2, 1, 2, 1);
            Sunday.Name = "Sunday";
            Sunday.Size = new Size(323, 222);
            Sunday.TabIndex = 2;
            // 
            // SundayRecipePicker
            // 
            SundayRecipePicker.Dock = DockStyle.Fill;
            SundayRecipePicker.Location = new Point(0, 0);
            SundayRecipePicker.Margin = new Padding(1, 0, 1, 0);
            SundayRecipePicker.Name = "SundayRecipePicker";
            SundayRecipePicker.Size = new Size(323, 222);
            SundayRecipePicker.TabIndex = 0;
            // 
            // Saturday
            // 
            Saturday.Controls.Add(SaturdayRecipePicker);
            Saturday.Dock = DockStyle.Left;
            Saturday.Location = new Point(334, 0);
            Saturday.Margin = new Padding(2, 1, 2, 1);
            Saturday.Name = "Saturday";
            Saturday.Size = new Size(323, 222);
            Saturday.TabIndex = 1;
            // 
            // SaturdayRecipePicker
            // 
            SaturdayRecipePicker.Dock = DockStyle.Fill;
            SaturdayRecipePicker.Location = new Point(0, 0);
            SaturdayRecipePicker.Margin = new Padding(1, 0, 1, 0);
            SaturdayRecipePicker.Name = "SaturdayRecipePicker";
            SaturdayRecipePicker.Size = new Size(323, 222);
            SaturdayRecipePicker.TabIndex = 0;
            // 
            // Friday
            // 
            Friday.Controls.Add(FridayRecipePicker);
            Friday.Dock = DockStyle.Left;
            Friday.Location = new Point(11, 0);
            Friday.Margin = new Padding(2, 1, 2, 1);
            Friday.Name = "Friday";
            Friday.Size = new Size(323, 222);
            Friday.TabIndex = 0;
            // 
            // FridayRecipePicker
            // 
            FridayRecipePicker.Dock = DockStyle.Fill;
            FridayRecipePicker.Location = new Point(0, 0);
            FridayRecipePicker.Margin = new Padding(1, 0, 1, 0);
            FridayRecipePicker.Name = "FridayRecipePicker";
            FridayRecipePicker.Size = new Size(323, 222);
            FridayRecipePicker.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 669);
            panel1.Margin = new Padding(2, 1, 2, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(1531, 10);
            panel1.TabIndex = 13;
            // 
            // TopDaysPanel
            // 
            TopDaysPanel.Controls.Add(Thursday);
            TopDaysPanel.Controls.Add(Wednesday);
            TopDaysPanel.Controls.Add(Tuesday);
            TopDaysPanel.Controls.Add(Monday);
            TopDaysPanel.Dock = DockStyle.Top;
            TopDaysPanel.Location = new Point(0, 48);
            TopDaysPanel.Margin = new Padding(2, 1, 2, 1);
            TopDaysPanel.Name = "TopDaysPanel";
            TopDaysPanel.Padding = new Padding(11, 0, 11, 0);
            TopDaysPanel.Size = new Size(1531, 390);
            TopDaysPanel.TabIndex = 11;
            // 
            // Thursday
            // 
            Thursday.Controls.Add(ThursdayRecipePicker);
            Thursday.Dock = DockStyle.Fill;
            Thursday.Location = new Point(980, 0);
            Thursday.Margin = new Padding(2, 1, 2, 1);
            Thursday.Name = "Thursday";
            Thursday.Size = new Size(540, 390);
            Thursday.TabIndex = 12;
            // 
            // ThursdayRecipePicker
            // 
            ThursdayRecipePicker.Dock = DockStyle.Fill;
            ThursdayRecipePicker.Location = new Point(0, 0);
            ThursdayRecipePicker.Margin = new Padding(1, 0, 1, 0);
            ThursdayRecipePicker.Name = "ThursdayRecipePicker";
            ThursdayRecipePicker.Size = new Size(540, 390);
            ThursdayRecipePicker.TabIndex = 0;
            // 
            // Wednesday
            // 
            Wednesday.Controls.Add(WednesdayRecipePicker);
            Wednesday.Dock = DockStyle.Left;
            Wednesday.Location = new Point(657, 0);
            Wednesday.Margin = new Padding(2, 1, 2, 1);
            Wednesday.Name = "Wednesday";
            Wednesday.Size = new Size(323, 390);
            Wednesday.TabIndex = 11;
            // 
            // WednesdayRecipePicker
            // 
            WednesdayRecipePicker.Dock = DockStyle.Fill;
            WednesdayRecipePicker.Location = new Point(0, 0);
            WednesdayRecipePicker.Margin = new Padding(1, 0, 1, 0);
            WednesdayRecipePicker.Name = "WednesdayRecipePicker";
            WednesdayRecipePicker.Size = new Size(323, 390);
            WednesdayRecipePicker.TabIndex = 0;
            // 
            // Tuesday
            // 
            Tuesday.Controls.Add(TuesdayRecipePicker);
            Tuesday.Dock = DockStyle.Left;
            Tuesday.Location = new Point(334, 0);
            Tuesday.Margin = new Padding(2, 1, 2, 1);
            Tuesday.Name = "Tuesday";
            Tuesday.Size = new Size(323, 390);
            Tuesday.TabIndex = 10;
            // 
            // TuesdayRecipePicker
            // 
            TuesdayRecipePicker.Dock = DockStyle.Fill;
            TuesdayRecipePicker.Location = new Point(0, 0);
            TuesdayRecipePicker.Margin = new Padding(1, 0, 1, 0);
            TuesdayRecipePicker.Name = "TuesdayRecipePicker";
            TuesdayRecipePicker.Size = new Size(323, 390);
            TuesdayRecipePicker.TabIndex = 0;
            // 
            // Monday
            // 
            Monday.Controls.Add(MondayRecipePicker);
            Monday.Dock = DockStyle.Left;
            Monday.Location = new Point(11, 0);
            Monday.Margin = new Padding(2, 1, 2, 1);
            Monday.Name = "Monday";
            Monday.Size = new Size(323, 390);
            Monday.TabIndex = 9;
            // 
            // MondayRecipePicker
            // 
            MondayRecipePicker.Dock = DockStyle.Fill;
            MondayRecipePicker.Location = new Point(0, 0);
            MondayRecipePicker.Margin = new Padding(1, 0, 1, 0);
            MondayRecipePicker.Name = "MondayRecipePicker";
            MondayRecipePicker.Size = new Size(323, 390);
            MondayRecipePicker.TabIndex = 0;
            // 
            // MenuPanel
            // 
            MenuPanel.BackColor = Color.Black;
            MenuPanel.Controls.Add(WeekSchedule);
            MenuPanel.Controls.Add(StartDatePicker);
            MenuPanel.Controls.Add(ShoppingList);
            MenuPanel.Controls.Add(RecipesButton);
            MenuPanel.Controls.Add(IngredientsButton);
            MenuPanel.Dock = DockStyle.Top;
            MenuPanel.Location = new Point(0, 0);
            MenuPanel.Margin = new Padding(2, 1, 2, 1);
            MenuPanel.Name = "MenuPanel";
            MenuPanel.Size = new Size(1531, 48);
            MenuPanel.TabIndex = 10;
            // 
            // WeekSchedule
            // 
            WeekSchedule.Location = new Point(620, 9);
            WeekSchedule.Name = "WeekSchedule";
            WeekSchedule.Size = new Size(104, 29);
            WeekSchedule.TabIndex = 11;
            WeekSchedule.Text = "Weekoverzicht";
            WeekSchedule.UseVisualStyleBackColor = true;
            WeekSchedule.Click += WeekSchedule_ClickAsync;
            // 
            // StartDatePicker
            // 
            StartDatePicker.CustomFormat = "dddd dd-MM-yyyy";
            StartDatePicker.Format = DateTimePickerFormat.Custom;
            StartDatePicker.Location = new Point(15, 12);
            StartDatePicker.Margin = new Padding(2, 1, 2, 1);
            StartDatePicker.Name = "StartDatePicker";
            StartDatePicker.Size = new Size(199, 23);
            StartDatePicker.TabIndex = 10;
            StartDatePicker.ValueChanged += StartDatePicker_ValueChangedAsync;
            // 
            // ShoppingList
            // 
            ShoppingList.Location = new Point(470, 9);
            ShoppingList.Margin = new Padding(2, 1, 2, 1);
            ShoppingList.Name = "ShoppingList";
            ShoppingList.Size = new Size(145, 29);
            ShoppingList.TabIndex = 9;
            ShoppingList.Text = "Boodschappenlijstje";
            ShoppingList.UseVisualStyleBackColor = true;
            ShoppingList.Click += ShoppingList_ClickAsync;
            // 
            // RecipesButton
            // 
            RecipesButton.Location = new Point(980, 9);
            RecipesButton.Margin = new Padding(2, 1, 2, 1);
            RecipesButton.Name = "RecipesButton";
            RecipesButton.Size = new Size(134, 29);
            RecipesButton.TabIndex = 5;
            RecipesButton.Text = "Recepten beheren";
            RecipesButton.UseVisualStyleBackColor = true;
            RecipesButton.Click += RecipesButton_ClickAsync;
            // 
            // IngredientsButton
            // 
            IngredientsButton.Location = new Point(825, 9);
            IngredientsButton.Margin = new Padding(2, 1, 2, 1);
            IngredientsButton.Name = "IngredientsButton";
            IngredientsButton.Size = new Size(151, 29);
            IngredientsButton.TabIndex = 4;
            IngredientsButton.Text = "Ingredienten beheren";
            IngredientsButton.UseVisualStyleBackColor = true;
            IngredientsButton.Click += IngredientsButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1551, 679);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(2, 1, 2, 1);
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
        private Button WeekSchedule;
    }
}
