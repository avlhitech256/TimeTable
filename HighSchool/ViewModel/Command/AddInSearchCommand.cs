using System.ComponentModel;
using System.Windows.Input;

namespace HighSchool.ViewModel.Command
{
    internal class AddInSearchCommand : CommonCommand, ICommand
    {
        #region Constructors

        public AddInSearchCommand(IHighSchoolViewModel viewModel) : base(viewModel)
        {
            if (ViewModel != null)
            {
                ViewModel.PropertyChanged += ChangeCanExecute;
                CanExecuteProperty = !ViewModel.IsEditControl;
            }

        }

        #endregion

        #region Methods

        private void ChangeCanExecute(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(ViewModel.IsEditControl))
            {
                CanExecuteProperty = !ViewModel.IsEditControl;
            }

        }

        public override void Execute(object parameter)
        {
            ViewModel?.AddInSearch();
        }

        #endregion
    }

}
