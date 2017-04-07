using System.Windows.Controls;
using HighSchool.ViewModel;

namespace HighSchool.View
{
    /// <summary>
    /// Логика взаимодействия для HighSchoolSearchControl.xaml
    /// </summary>
    public partial class HighSchoolSearchControl : UserControl
    {
        #region Members

        private HighSchoolViewModel viewModel;

        #endregion

        #region Constructors

        public HighSchoolSearchControl()
        {
            InitializeComponent();
            viewModel = null;
        }

        #endregion

        #region Properties

        public HighSchoolViewModel ViewModel { get; private set; }

        #endregion

        #region Methods

        private void InitializeDataContext(object viewModel)
        {
            ViewModel = viewModel as HighSchoolViewModel;
            DataContext = ViewModel;
        }

        #endregion

    }

}
