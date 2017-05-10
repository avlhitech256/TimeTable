namespace TimeTable.ViewModel.TopMenu.Command
{
    internal class HighSchoolCommand : TopMenuCommonCommand
    {
        #region Constructors

        public HighSchoolCommand(ITopMenuViewModel viewModel) : base(viewModel)
        {
            CanExecuteProperty = true;
        }

        #endregion

        #region Methods

        public override void Execute(object parameter)
        {
            ViewModel?.SelectHighSchoolMenu();
        }

        #endregion
    }
}
