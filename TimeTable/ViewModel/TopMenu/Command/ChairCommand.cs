namespace TimeTable.ViewModel.TopMenu.Command
{
    internal class ChairCommand : TopMenuCommonCommand
    {
        #region Constructors

        public ChairCommand(ITopMenuViewModel viewModel) : base(viewModel)
        {
            CanExecuteProperty = true;
        }

        #endregion

        #region Methods

        public override void Execute(object parameter)
        {
            ViewModel?.SelectChairMenu();
        }

        #endregion
    }

}
