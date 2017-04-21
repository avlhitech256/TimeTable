﻿using System.ComponentModel;
using System.Windows.Input;

namespace Domain.ViewModel.Command
{
    internal class EditCommand<T> : CommonCommand<T>, ICommand where T : class 
    {
        #region Constructors

        public EditCommand(IDataViewModel<T> viewModel) : base(viewModel)
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
            ViewModel?.Edit();
        }

        #endregion
    }
}