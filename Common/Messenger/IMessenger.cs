using System;
using Common.Messenger.Impl;

namespace Common.Messenger
{
    public interface IMessenger
    {
        void Register<T>(CommandName commandType, Action<T> execute, Func<T, bool> canExecute) where T : class;
        void MultiRegister<T>(CommandName commandType, Action<T> execute, Func<T, bool> canExecute) where T : class;
        void Unregister(CommandName commandType);
        void MultiUnregister(CommandName commandType);
        void MultiUnregister<T>(CommandName commandType, Action<T> execute, Func<T, bool> canExecute) where T : class;
        void Send<T>(CommandName commandType, T arg) where T : class;
    }
}
