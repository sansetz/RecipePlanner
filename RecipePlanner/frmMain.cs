using RecipePlanner.Data;

namespace RecipePlanner {
    public partial class frmMain : Form {
        private readonly IRecipePlannerDbContextFactory _dbFactory;

        public frmMain(IRecipePlannerDbContextFactory dbFactory) {
            InitializeComponent();
            _dbFactory = dbFactory;
        }

        private void frmMain_Load(object sender, EventArgs e) {

        }
    }
}
