using System.ComponentModel;
using System.Windows.Input;

namespace HighSchool.ViewModel.Command
{
    class SaveCommand : CommonCommand, ICommand
    {
        #region Constructors

        public SaveCommand(IHighSchoolViewModel viewModel) : base(viewModel)
        {
            if (ViewModel != null)
            {
                ViewModel.PropertyChanged += ChangeCanExecute;
            }

        }

        #endregion

        #region Methods

        private void ChangeCanExecute(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.HasChanges))
            {
                CanExecuteProperty = ViewModel.HasChanges && !ViewModel.ReadOnly;
            }

        }

        public override void Execute(object parameter)
        {
            CanExecuteProperty = false;
            ViewModel?.Save();
        }

        #endregion
    }

}
