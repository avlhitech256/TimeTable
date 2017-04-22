using System.Windows.Input;

namespace Domain.ViewModel.Command
{
    internal class BackCommand<T> : CommonCommand<T>, ICommand where T : class 
    {
        #region Constructors

        public BackCommand(IDataViewModel<T> viewModel) : base(viewModel)
        {
            CanExecuteProperty = true;
        }

        #endregion

        #region Methods

        public override void Execute(object parameter)
        {
            ViewModel?.Back();
        }

        #endregion
    }
}
