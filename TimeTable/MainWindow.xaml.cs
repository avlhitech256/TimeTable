using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Windows;
using Common.Messenger;
using Common.Messenger.Impl;
using Domain.DomainContext;
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
            SubscribeMessenger();

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

        private void SubscribeMessenger()
        {
            if (Messenger != null)
            {
                Messenger.Register<EntityException>(CommandName.ShowEntityException,
                                                    ShowEntityException,
                                                    CanShowEntityException);
                Messenger.Register<DbEntityValidationException>(CommandName.ShowDbEntityValidationException,
                                                                ShowDbEntityValidationException,
                                                                CanShowDbEntityValidationException);
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
