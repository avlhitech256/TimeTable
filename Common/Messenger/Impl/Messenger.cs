using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Common.Exception;

namespace Common.Messenger.Impl
{
    public class Messenger : IMessenger
    {
        #region Members

        private readonly IDictionary<CommandName, ICommand> commands;
        private readonly IDictionary<CommandName, List<ICommand>> multiCommands;

        #endregion

        #region Constructors
        public Messenger()
        {
            commands = new Dictionary<CommandName, ICommand>();
            multiCommands = new Dictionary<CommandName, List<ICommand>>();
        }

        #endregion

        #region Methods
        public void Register<T>(CommandName commandType, Action<T> execute, Func<T, bool> canExecute) where T : class
        {
            ICommand command = new MessengerCommand<T>(execute, canExecute);

            if (multiCommands.ContainsKey(commandType))
            {
                OnBusinessLogicException(commandType);
            }
            else
            {
                if (commands.ContainsKey(commandType))
                {
                    commands[commandType] = command;
                }
                else
                {
                    commands.Add(commandType, command);
                }

            }

        }

        public void MultiRegister<T>(CommandName commandType, Action<T> execute, Func<T, bool> canExecute) where T : class
        {
            ICommand command = new MessengerCommand<T>(execute, canExecute);

            if (commands.ContainsKey(commandType))
            {
                OnBusinessLogicException(commandType);
            }
            else
            {
                if (multiCommands.ContainsKey(commandType))
                {
                    if (multiCommands[commandType].All(x => !command.Equals(x)))
                    {
                        multiCommands[commandType].Add(command);
                    }

                }
                else
                {
                    multiCommands.Add(commandType, new List<ICommand> {command});
                }

            }

        }

        public void Unregister(CommandName commandType)
        {
            if (multiCommands.ContainsKey(commandType))
            {
                OnBusinessLogicException(commandType);
            }
            else
            {
                commands.Remove(commandType);
            }

        }

        public void MultiUnregister(CommandName commandType)
        {
            if (commands.ContainsKey(commandType))
            {
                OnBusinessLogicException(commandType);
            }
            else
            {
                multiCommands.Remove(commandType);
            }

        }

        public void MultiUnregister<T>(CommandName commandType, Action<T> execute, Func<T, bool> canExecute) where T : class
        {
            if (commands.ContainsKey(commandType))
            {
                OnBusinessLogicException(commandType);
            }
            else
            {
                MessengerCommand<T> command = new MessengerCommand<T>(execute, canExecute);

                if (multiCommands.ContainsKey(commandType) && multiCommands[commandType] != null)
                {
                    multiCommands[commandType].RemoveAll(x => command.Equals(x));

                    if (!multiCommands[commandType].Any())
                    {
                        MultiUnregister(commandType);
                    }

                }
            }

        }

        public void Send<T>(CommandName commandType, T arg) where T : class
        {
            Action<ICommand> executeCommand =
                (command) =>
                {
                    if (null != command && command.CanExecute(arg))
                    {
                        command.Execute(arg);
                    }

                };

            if (commands.ContainsKey(commandType) && multiCommands.ContainsKey(commandType))
            {
                OnBusinessLogicException(commandType);
            }
            else
            {
                if (commands.ContainsKey(commandType))
                {
                    executeCommand(commands[commandType]);
                }
                else
                {
                    if (multiCommands.ContainsKey(commandType) && multiCommands[commandType] != null)
                    {
                        multiCommands[commandType].ForEach(executeCommand);
                    }

                }

            }

        }

        private void OnBusinessLogicException(CommandName commandType)
        {
            StringBuilder message = new StringBuilder();
            message.AppendFormat("Для команды {0} используются одновременно два режима", nameof(commandType));
            message.Append("(Single и Multi mode). Для одного типа команды можно истользовать только");
            message.Append("один, из перечисленных выше, режимов.");
            throw new BusinessLogicException(message.ToString());
        }

        #endregion

    }

}
