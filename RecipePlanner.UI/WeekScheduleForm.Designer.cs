namespace RecipePlanner.UI {
    partial class WeekScheduleForm {
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
            WeekSchedule = new RichTextBox();
            Export = new Button();
            SuspendLayout();
            // 
            // WeekSchedule
            // 
            WeekSchedule.Location = new Point(12, 12);
            WeekSchedule.Name = "WeekSchedule";
            WeekSchedule.Size = new Size(583, 286);
            WeekSchedule.TabIndex = 0;
            WeekSchedule.Text = "";
            // 
            // Export
            // 
            Export.Location = new Point(482, 303);
            Export.Name = "Export";
            Export.Size = new Size(112, 31);
            Export.TabIndex = 1;
            Export.Text = "Export";
            Export.UseVisualStyleBackColor = true;
            Export.Click += Export_ClickAsync;
            // 
            // WeekScheduleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(607, 342);
            Controls.Add(Export);
            Controls.Add(WeekSchedule);
            Name = "WeekScheduleForm";
            Text = "WeekScheduleForm";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox WeekSchedule;
        private Button Export;
    }
}