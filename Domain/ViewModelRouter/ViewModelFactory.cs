using System;
using System.Collections.Generic;
using Common.Data.Enum;
using HighSchool.View;
using HighSchool.ViewModel;

namespace Domain.ViewModelRouter
{
    public class ViewModelFactory
    {
        #region Members

        private readonly Dictionary<MenuItemName, Func<object>> mapCreators;

        #endregion
        
        #region Constructors

        public ViewModelFactory()
        {
            mapCreators = 
                new Dictionary<MenuItemName, Func<object>>
                {
                    { MenuItemName.HighSchool, () => new HighSchoolViewModel()},
                };
        }

        #endregion

        #region Methods

        public object Create(MenuItemName menuItemName)
        {
            object result = null;

            if (mapCreators.ContainsKey(menuItemName))
            {
                Func<object> creator = mapCreators[menuItemName];
                result = creator();
            }

            return result;
        }

        #endregion
    }
}
