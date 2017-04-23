using System;
using System.Collections.Generic;
using Domain.Data.Enum;
using Domain.DomainContext;
using Faculty.ViewModel;
using HighSchool.ViewModel;
using Specialty.ViewModel;

namespace TimeTable.ViewModel.MainWindow
{
    public class ViewModelFactory
    {
        #region Members

        private readonly Dictionary<MenuItemName, Func<IDomainContext, object>> mapCreators;
        private readonly IDomainContext context;

        #endregion

        #region Constructors

        public ViewModelFactory(IDomainContext context)
        {
            mapCreators = 
                new Dictionary<MenuItemName, Func<IDomainContext, object>>
                {
                    { MenuItemName.HighSchool, (x) => new HighSchoolViewModel(x)},
                    { MenuItemName.Faculty, (x) => new FacultyViewModel(x)},
                    {MenuItemName.Specialty, (x) => new SpecialtyViewModel(x) }
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
