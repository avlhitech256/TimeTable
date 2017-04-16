using System;
using System.Windows.Input;

namespace Common.Messenger.Impl
{
    public class MessengerCommand<T> : ICommand where T : class
    {
        #region Members

        private bool canExecuteCommand = true;
        private readonly Action<T> execute;
        private readonly Func<T, bool> canExecute;

        #endregion

        #region Constructors

        public MessengerCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        #endregion

        #region Methods
        public bool CanExecute(object parameter)
        {
            bool result = true;
            T arg = parameter as T;

            if (null != arg)
            {
                result = canExecute(arg);

                if (canExecuteCommand != result)
                {
                    canExecuteCommand = result;
                    OnCanExecuteChanged();
                }

            }

            return result;
        }

        public void Execute(object parameter)
        {
            T arg = parameter as T;

            if (null != arg)
            {
                execute(arg);
            }
        }

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged;

        private void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        #endregion
    }
}
