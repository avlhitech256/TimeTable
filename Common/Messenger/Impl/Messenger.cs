using System;
using System.Collections.Generic;
using System.Windows.Input;
using Common.Messenger.Impl;

namespace Common.Messenger
{
    public class Messenger : IMessenger
    {
        #region Members

        private IDictionary<CommandName, ICommand> commands;

        #endregion

        #region Constructors
        public Messenger()
        {
            commands = new Dictionary<CommandName, ICommand>();
        }

        #endregion

        #region Methods
        public void Register<T>(CommandName commandType, Action<T> execute, Func<T, bool> canExecute) where T : class
        {
            ICommand command = new MessengerCommand<T>(execute, canExecute);

            if (commands.ContainsKey(commandType))
            {
                commands[commandType] = command;
            }
            else
            {
                commands.Add(commandType, command);
            }

        }

        public void Unregister(CommandName commandType)
        {
            commands.Remove(commandType);
        }

        public void Send<T>(CommandName commandType, T arg) where T : class
        {
            if (commands.ContainsKey(commandType))
            {
                ICommand command = commands[commandType];

                if (null != command && command.CanExecute(arg))
                {
                    command.Execute(arg);
                }

            }

        }

        #endregion

    }

}
