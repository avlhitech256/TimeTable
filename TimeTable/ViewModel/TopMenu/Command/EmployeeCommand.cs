namespace TimeTable.ViewModel.TopMenu.Command
{
    internal class EmployeeCommand : TopMenuCommonCommand
    {
        #region Constructors

        public EmployeeCommand(ITopMenuViewModel viewModel) : base(viewModel)
        {
            CanExecuteProperty = true;
        }

        #endregion

        #region Methods

        public override void Execute(object parameter)
        {
            ViewModel?.SelectEmployeeMenu();
        }

        #endregion
    }

}
