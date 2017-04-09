using System;
using System.Windows.Input;

namespace HighSchool.ViewModel.Command
{
    internal class SearchCommand : CommonCommand, ICommand
    {
        #region Members

        private bool canExecute;

        #endregion

        #region Constructors

        public SearchCommand(IHighSchoolViewModel viewModel) : base(viewModel)
        {
            ViewModel.SearchCriteria.SearchCriteriaChanged += ChangeCanExecute;
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
            ViewModel.ApplySearchCriteria();
            CanExecuteProperty = false;
        }

        #endregion
    }
}
