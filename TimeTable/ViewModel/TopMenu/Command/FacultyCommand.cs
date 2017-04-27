namespace TimeTable.ViewModel.TopMenu.Command
{
    internal class FacultyCommand : TopMenuCommonCommand
    {
        #region Constructors

        public FacultyCommand(ITopMenuViewModel viewModel) : base(viewModel)
        {
            CanExecuteProperty = true;
        }

        #endregion

        #region Methods

        public override void Execute(object parameter)
        {
            ViewModel?.SelectFacultyMenu();
        }

        #endregion
    }

}
