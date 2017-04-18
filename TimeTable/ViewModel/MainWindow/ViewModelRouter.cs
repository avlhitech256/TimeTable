using System.Collections.Generic;
using Domain.Data.Enum;
using Domain.DomainContext;

namespace TimeTable.ViewModel.MainWindow
{
    public class ViewModelRouter
    {
        #region Members

        private readonly Dictionary<MenuItemName, object> viewModelMap;
        private readonly IDomainContext context;

        #endregion

        #region Constructors

        public ViewModelRouter(IDomainContext context)
        {
            viewModelMap = new Dictionary<MenuItemName, object>();
            this.context = context;

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
                ViewModelFactory factory = new ViewModelFactory(context);
                viewModel = factory.Create(menuItemName);
                viewModelMap.Add(menuItemName, viewModel);
            }

            return viewModel;
        }

        #endregion
    }
}
