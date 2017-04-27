namespace TimeTable.ViewModel.TopMenu.Command
{
    internal class SpecializationCommand : TopMenuCommonCommand
    {
        #region Constructors

        public SpecializationCommand(ITopMenuViewModel viewModel) : base(viewModel)
        {
            CanExecuteProperty = true;
        }

        #endregion

        #region Methods

        public override void Execute(object parameter)
        {
            ViewModel?.SelectSpecializationMenu();
        }

        #endregion
    }

}
