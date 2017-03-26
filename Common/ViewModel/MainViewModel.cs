using Common.Data.Notifier;

namespace Common.ViewModel
{
    public class MainViewModel : Notifier
    {
        #region Members

        private MenuItemsStyle menuItemsStyle;

        #endregion

        #region Constructors

        public MainViewModel()
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
