using Microsoft.Extensions.DependencyInjection;
using RecipePlanner.App;
using RecipePlanner.Contracts.WeekSchedule;

namespace RecipePlanner.UI {
    public partial class WeekScheduleForm : Form {

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly WeekScheduleService _weekScheduleService;
        private int _weekplanId;


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
    }
}
