using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Border button = sender as Border;

            if (button != null)
            {
                DomainContext context = DataContext as DomainContext;

                if (context != null)
                {
                    if (button.Equals(HighSchoolButton))
                    {
                        context.MainViewModel.MenuItemsStyle.HighSchoolMenuItemStyle.Selected = true;
                    }
                    else if (button.Equals(FacultyButton))
                    {
                        context.MainViewModel.MenuItemsStyle.FacultyMenuItemStyle.Selected = true;
                    }
                    else if (button.Equals(ChairButton))
                    {
                        context.MainViewModel.MenuItemsStyle.ChairMenuItemStyle.Selected = true;
                    }
                    else if (button.Equals(SpecialtyButton))
                    {
                        context.MainViewModel.MenuItemsStyle.SpecialtyMenuItemStyle.Selected = true;
                    }
                    else if (button.Equals(SpecializationButton))
                    {
                        context.MainViewModel.MenuItemsStyle.SpecializationMenuItemStyle.Selected = true;
                    }

                }

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

        private void Border_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Border button = sender as Border;

            if (button != null)
            {
                DomainContext context = DataContext as DomainContext;

                if (context != null)
                {

                    if (button.Equals(HighSchoolButton))
                    {
                        context.MainViewModel.MenuItemsStyle.HighSchoolMenuItemStyle.IsMouseOver = true;
                    }
                    else if (button.Equals(FacultyButton))
                    {
                        context.MainViewModel.MenuItemsStyle.FacultyMenuItemStyle.IsMouseOver = true;
                    }
                    else if (button.Equals(ChairButton))
                    {
                        context.MainViewModel.MenuItemsStyle.ChairMenuItemStyle.IsMouseOver = true;
                    }
                    else if (button.Equals(SpecialtyButton))
                    {
                        context.MainViewModel.MenuItemsStyle.SpecialtyMenuItemStyle.IsMouseOver = true;
                    }
                    else if (button.Equals(SpecializationButton))
                    {
                        context.MainViewModel.MenuItemsStyle.SpecializationMenuItemStyle.IsMouseOver = true;
                    }

                }

            }

        }

        private void Border_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Border button = sender as Border;

            if (button != null)
            {
                DomainContext context = DataContext as DomainContext;

                if (context != null)
                {

                    if (button.Equals(HighSchoolButton))
                    {
                        context.MainViewModel.MenuItemsStyle.HighSchoolMenuItemStyle.IsMouseOver = false;
                    }
                    else if (button.Equals(FacultyButton))
                    {
                        context.MainViewModel.MenuItemsStyle.FacultyMenuItemStyle.IsMouseOver = false;
                    }
                    else if (button.Equals(ChairButton))
                    {
                        context.MainViewModel.MenuItemsStyle.ChairMenuItemStyle.IsMouseOver = false;
                    }
                    else if (button.Equals(SpecialtyButton))
                    {
                        context.MainViewModel.MenuItemsStyle.SpecialtyMenuItemStyle.IsMouseOver = false;
                    }
                    else if (button.Equals(SpecializationButton))
                    {
                        context.MainViewModel.MenuItemsStyle.SpecializationMenuItemStyle.IsMouseOver = false;
                    }

                }

            }

        }

    }

}
