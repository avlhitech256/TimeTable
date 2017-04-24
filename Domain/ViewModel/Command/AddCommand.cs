using System.ComponentModel;

namespace Domain.ViewModel.Command
{
    internal class AddCommand<T> : CommonCommand<T> where T : class 
    {
        #region Constructors

        public AddCommand(IDataViewModel<T> viewModel) : base(viewModel)
        {
            if (ViewModel != null)
            {
                ViewModel.PropertyChanged += ChangeCanExecute;
                CanExecuteProperty = ViewModel.ReadOnly;
            }

        }

        #endregion

        #region Methods

        private void ChangeCanExecute(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.ReadOnly))
            {
                CanExecuteProperty = ViewModel.ReadOnly;
            }

        }

        public override void Execute(object parameter)
        {
            ViewModel?.Add();
        }

        #endregion
    }

}
