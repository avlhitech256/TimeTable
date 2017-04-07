using System;
using Domain.Data.Enum;

namespace Domain.Event
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
