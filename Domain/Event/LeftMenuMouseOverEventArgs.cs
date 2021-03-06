﻿using System;
using Domain.Data.Enum;

namespace Domain.Event
{
    public class LeftMenuMouseOverEventArgs : EventArgs
    {
        #region Constructors

        public LeftMenuMouseOverEventArgs(MenuItemName menuItemName, bool isMouseOver)
        {
            MenuItemName = menuItemName;
            IsMouseOver = isMouseOver;
        }

        #endregion

        #region Properties

        public MenuItemName MenuItemName { get; }
        public bool IsMouseOver { get; }

        #endregion

        #region Methods

        public bool Equals(LeftMenuMouseOverEventArgs other)
        {
            return MenuItemName == other.MenuItemName &&
                   IsMouseOver == other.IsMouseOver;
        }

        #endregion
    }

}
