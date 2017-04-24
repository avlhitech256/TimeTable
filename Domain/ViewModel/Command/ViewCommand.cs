using System.ComponentModel;

namespace Domain.ViewModel.Command
{
    internal class ViewCommand<T> : CommonCommand<T> where T : class
    {
        #region Constructors

        public ViewCommand(IDataViewModel<T> viewModel) : base(viewModel)
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
            ViewModel?.View();
        }

        #endregion
    }
}
