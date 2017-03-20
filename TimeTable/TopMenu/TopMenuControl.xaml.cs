using System.Windows;
using System.Windows.Controls;

namespace TimeTable.TopMenu
{
    /// <summary>
    /// Логика взаимодействия для TopMenuControl.xaml
    /// </summary>
    public partial class TopMenuControl : UserControl
    {
        public TopMenuControl()
        {
            InitializeComponent();
        }

        private void MenuItem_Click_Close(object sender, RoutedEventArgs e)
        {
            DockPanel parentDockPanel = Parent as DockPanel;

            if (parentDockPanel != null)
            {
                Window mainWindow = parentDockPanel.Parent as Window;
                mainWindow?.Close();
            }

        }

    }

}
