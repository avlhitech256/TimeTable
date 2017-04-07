using System;
using System.Collections.Generic;
using Domain.Data.Enum;
using HighSchool.View;

namespace TimeTable.ViewModel.MainWindow
{
    public class ViewFactory
    {
        #region Members

        private readonly Dictionary<MenuItemName, Func<MenuItemName, object>> mapFactories;

        #endregion

        #region Constructors

        public ViewFactory(ViewModelRouter viewModelRouter)
        {
            mapFactories =
                new Dictionary<MenuItemName, Func<MenuItemName, object>>
                {
                    { MenuItemName.HighSchool,
                      (menuItemName) => new HighSchoolSearchControl(viewModelRouter.GetViewModel(menuItemName))}
                };
        }

        #endregion

        #region Methods

        public object GetView(MenuItemName menuItemName)
        {
            object view = null;

            if (mapFactories != null && mapFactories.ContainsKey(menuItemName))
            {
                Func<MenuItemName, object> creator = mapFactories[menuItemName];
                view = creator(menuItemName);
            }

            return view;
        }

        #endregion

    }
}
