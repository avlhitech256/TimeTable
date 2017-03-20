using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
