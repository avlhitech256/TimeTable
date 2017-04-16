using System;
using Common.Data.Enum;

namespace Common.Event
{
    public class MenuChangedEventArgs : EventArgs
    {
        #region Constructors

        public MenuChangedEventArgs(MenuItemName menuItemName)
        {
            MenuItemName = menuItemName;
        }

        #endregion

        #region Properties
        
        public MenuItemName MenuItemName { get; }

        #endregion

    }

}
