using System.Windows.Controls;
using TimeTable.ViewModel.FooterStatusBar;

namespace TimeTable.View.FooterStatusBar
{
    /// <summary>
    /// Логика взаимодействия для FooterStatusBarControl.xaml
    /// </summary>
    public partial class FooterStatusBarControl : UserControl
    {
        #region Members

        private FooterStatusBarViewModel viewModel;

        #endregion

        #region Constructors

        public FooterStatusBarControl()
        {
            InitializeComponent();
            InitializeDataContext();
        }

        #endregion

        #region Methods

        private void InitializeDataContext()
        {
            viewModel = new FooterStatusBarViewModel();
            DataContext = viewModel;
        }

        #endregion

    }

}
