using System.ComponentModel;
using System.Windows.Input;

namespace HighSchool.ViewModel.Command
{
    internal class SaveAndNewCommand : CommonCommand, ICommand
    {
        #region Constructors

        public SaveAndNewCommand(IHighSchoolViewModel viewModel) : base(viewModel)
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
            ViewModel?.SaveAndAdd();
        }

        #endregion
    }
}
