using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Windows;
using Common.Data.Enum;
using Common.DomainContext;
using Common.Messenger;
using Common.Messenger.Impl;
using TimeTable.ViewModel.MainWindow;

namespace TimeTable
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors

        public MainWindow()
        {
            List<string> picturesList = CreatePicturesList();
            SplashScreen splashScreen = CreateSplashScreen(picturesList);

            InitializeComponent();
            DomainContext = new DomainContext();
            DataContext = new MainWindowViewModel(DomainContext);
            SetDomainContext();
            SubsribeMessenger();

            if (splashScreen != null)
            {
                //Hide();
                //Thread.Sleep(3000);
                DateTime now = DateTime.Now.AddSeconds(3);
                splashScreen.Close(TimeSpan.FromSeconds(1));

                while (DateTime.Now < now)
                {

                }

            }

        }

        #endregion

        #region Properties

        private IDomainContext DomainContext { get; }
        private IMessenger Messenger => DomainContext?.Messenger;

        #endregion

        #region Methods

        private void SetDomainContext()
        {
            LeftMenuControl.DomainContext = DomainContext;
            FooterBarControl.DomainContext = DomainContext;
        }

        private void SubsribeMessenger()
        {
            if (Messenger != null)
            {
                Messenger.Register<EntityException>(CommandName.ShowEntityException,
                                                    ShowEntityException,
                                                    CanShowEntityException);
                Messenger.Register<DbEntityValidationException>(CommandName.ShowDbEntityValidationException,
                                                                ShowDbEntityValidationException,
                                                                CanShowDbEntityValidationException);
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

        #region EntityException
        private void ShowEntityException(EntityException exception)
        {
            string header = exception.Source +
                            " - Ошибка соединения с сервером баз данных (" + exception.HResult + ")";
            StringBuilder message = new StringBuilder();
            message.AppendLine(exception.Message);
            message.AppendLine(" ");

            if (!string.IsNullOrWhiteSpace(exception.HelpLink))
            {
                message.AppendLine("Дополнительную информацию об ошибке можно посмотреть перейдя по нижеприведенной ссылке: ");
                message.AppendLine(exception.HelpLink);
                message.AppendLine(" ");
            }

            message.AppendLine("Данная ошибка возникла в следующем методе: " + exception.TargetSite);
            message.AppendLine(exception.StackTrace);

            MessageBox.Show(message.ToString(), header, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK);
        }

        private bool CanShowEntityException(EntityException exception)
        {
            return exception != null;
        }

        #endregion

        #region DbEntityValidationException

        private void ShowDbEntityValidationException(DbEntityValidationException exception)
        {
            string header = exception.Source +
                            " - Валидационная ошибка сущностей баз данных (" + exception.HResult + ")";
            StringBuilder message = new StringBuilder();
            message.AppendFormat(exception.Message);
            message.AppendLine(" ");

            if (!string.IsNullOrWhiteSpace(exception.HelpLink))
            {
                message.AppendLine("Дополнительную информацию об ошибке можно посмотреть перейдя по нижеприведенной ссылке: ");
                message.AppendLine(exception.HelpLink);
                message.AppendLine(" ");
            }

            message.AppendLine("Данная ошибка возникла в следующем методе: " + exception.TargetSite);
            message.AppendLine(" ");
            message.AppendLine(exception.StackTrace);
            message.AppendLine(" ");
            message.AppendLine("==========================================");
            message.AppendLine("Список валидационных ошибок:");
            message.AppendLine("------------------------------------------");
            string firstIndentation = "    ";
            string secondIndentation = "        ";

            foreach (var entityValidationError in exception.EntityValidationErrors.Where(x => !x.IsValid))
            {
                try
                {
                    message.AppendLine("Сущность: " + nameof(entityValidationError.Entry.Entity));

                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        string propertyName = validationError.PropertyName;
                        string originalValue = entityValidationError.Entry.OriginalValues[propertyName].ToString();
                        string currentValue = entityValidationError.Entry.CurrentValues[propertyName].ToString();
                        string dbValue = entityValidationError.Entry.GetDatabaseValues()[propertyName].ToString();
                        message.AppendLine(firstIndentation + "Поле [" + propertyName + "]:");
                        message.AppendLine(secondIndentation + "Значение поля до изменения: " + originalValue);
                        message.AppendLine(secondIndentation + "Значение после изменений: " + currentValue);
                        message.AppendLine(secondIndentation +
                                           "Значение после, которое сейчас находится в базе данных: " +
                                           dbValue);
                        message.AppendLine(secondIndentation + "Ошибка: " + validationError.ErrorMessage);
                    }
                }
                catch (EntityException e)
                {
                    ShowEntityException(e);
                }

            }

            MessageBox.Show(message.ToString(), header, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK);
        }

        private bool CanShowDbEntityValidationException(DbEntityValidationException exception)
        {
            return exception != null;
        }

        #endregion

        #region InvalidRequiredCode

        private void ShowInvalidRequiredCodeMessage(EventArgs args)
        {
            MessageBox.Show("Поле \"Код:\" не заполнено. Данное поле" + Environment.NewLine +
                "является обязательным. Заполните это поле" + Environment.NewLine +
                "и посторите попытку сохранения снова.",
                "Ошибка сохранения!",
                MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        }

        private bool CanShowInvalidRequiredCodeMessage(EventArgs args)
        {
            return true;
        }

        #endregion

        #region InvalidateUniqueCode

        private void ShowInvalidateUniqueCodeMessage(EventArgs args)
        {
            MessageBox.Show("Поле \"Код:\" является уникальным. Данное поле" + Environment.NewLine +
                            "содержит данные, которые уже были внесены в одну" + Environment.NewLine +
                            "из сохраненных записей. Измените значение этого" + Environment.NewLine +
                            "поля и посторите попытку сохранения снова.",
                            "Ошибка сохранения!",
                            MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        }

        private bool CanShowInvalidateUniqueCodeMessage(EventArgs args)
        {
            return true;
        }

        #endregion

        #region MismatchSearchCriteria

        private void ShowMismatchSearchCriteriaMessage(EventArgs args)
        {
            MessageBox.Show("Критерии поиска не включают" + Environment.NewLine +
                            "добавленные или измененные записи" + Environment.NewLine +
                            "учебных заведений. Для их отображения" + Environment.NewLine +
                            "измените критерии поиска.",
                            "Поиск записей учебных заведений",
                            MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
        }

        private bool CanShowMismatchSearchCriteriaMessage(EventArgs args)
        {
            return true;
        }

        #endregion

        #region RequestForBack

        private void ShowRequestForBackMessage(EventArgs args)
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

        private bool CanShowRequestForBackMessage(EventArgs args)
        {
            return true;
        }

        #endregion

        #region RequestForDelete

        private void ShowRequestForDeleteMessage(EventArgs args)
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

        private bool CanShowRequestForDeleteMessage(EventArgs args)
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

        private List<string> CreatePicturesList()
        {
            List<string> result = new List<string>();

            result.Add("Pictures/ipsa0.gif");
            result.Add("Pictures/ipsa1.jpg");
            result.Add("Pictures/ipsa10.jpg");
            result.Add("Pictures/ipsa11.jpg");
            result.Add("Pictures/ipsa11.png");
            result.Add("Pictures/ipsa12.jpg");
            result.Add("Pictures/ipsa12.png");
            result.Add("Pictures/ipsa13.jpg");
            result.Add("Pictures/ipsa13.png");
            result.Add("Pictures/ipsa14.jpg");
            result.Add("Pictures/ipsa14.png");
            result.Add("Pictures/ipsa15.jpg");
            result.Add("Pictures/ipsa15.png");
            result.Add("Pictures/ipsa16.jpg");
            result.Add("Pictures/ipsa16.png");
            result.Add("Pictures/ipsa17.jpg");
            result.Add("Pictures/ipsa17.png");
            result.Add("Pictures/ipsa18.jpg");
            result.Add("Pictures/ipsa18.png");
            result.Add("Pictures/ipsa19.jpg");
            result.Add("Pictures/ipsa2.jpg");
            result.Add("Pictures/ipsa20.jpg");
            result.Add("Pictures/ipsa21.jpg");
            result.Add("Pictures/ipsa22.jpg");
            result.Add("Pictures/ipsa23.jpg");
            result.Add("Pictures/ipsa24.jpg");
            result.Add("Pictures/ipsa25.jpg");
            result.Add("Pictures/ipsa26.jpg");
            result.Add("Pictures/ipsa3.png");
            result.Add("Pictures/ipsa4.JPG");
            result.Add("Pictures/ipsa5.jpg");
            result.Add("Pictures/ipsa6.jpg");
            result.Add("Pictures/ipsa7.jpg");
            result.Add("Pictures/ipsa7.png");
            result.Add("Pictures/ipsa8.jpg");
            result.Add("Pictures/ipsa9.png");

            return result;
        }

        private SplashScreen CreateSplashScreen(List<string> picturesList)
        {
            SplashScreen splashScreen = null;
            List<int> indexList = new List<int>();
            bool isNotCreatedSplashScreen = true;
            int currentSeconds = DateTime.Now.Second;

            while (isNotCreatedSplashScreen && indexList.Count < picturesList.Count)
            {
                int index = currentSeconds % picturesList.Count;

                if (!indexList.Contains(index))
                {
                    indexList.Add(index);
                    string pathPicture = picturesList[index];
                    splashScreen = new SplashScreen(pathPicture);

                    try
                    {
                        isNotCreatedSplashScreen = false;
                        splashScreen.Show(false);
                    }
                    catch (Exception)
                    {
                        isNotCreatedSplashScreen = true;
                    }

                }

            }

            return splashScreen;
        }

        #endregion
    }

}
