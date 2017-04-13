using System.ComponentModel;
using System.Windows.Input;

namespace HighSchool.ViewModel.Command
{
    internal class DeleteCommand : CommonCommand, ICommand
    {
        #region Constructors

        public DeleteCommand(IHighSchoolViewModel viewModel) : base(viewModel)
        {
            ViewModel.PropertyChanged += ChangeCanExecute;
        }

        #endregion

        #region Methods

        private void ChangeCanExecute(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.SelectedItem))
            {
                CanExecuteProperty = ViewModel.SelectedItem != null;
            }
        }

        public override void Execute(object parameter)
        {
            ViewModel?.Delete();
        }

        #endregion
    }

}
