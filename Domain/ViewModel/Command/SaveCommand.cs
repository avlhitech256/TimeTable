using System.ComponentModel;
using System.Windows.Input;

namespace Domain.ViewModel.Command
{
    internal class SaveCommand<T> : CommonCommand<T>, ICommand where T : class 
    {
        #region Constructors

        public SaveCommand(IDataViewModel<T> viewModel) : base(viewModel)
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
            ViewModel?.Save();
        }

        #endregion
    }

}
