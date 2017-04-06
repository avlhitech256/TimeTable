using System.Windows.Controls;
using Domain.DomainContext;
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
        private IDomainContext context;

        #endregion

        #region Constructors

        public FooterStatusBarControl()
        {
            InitializeComponent();
            InitializeDataContext();
        }

        #endregion

        #region Properties

        public IDomainContext DomainContext
        {
            get
            {
                return context;
            }

            set
            {
                if (context == null || context != value)
                {
                    context = value;
                    viewModel.DomainContext = value;
                }
            }

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
