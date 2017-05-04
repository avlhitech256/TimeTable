using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Chair.View;
using CommonControl.EditControl;
using CommonControl.SearchControl;
using HighSchool.View;
using Domain.Data.Enum;
using Domain.DomainContext;
using Domain.ViewModel;
using Faculty.View;
using Specialty.View;
using Specialization.View;

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
                    {MenuItemName.Faculty, () => new FacultySearchControl()},
                    {MenuItemName.Specialty, () => new SpecialtySearchControl()},
                    {MenuItemName.Chair, () => new ChairSearchControl()},
                    {MenuItemName.Specialization, () => new SpecializationSearchControl()}
                };

            mapEditControlFactories =
                new Dictionary<MenuItemName, Func<object>>
                {
                    {MenuItemName.HighSchool, () => new HighSchoolEditControl()},
                    {MenuItemName.Faculty, () => new FacultyEditControl()},
                    {MenuItemName.Specialty, () => new SpecialtyEditControl()},
                    {MenuItemName.Chair, () => new ChairEditControl()},
                     {MenuItemName.Specialization, () => new SpecializationEditControl()}
                };

        }

        #endregion

        #region Methods

        public object GetView(MenuItemName menuItemName, object oldView)
        {
            object view = null;
            object viewModel = viewModelRouter.GetViewModel(menuItemName);
            IControlViewModel viewModelWithInterface = viewModel as IControlViewModel;
            Func<object> factory = null;

            if (viewModelWithInterface != null)
            {
                if ((domainContext.ViewModel != viewModelWithInterface) ||
                    (domainContext.IsEditControl != viewModelWithInterface.IsEditControl))
                {
                    domainContext.ViewModel = viewModelWithInterface;
                    domainContext.IsEditControl = viewModelWithInterface.IsEditControl;

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
                else
                {
                    view = oldView;
                }

            }
            else
            {
                domainContext.ViewModel = null;
                domainContext.IsEditControl = false;
            }

            return view;
        }

        #endregion

    }

}
