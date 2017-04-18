using System;
using System.Windows.Input;

namespace Domain.ViewModel.Command
{
    internal class SearchCommand<T> : CommonCommand<T>, ICommand where T : class 
    {
        #region Constructors

        public SearchCommand(IDataViewModel<T> viewModel) : base(viewModel)
        {
            if (ViewModel?.SearchCriteria != null)
            {
                ViewModel.SearchCriteria.SearchCriteriaChanged += ChangeCanExecute;
            }

            CanExecuteProperty = true;
        }

        #endregion

        #region Methods

        private void ChangeCanExecute(object sender, EventArgs args)
        {
            CanExecuteProperty = true;
        }

        public override void Execute(object parameter)
        {
            ViewModel?.ApplySearchCriteria();
            CanExecuteProperty = false;
        }

        #endregion
    }
}
