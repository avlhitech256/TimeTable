﻿using System;
using System.ComponentModel;
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
            ViewModel.PropertyChanged += ChangeCanExecute;
            CanExecuteProperty = false;
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

        private void ChangeCanExecute(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.SelectedHighSchool))
            {
                CanExecuteProperty = ViewModel.SelectedHighSchool != null;
            }
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
            IMessenger messenger = ViewModel?.Messenger;

            if (ViewModel != null && messenger != null)
            {
                ViewModel.ReadOnly = false;
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
