using System;
using System.Windows;
using System.Windows.Controls;
using Common.Messenger;
using Common.Messenger.Impl;
using Domain.Data.Enum;
using Domain.DomainContext;

namespace CommonControl.EditControl
{
    public class EditControl : UserControl
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
            if (Messenger != null)
            {
                Messenger.Register<EventArgs>(CommandName.ShowInvalidRequiredCodeMessage,
                                              ShowInvalidRequiredCodeMessage,
                                              CanShowInvalidRequiredCodeMessage);
                Messenger.Register<EventArgs>(CommandName.ShowInvalidateUniqueCodeMessage,
                                              ShowInvalidateUniqueCodeMessage,
                                              CanShowInvalidateUniqueCodeMessage);
                Messenger.Register<EventArgs>(CommandName.ShowMismatchSearchCriteriaMessage,
                                              ShowMismatchSearchCriteriaMessage,
                                              CanShowMismatchSearchCriteriaMessage);
                Messenger.Register<EventArgs>(CommandName.RequestForBack,
                                              ShowRequestForBackMessage,
                                              CanShowRequestForBackMessage);
                Messenger.Register<EventArgs>(CommandName.RequestForDelete,
                                              ShowRequestForDeleteMessage,
                                              CanShowRequestForDeleteMessage);
            }

        }

        protected virtual void UnsubscribeMessenger()
        {
            if (Messenger != null)
            {
                Messenger.Unregister(CommandName.ShowInvalidRequiredCodeMessage);
                Messenger.Unregister(CommandName.ShowInvalidateUniqueCodeMessage);
                Messenger.Unregister(CommandName.ShowMismatchSearchCriteriaMessage);
                Messenger.Unregister(CommandName.RequestForBack);
                Messenger.Unregister(CommandName.RequestForDelete);
            }

        }

        #region InvalidRequiredCode

        protected virtual void ShowInvalidRequiredCodeMessage(EventArgs args)
        {
            MessageBox.Show("Поле \"Код:\" не заполнено. Данное поле" + Environment.NewLine +
                "является обязательным. Заполните это поле" + Environment.NewLine +
                "и посторите попытку сохранения снова.",
                "Ошибка сохранения!",
                MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        }

        protected virtual bool CanShowInvalidRequiredCodeMessage(EventArgs args)
        {
            return true;
        }

        #endregion

        #region InvalidateUniqueCode

        protected virtual void ShowInvalidateUniqueCodeMessage(EventArgs args)
        {
            MessageBox.Show("Поле \"Код:\" является уникальным. Данное поле" + Environment.NewLine +
                            "содержит данные, которые уже были внесены в одну" + Environment.NewLine +
                            "из сохраненных записей. Измените значение этого" + Environment.NewLine +
                            "поля и посторите попытку сохранения снова.",
                            "Ошибка сохранения!",
                            MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        }

        protected virtual bool CanShowInvalidateUniqueCodeMessage(EventArgs args)
        {
            return true;
        }

        #endregion

        #region MismatchSearchCriteria

        protected virtual void ShowMismatchSearchCriteriaMessage(EventArgs args)
        {
            MessageBox.Show("Критерии поиска не включают" + Environment.NewLine +
                            "добавленные или измененные записи" + Environment.NewLine +
                            "учебных заведений. Для их отображения" + Environment.NewLine +
                            "измените критерии поиска.",
                            "Поиск записей учебных заведений",
                            MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
        }

        protected virtual bool CanShowMismatchSearchCriteriaMessage(EventArgs args)
        {
            return true;
        }

        #endregion

        #region RequestForBack

        protected virtual void ShowRequestForBackMessage(EventArgs args)
        {
            MessageBoxResult messageResult = MessageBox.Show("В текущую запись учебного заведения" +
                                                             Environment.NewLine +
                                                             "были внесены изменения." +
                                                             Environment.NewLine + Environment.NewLine +
                                                             "Сохранить внесенные изменения?",
                                                             "Сохранение текущей записи",
                                                             MessageBoxButton.YesNoCancel,
                                                             MessageBoxImage.Question,
                                                             MessageBoxResult.Yes);

            if (DomainContext != null && DomainContext.ViewModel != null)
            {
                ValueEnum result = ConvertToValueEnum(messageResult);
                DomainContext.ViewModel.SetResponseForBack(result);
            }

        }

        protected virtual bool CanShowRequestForBackMessage(EventArgs args)
        {
            return true;
        }

        #endregion

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
