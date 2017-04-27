using Common.Data.Notifier;
using Common.Messenger;
using Common.Messenger.Impl;
using Domain.DomainContext;
using Domain.Event;

namespace TimeTable.ViewModel.LeftMenu
{
    public class LeftMenuViewModel : Notifier
    {
        #region Members

        private MenuItemsStyle menuItemsStyle;

        #endregion

        #region Constructors

        public LeftMenuViewModel(IDomainContext domainContext)
        {
            DomainContext = domainContext;
            menuItemsStyle = new MenuItemsStyle("#FF808080", "#FF646464", "#FF4747B8", "#FF6767D8");
            SubscribeMessenger();
        }

        #endregion

        #region Properties

        private IDomainContext DomainContext { get; }

        private IMessenger Messenger => DomainContext?.Messenger;
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

        #region Methods

        private void SubscribeMessenger()
        {
            Messenger?.Register<MenuChangedEventArgs>(CommandName.SelectLeftMenu, SelectLeftMenu, CanSelectLeftMenu);
        }

        private void SelectLeftMenu(MenuChangedEventArgs args)
        {
            MenuItemsStyle.SetMouseUpMenuItems(args);
        }

        private bool CanSelectLeftMenu(MenuChangedEventArgs args)
        {
            return args != null;
        }

        #endregion
    }
}
