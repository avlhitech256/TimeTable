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

        public bool Equals(MessengerCommand<T> other)
        {
            bool result = execute == other.execute && 
                          canExecute == other.canExecute;

            return result;
        }

        public override bool Equals(object other)
        {
            MessengerCommand<T> messengerCommand = other as MessengerCommand<T>;
            bool result = false;

            if (messengerCommand != null)
            {
                result = this.Equals(messengerCommand);
            }

            return result;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((execute?.GetHashCode() ?? 0)*397) ^ (canExecute?.GetHashCode() ?? 0);
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
