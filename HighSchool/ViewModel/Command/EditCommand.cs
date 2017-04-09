using System.ComponentModel;
using System.Windows.Input;
using Domain.Data.Enum;
using Domain.Event;
using Domain.Messenger;
using Domain.Messenger.Impl;

namespace HighSchool.ViewModel.Command
{
    internal class EditCommand : CommonCommand, ICommand
    {
        #region Constructors

        public EditCommand(IHighSchoolViewModel viewModel) : base(viewModel)
        {
            ViewModel.PropertyChanged += ChangeCanExecute;
        }

        #endregion

        #region Methods

        private void ChangeCanExecute(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.SelectedItem))
            {
                CanExecuteProperty = ViewModel.SelectedItem != null;
            }
        }

        public override void Execute(object parameter)
        {
            IMessenger messenger = ViewModel?.Messenger;

            if (ViewModel != null && messenger != null)
            {
                ViewModel.ReadOnly = false;
                ViewModel.IsEditControl = true;
                messenger.Send(CommandName.SetEntryControl, new MenuChangedEventArgs(MenuItemName.HighSchool));
            }

        }

        #endregion
    }
}
