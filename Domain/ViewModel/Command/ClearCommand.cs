using System;
using System.Windows.Input;

namespace Domain.ViewModel.Command
{
    internal class ClearCommand<T> : CommonCommand<T>, ICommand where T : class
    {
        #region Constructors

        public ClearCommand(IDataViewModel<T> viewModel) : base(viewModel)
        {
            if (ViewModel?.SearchCriteria != null)
            {
                ViewModel.SearchCriteria.SearchCriteriaIsEmpty += ChangeCanExecute;
                CanExecuteProperty = !ViewModel?.SearchCriteria?.IsEmpty ?? false;
            }

        }

        #endregion

        #region Methods

        private void ChangeCanExecute(object sender, EventArgs args)
        {
            CanExecuteProperty = !ViewModel?.SearchCriteria?.IsEmpty ?? false;
        }

        public override void Execute(object parameter)
        {
            ViewModel?.Clear();
            CanExecuteProperty = false;
        }

        #endregion
    }
}
