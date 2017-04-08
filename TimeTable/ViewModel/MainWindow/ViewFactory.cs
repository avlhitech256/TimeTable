using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Domain.Data.Enum;
using Domain.DomainContext;
using Domain.Entry;
using HighSchool.View;

namespace TimeTable.ViewModel.MainWindow
{
    public class ViewFactory
    {
        #region Members

        private readonly Dictionary<MenuItemName, Func<object>> mapSearchControlFactories;
        private readonly Dictionary<MenuItemName, Func<object>> mapEditControlFactories;
        private readonly IDomainContext domainContext;
        private readonly ViewModelRouter viewModelRouter;

        #endregion

        #region Constructors

        public ViewFactory(IDomainContext domainContext, ViewModelRouter viewModelRouter)
        {
            this.domainContext = domainContext;
            this.viewModelRouter = viewModelRouter;

            mapSearchControlFactories =
                new Dictionary<MenuItemName, Func<object>>
                {
                    {MenuItemName.HighSchool, () => new HighSchoolSearchControl()}
                };

            mapEditControlFactories =
                new Dictionary<MenuItemName, Func<object>>
                {
                    {MenuItemName.HighSchool, () => new HighSchoolEditControl()}
                };

        }

        #endregion

        #region Methods

        public object GetView(MenuItemName menuItemName)
        {
            object view = null;
            object viewModel = viewModelRouter.GetViewModel(menuItemName);
            IViewModel viewModelWithInterface = viewModel as IViewModel;
            Func<object> factory = null;

            if (viewModelWithInterface != null)
            {
                domainContext.ViewModel = viewModelWithInterface;

                if (viewModelWithInterface.IsEditControl)
                {
                    if (mapEditControlFactories != null && mapEditControlFactories.ContainsKey(menuItemName))
                    {
                        factory = mapEditControlFactories[menuItemName];
                    }

                }
                else
                {
                    if (mapSearchControlFactories != null && mapSearchControlFactories.ContainsKey(menuItemName))
                    {
                        factory = mapSearchControlFactories[menuItemName];
                    }

                }

                if (factory != null)
                {
                    view = factory.Invoke();
                    UserControl viewWithInterface = view as UserControl;

                    if (viewWithInterface != null)
                    {
                        viewWithInterface.DataContext = viewModel;
                    }

                }

            }

            return view;
        }

        #endregion

    }

}
