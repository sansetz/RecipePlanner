using Microsoft.Extensions.DependencyInjection;
using RecipePlanner.App;
using RecipePlanner.Contracts.WeekSchedule;

namespace RecipePlanner.UI {
    public partial class WeekScheduleForm : Form {

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly WeekScheduleService _weekScheduleService;
        private int _weekplanId;
        private bool _isExporting;

        public WeekScheduleForm(
            IServiceScopeFactory scopeFactory,
            WeekScheduleService weekScheduleService
        ) {
            InitializeComponent();
            _scopeFactory = scopeFactory;
            _weekScheduleService = weekScheduleService;
        }

        public async Task ShowDialogAsync(int weekplanId, IWin32Window? owner = null) {
            _weekplanId = weekplanId;

            await LoadWeekScheduleAsync();

            base.ShowDialog(owner);
        }

        private async Task LoadWeekScheduleAsync() {
            var items = await _weekScheduleService
                .GetWeekScheduleItemsAsync(_weekplanId);

            WeekSchedule.Clear();

            foreach (var item in items) {
                WeekSchedule.AppendText(
                    FormatWeekscheduleItem(item) + Environment.NewLine
                );
            }
        }

        private static string FormatWeekscheduleItem(WeekScheduleItem item) {
            return $"{WeekDayHelpers.GetDayName(item.Date.DayOfWeek)}: {item.RecipeName} - {item.Info}";
        }

        private async void Export_ClickAsync(object sender, EventArgs e) {
            if (_isExporting) return;
            _isExporting = true;

            var filename = GetExportLocation();
            if (string.IsNullOrWhiteSpace(filename)) {
                MessageBox.Show("No valid file location selected");
                return;
            }

            try {
                await _weekScheduleService.ExportWeekScheduleCsvAsync(_weekplanId, filename);
                MessageBox.Show("Week schedule exported successfully");
            }
            catch (Exception ex) {
                MessageBox.Show("Error exporting week schedule: " + ex.Message);
            }
            finally {
                _isExporting = false;
            }
        }

        private string GetExportLocation() {
            using var dlg = new SaveFileDialog {
                FileName = "weekschedule",
                DefaultExt = ".csv",
                Filter = "CSV file (*.csv)|*.csv|All Files (*.*)|*.*",
                AddExtension = true,
                OverwritePrompt = true
            };

            return dlg.ShowDialog() == DialogResult.OK
                ? dlg.FileName
                : string.Empty;

        }
    }
}
