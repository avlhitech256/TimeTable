using System;
using System.Collections.Generic;
using Common.Data.Enum;
using Common.DomainContext;
using HighSchool.ViewModel;

namespace TimeTable.ViewModel.MainWindow
{
    public class ViewModelFactory
    {
        #region Members

        private readonly Dictionary<MenuItemName, Func<IDomainContext, object>> mapCreators;
        private IDomainContext context;

        #endregion

        #region Constructors

        public ViewModelFactory(IDomainContext context)
        {
            mapCreators = 
                new Dictionary<MenuItemName, Func<IDomainContext, object>>
                {
                    { MenuItemName.HighSchool, (x) => new HighSchoolViewModel(x)},
                };
            this.context = context;
        }

        #endregion

        #region Methods

        public object Create(MenuItemName menuItemName)
        {
            object result = null;

            if (mapCreators.ContainsKey(menuItemName))
            {
                Func<IDomainContext, object> creator = mapCreators[menuItemName];
                result = creator(context);
            }

            return result;
        }

        #endregion
    }
}
