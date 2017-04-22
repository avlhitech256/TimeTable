using System;

namespace Domain.ViewModel.Command
{
    abstract public class CommonCommand<T> where T : class
    {
        #region Members

        private bool canExecute;

        #endregion

        #region Constructors

        protected CommonCommand(IDataViewModel<T> viewModel)
        {
            ViewModel = viewModel;
            CanExecuteProperty = false;
        }

        #endregion

        #region Properties

        protected IDataViewModel<T> ViewModel { get; }

        protected bool CanExecuteProperty
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

        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }


        public virtual bool CanExecute(object parameter)
        {
            return CanExecuteProperty;
        }

        public abstract void Execute(object parameter);

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged = delegate { };

        #endregion
    }
}
