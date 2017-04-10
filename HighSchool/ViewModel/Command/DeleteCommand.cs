using System.Windows.Input;

namespace HighSchool.ViewModel.Command
{
    internal class DeleteCommand : CommonCommand, ICommand
    {
        #region Constructors

        public DeleteCommand(IHighSchoolViewModel viewModel) : base(viewModel)
        {
            CanExecuteProperty = true;
        }

        #endregion

        #region Methods

        public override void Execute(object parameter)
        {
            ViewModel?.Delete();
        }

        #endregion
    }

}
