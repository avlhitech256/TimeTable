using Domain.Data.Notifier;

namespace TimeTable.ViewModel.LeftMenu
{
    public class LeftMenuViewModel : Notifier
    {
        #region Members

        private MenuItemsStyle menuItemsStyle;

        #endregion

        #region Constructors

        public LeftMenuViewModel()
        {
            menuItemsStyle = new MenuItemsStyle("#FF808080", "#FF646464", "#FF4747B8", "#FF6767D8");
        }

        #endregion

        #region Properties

        public MenuItemsStyle MenuItemsStyle
        {
            get
            {
                return menuItemsStyle;
            }

            set
            {
                if (!menuItemsStyle.Equals(value))
                {
                    menuItemsStyle = value;
                    OnPropertyChanged();
                }

            }

        }

        #endregion

    }
}
