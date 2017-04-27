namespace TimeTable.ViewModel.TopMenu.Command
{
    internal class HighSchoolComman : TopMenuCommonCommand
    {
        #region Constructors

        public HighSchoolComman(ITopMenuViewModel viewModel) : base(viewModel)
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
