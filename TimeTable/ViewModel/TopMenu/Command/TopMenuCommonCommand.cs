using System;
using System.Windows.Input;
using TimeTable.Annotations;

namespace TimeTable.ViewModel.TopMenu.Command
{
    abstract public class TopMenuCommonCommand : ICommand
    {
        #region Members

        private bool canExecute;

        #endregion

        #region Constructors

        protected TopMenuCommonCommand(ITopMenuViewModel viewModel)
        {
            ViewModel = viewModel;
            CanExecuteProperty = false;
        }

        #endregion

        #region Properties

        [CanBeNull]
        protected ITopMenuViewModel ViewModel { get; }

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
