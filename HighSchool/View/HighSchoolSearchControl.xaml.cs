using System.Windows.Controls;
using Common.Entry;
using HighSchool.ViewModel;

namespace HighSchool.View
{
    /// <summary>
    /// Логика взаимодействия для HighSchoolSearchControl.xaml
    /// </summary>
    public partial class HighSchoolSearchControl : UserControl
    {
        #region Constructors

        public HighSchoolSearchControl(object viewModel)
        {
            InitializeComponent();
            InitializeDataContext(viewModel);
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
