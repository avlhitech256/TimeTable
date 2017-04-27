namespace TimeTable.ViewModel.TopMenu.Command
{
    internal class SpecialtyCommand : TopMenuCommonCommand
    {
        #region Constructors

        public SpecialtyCommand(ITopMenuViewModel viewModel) : base(viewModel)
        {
            CanExecuteProperty = true;
        }

        #endregion

        #region Methods

        public override void Execute(object parameter)
        {
            ViewModel?.SelectSpecialtyMenu();
        }

        #endregion
    }

}
