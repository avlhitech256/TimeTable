using System;
using System.Windows;
using System.Windows.Controls;
using Common.Messenger;
using Common.Messenger.Impl;
using Domain.Data.Enum;
using Domain.DomainContext;

namespace CommonControl.SearchControl
{
    public class SearchControl : UserControl
    {
        #region Members

        private IDomainContext domainContext;
        private IMessenger messenger;

        #endregion

        #region Properties

        public IDomainContext DomainContext
        {
            get
            {
                return domainContext;
            }

            set
            {
                if (domainContext != value)
                {
                    domainContext = value;

                    if (value != null)
                    {
                        Messenger = value.Messenger;
                    }

                }

            }

        }

        private IMessenger Messenger
        {
            get
            {
                return messenger;
            }

            set
            {
                if (messenger != value)
                {
                    UnsubscribeMessenger();
                    messenger = value;
                    SubscribeMessenger();
                }

            }

        }

        #endregion

        #region Methods

        protected virtual void SubscribeMessenger()
        {
            Messenger?.Register<EventArgs>(CommandName.RequestForDelete,
                                           ShowRequestForDeleteMessage,
                                           CanShowRequestForDeleteMessage);
        }

        protected virtual void UnsubscribeMessenger()
        {
            Messenger?.Unregister(CommandName.RequestForDelete);
        }

        #region RequestForDelete

        protected virtual void ShowRequestForDeleteMessage(EventArgs args)
        {
            MessageBoxResult messageResult = MessageBox.Show("Текущая запись учебного заведения" +
                                                             Environment.NewLine +
                                                             "будет удалена без возможности" +
                                                             Environment.NewLine +
                                                             "восстановления!" +
                                                             Environment.NewLine + Environment.NewLine +
                                                             "Вы уверены, что ходите сделать" +
                                                             Environment.NewLine + "данную операцию?",
                                                             "Удаление текущей записи",
                                                             MessageBoxButton.YesNo,
                                                             MessageBoxImage.Stop,
                                                             MessageBoxResult.Yes);

            if (DomainContext != null && DomainContext.ViewModel != null)
            {
                ValueEnum result = ConvertToValueEnum(messageResult);
                DomainContext.ViewModel.SetResponseForDelete(result);
            }

        }

        protected virtual bool CanShowRequestForDeleteMessage(EventArgs args)
        {
            return true;
        }

        #endregion
        private ValueEnum ConvertToValueEnum(MessageBoxResult messageBoxResult)
        {
            ValueEnum result;

            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    result = ValueEnum.Yes;
                    break;
                case MessageBoxResult.No:
                    result = ValueEnum.No;
                    break;
                case MessageBoxResult.Cancel:
                    result = ValueEnum.Cancel;
                    break;
                default:
                    result = ValueEnum.Cancel;
                    break;
            }

            return result;
        }

        #endregion
    }
}
