using System.ComponentModel;
using System.Windows.Input;

namespace HighSchool.ViewModel.Command
{
    internal class AddCommand : CommonCommand, ICommand
    {
        #region Constructors

        public AddCommand(IHighSchoolViewModel viewModel) : base(viewModel)
        {
            if (ViewModel != null)
            {
                ViewModel.PropertyChanged += ChangeCanExecute;
                CanExecuteProperty = ViewModel.ReadOnly;
            }

        }

        #endregion

        #region Methods

        private void ChangeCanExecute(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.ReadOnly))
            {
                CanExecuteProperty = ViewModel.ReadOnly;
            }

        }

        public override void Execute(object parameter)
        {
            ViewModel?.Add();
        }

        #endregion
    }

}
