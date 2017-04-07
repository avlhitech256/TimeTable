using System;
using System.Security.Policy;
using System.Windows.Input;
using Domain.Data.Enum;
using Domain.Event;
using Domain.Messenger;
using Domain.Messenger.Impl;

namespace HighSchool.ViewModel.Command
{
    internal class EditCommand : ICommand
    {
        #region Members

        private bool canExecute;

        #endregion

        #region Constructors

        public EditCommand(IHighSchoolViewModel viewModel)
        {
            ViewModel = viewModel;
            ViewModel.SearchCriteria.SearchCriteriaChanged += ChangeCanExecute;
            CanExecuteProperty = true;
        }

        #endregion

        #region Properties

        private IHighSchoolViewModel ViewModel { get; }

        private bool CanExecuteProperty
        {
            get
            {
                return canExecute;
            }

            set
            {
                if (canExecute != value)
                {
                    canExecute = value;
                    OnCanExecuteChanged();
                }
            }

        }

        #endregion

        #region Methods

        private void ChangeCanExecute(object sender, EventArgs args)
        {
            CanExecuteProperty = true;
        }

        private void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }


        public bool CanExecute(object parameter)
        {
            return CanExecuteProperty;
        }

        public void Execute(object parameter)
        {
            IMessenger messenger = ViewModel.Messenger;

            if (messenger != null)
            {
                ViewModel.IsEditControl = true;
                messenger.Send(CommandName.SetEntryControl, new MenuChangedEventArgs(MenuItemName.HighSchool));
            }

        }

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged = delegate { };

        #endregion
    }
}
