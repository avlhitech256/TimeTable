using System;
using System.Collections.Generic;
using Common.Data.Enum;

namespace Domain.ViewModelRouter
{
    public class ViewModelRouter
    {
        #region Members

        private readonly Dictionary<MenuItemName, object> viewModelMap;

        #endregion

        #region Constructors

        public ViewModelRouter()
        {
            viewModelMap = new Dictionary<MenuItemName, object>();
        }

        #endregion

        #region Methods

        public object GetViewModel(MenuItemName menuItemName)
        {
            object viewModel;

            if (viewModelMap.ContainsKey(menuItemName))
            {
                viewModel = viewModelMap[menuItemName];
            }
            else
            {
                ViewModelFactory factory = new ViewModelFactory();
                viewModel = factory.Create(menuItemName);
                viewModelMap.Add(menuItemName, viewModel);
            }

            return viewModel;
        }

        #endregion
    }
}
