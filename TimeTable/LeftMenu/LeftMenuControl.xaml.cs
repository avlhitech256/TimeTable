using System.Windows.Controls;

namespace TimeTable.LeftMenu
{
    /// <summary>
    /// Логика взаимодействия для LeftMenuControl.xaml
    /// </summary>
    public partial class LeftMenuControl : UserControl
    {
        public LeftMenuControl()
        {
            InitializeComponent();
            InitLeftMenu();
        }

        private void InitLeftMenu()
        {
            Button1.Tag = "N";
            Button2.Tag = "N";
            Button3.Tag = "N";
        }

        private void Border_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            InitLeftMenu();

            Border button = sender as Border;

            if (button != null)
            {
                button.Tag = "Y";
            }

        }

    }

}
