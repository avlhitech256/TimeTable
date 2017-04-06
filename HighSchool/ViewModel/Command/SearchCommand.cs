using System;
using System.Windows.Input;

namespace HighSchool.ViewModel.Command
{
    internal class SearchCommand : ICommand
    {
        #region Members

        private bool canExecute;

        #endregion

        #region Constructors

        public SearchCommand(IHighSchoolViewModel viewModel)
        {
            ViewModel = viewModel;
            ViewModel.SearchCriteria.SearchCriteriaChanged += ChangeCanExecute;
            CanExecuteProperty = true;
        }

        #endregion

        #region Properties

        private IHighSchoolViewModel ViewModel { get; }

        private bool CanExecuteProperty
        {
            get
            {
                return canExecute;
            }

            set
            {
                if (canExecute != value)
                {
                    canExecute = value;
                    OnCanExecuteChanged();
                }
            }

        }

        #endregion

        #region Methods

        private void ChangeCanExecute(object sender, EventArgs args)
        {
            CanExecuteProperty = true;
        }

        private void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }


        public bool CanExecute(object parameter)
        {
            return CanExecuteProperty;
        }

        public void Execute(object parameter)
        {
            ViewModel.ApplySearchCriteria();
            CanExecuteProperty = false;
        }

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged = delegate { };

        #endregion
    }
}
