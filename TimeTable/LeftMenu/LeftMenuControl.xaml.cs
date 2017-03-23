using System.Windows.Controls;
using Common.DomainContext;
using Common.Entry;
using Common.Messenger;
using Common.Messenger.Impl;
using HighSchool.View;

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
            HighSchoolButton.Tag = "N";
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
                IMessenger messenger = DomainContext.Instance().Messenger;

                if (messenger != null)
                {
                    if (button.Equals(HighSchoolButton))
                    {
                        messenger.Send(CommandName.SetEntryControl, (EntryControl) HighSchoolSearchControl.Instance());
                    }
                    else
                    {
                        messenger.Send(CommandName.SetEntryControl, EntryControl.Instance());
                    }

                }

            }

        }

    }

}
