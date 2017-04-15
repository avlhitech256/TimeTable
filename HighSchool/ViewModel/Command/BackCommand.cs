using System.Windows.Input;

namespace HighSchool.ViewModel.Command
{
    internal class BackCommand : CommonCommand, ICommand
    {
        #region Constructors

        public BackCommand(IHighSchoolViewModel viewModel) : base(viewModel)
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
