﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using CommonControl.EditControl;
using CommonControl.SearchControl;
using HighSchool.View;
using Domain.Data.Enum;
using Domain.DomainContext;
using Domain.ViewModel;
using Faculty.View;

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
                    {MenuItemName.HighSchool, () => new HighSchoolSearchControl()},
                    {MenuItemName.Faculty, () => new FacultySearchControl()}
                };

            mapEditControlFactories =
                new Dictionary<MenuItemName, Func<object>>
                {
                    {MenuItemName.HighSchool, () => new HighSchoolEditControl()},
                    {MenuItemName.Faculty, () => new FacultyEditControl()}
                };

        }

        #endregion

        #region Methods

        public object GetView(MenuItemName menuItemName)
        {
            object view = null;
            object viewModel = viewModelRouter.GetViewModel(menuItemName);
            IControlViewModel viewModelWithInterface = viewModel as IControlViewModel;
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

                    if (viewModelWithInterface.IsEditControl)
                    {
                        EditControl editView = view as EditControl;

                        if (editView != null)
                        {
                            editView.DomainContext = domainContext;
                        }

                    }
                    else
                    {
                        SearchControl searchView = view as SearchControl;

                        if (searchView != null)
                        {
                            searchView.DomainContext = domainContext;
                        }

                    }

                }

            }

            return view;
        }

        #endregion

    }

}
