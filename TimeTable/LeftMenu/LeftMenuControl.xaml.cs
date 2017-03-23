using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Common.DomainContext;
using Common.Entry;
using Common.Messenger;
using Common.Messenger.Impl;
using HighSchool.View;
using TimeTable.Annotations;

namespace TimeTable.LeftMenu
{
    /// <summary>
    /// Логика взаимодействия для LeftMenuControl.xaml
    /// </summary>
    public partial class LeftMenuControl : UserControl, INotifyPropertyChanged
    {
        #region Members

        private ContentControl entry;

        #endregion

        #region Constructors

        public LeftMenuControl()
        {
            InitializeComponent();
            InitLeftMenu();
            entry = EntryControl.Instance();
        }

        #endregion

        #region Properties

        public ContentControl Entry
        {
            get
            {
                return entry;
            }

            set
            {
                if (!Equals(entry, value))
                {
                    entry = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Methods

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
                //IMessenger messenger = DomainContext.Instance().Messenger;

                //if (messenger != null)
                //{
                    if (button.Equals(HighSchoolButton))
                    {
                        //messenger.Send(CommandName.SetEntryControl, (EntryControl) HighSchoolSearchControl.Instance());
                        //Entry = HighSchoolSearchControl.Instance();
                        ((MainWindow)((DockPanel)this.Parent).Parent).EntryControl.Content = HighSchoolSearchControl.Instance();
                }
                    else
                    {
                    //messenger.Send(CommandName.SetEntryControl, EntryControl.Instance());
                    //Entry = EntryControl.Instance();
                    ((MainWindow)((DockPanel)this.Parent).Parent).EntryControl.Content = EntryControl.Instance();
                }

                //}

            }

        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }

}
