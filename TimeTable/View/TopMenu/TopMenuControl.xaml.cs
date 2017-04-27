using System.Windows;
using System.Windows.Controls;
using Domain.DomainContext;
using TimeTable.ViewModel.TopMenu;

namespace TimeTable.View.TopMenu
{
    /// <summary>
    /// Логика взаимодействия для TopMenuControl.xaml
    /// </summary>
    public partial class TopMenuControl : UserControl
    {
        #region Members

        private IDomainContext domainContext;

        #endregion

        #region Constructors

        public TopMenuControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        public IDomainContext DomainContext
        {
            get
            {
                return domainContext;
            }

            set
            {
                if (domainContext != value)
                {
                    domainContext = value;
                    DataContext = new TopMenuViewModel(domainContext);
                }

            }

        }

        #endregion

        #region Methods

        private void MenuItem_Click_Close(object sender, RoutedEventArgs e)
        {
            DockPanel parentDockPanel = Parent as DockPanel;

            if (parentDockPanel != null)
            {
                Window mainWindow = parentDockPanel.Parent as Window;
                mainWindow?.Close();
            }

        }

        #endregion

    }

}
