using System.Windows.Controls;
using System.Windows.Input;
using Common.DomainContext;

namespace TimeTable.LeftMenu
{
    /// <summary>
    /// Логика взаимодействия для LeftMenuControl.xaml
    /// </summary>
    public partial class LeftMenuControl : UserControl
    {
        #region Constructors

        public LeftMenuControl()
        {
            InitializeComponent();
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

        #endregion

    }

}
