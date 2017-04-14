using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Core;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Common.Data.Notifier;
using Domain.Data.Enum;
using Domain.Messenger;
using Domain.DomainContext;
using Domain.Entity.HighSchool;
using Domain.Event;
using Domain.Messenger.Impl;
using HighSchool.Model;
using HighSchool.ViewModel.Command;

namespace HighSchool.ViewModel
{
    public class HighSchoolViewModel : Notifier, IHighSchoolViewModel
    {
        #region Members

        private IHighSchoolEntity oldHighSchool;
        private bool hasChanges;
        private bool readOnly;
        private bool isEditControl;
        private string toolTipForEditButton;
        private bool enabled;

        #endregion

        #region Constructors
        public HighSchoolViewModel(IDomainContext context)
        {
            DomainContext = context;
            Model = new HighSchoolModel(DomainContext);
            DomainContext.DataBaseServer = Model.DataBaseServer;
            oldHighSchool = Model?.SelectedHighSchool;
            SubscribeEvents();
            SubscribeMessenger();
            ReadOnly = true;
            IsEditControl = false;
            InitializeButtons();
            InitializeProperties();
        }

        #endregion

        #region Properties

        private IHighSchoolModel Model { get; }

        public HighSchoolSearchCriteria SearchCriteria => Model.SearchCriteria;

        public IHighSchoolEntity SelectedItem
        {
            get
            {
                return Model?.SelectedHighSchool;
            }

            set
            {
                if (Model != null)
                {
                    Model.SelectedHighSchool = value;
                }

            }

        }

        public ObservableCollection<IHighSchoolEntity> HighSchools => Model.HighSchools;

        public ObservableCollection<DataService.Model.Employee> Employees => Model.Employees;

        public bool HasChanges
        {
            get
            {
                return Model?.HasChanges ?? false;
            }

            set
            {
                if (hasChanges != value)
                {
                    hasChanges = value;
                    OnPropertyChanged();
                }

            }

        }

        public IDomainContext DomainContext { get; }

        public IMessenger Messenger => DomainContext.Messenger;

        public string Code
        {
            get
            {
                return SelectedItem?.Code;
            }

            set
            {
                if (SelectedItem != null)
                {
                    SelectedItem.Code = value;
                }

            }

        }

        public string Name
        {
            get
            {
                return SelectedItem?.Name;
            }

            set
           {
                if (SelectedItem != null)
                {
                    SelectedItem.Name = value;
                }

            }

        }

        public bool Active
        {
            get
            {
                return SelectedItem != null && SelectedItem.Active;
            }

            set
            {
                if (SelectedItem != null)
                {
                    SelectedItem.Active = value;
                }

            }

        }
        public long Rector
        {
            get
            {
                return SelectedItem?.Rector ?? 0;
            }

            set
            {
                if (SelectedItem != null)
                {
                    SelectedItem.Rector = value;
                }
            }
        }

        public DateTime Created => SelectedItem?.Created ?? DateTime.MinValue;

        public DateTime LastModify => SelectedItem?.LastModify ?? DateTime.MinValue;

        public string UserModify => SelectedItem?.UserModify;

        public ICommand BackToSearchButtonCommand { get; private set; }

        public ICommand NewButtonCommand { get; private set; }

        public ICommand ViewButtonCommand { get; private set; }

        public ICommand EditButtonCommand { get; private set; }

        public ICommand SaveButtonCommand { get; private set; }

        public ICommand SaveAndNewButtonCommand { get; private set; }

        public ICommand DeleteButtonCommand { get; private set; }

        public ICommand SearchButtonCommand { get; private set; }

        public ICommand ChangeEditModeButtonCommand { get; private set; }

        public string ToolTipForEditButton
        {
            get
            {
                return toolTipForEditButton;
            }

            private set
            {
                if (toolTipForEditButton != value)
                {
                    toolTipForEditButton = value;
                    OnPropertyChanged();
                }

            }

        }

        public bool ReadOnly
        {
            get
            {
                return readOnly;
            }

            set
            {
                if (readOnly != value)
                {
                    readOnly = value;
                    Enabled = !value;
                    OnPropertyChanged();
                }

            }
        }

        public bool Enabled
        {
            get
            {
                return enabled;
            }

            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    ReadOnly = !value;
                    ToolTipForEditButton = value
                        ? "Закончить редактирование текущей записи"
                        : "Редактировать текущую запись";
                    OnPropertyChanged();
                }

            }

        }

        public bool IsEditControl
        {
            get
            {
                return isEditControl;
            }

            set
            {
                if (isEditControl != value)
                {
                    isEditControl = value;
                    OnPropertyChanged();

                    if (!value)
                    {
                        ReadOnly = true;
                    }

                }

            }

        }

        #endregion

        #region Methods

        private void SubscribeEvents()
        {
            if (Model != null)
            {
                Model.PropertyChanged += OnChangedSelectedHighSchool;
                Model.EntityException += OnEntityException;

                if (SelectedItem != null)
                {
                    SelectedItem.PropertyChanged += OnChangedHighSchoolProperties;
                }

            }

        }

        private void SubscribeMessenger()
        {
            Messenger.Register<EntityException>(CommandName.ShowEntityException, 
                                                ShowEntityException, 
                                                CanShowEntityException);
        }

        private void InitializeButtons()
        {
            BackToSearchButtonCommand = new BackCommand(this);
            NewButtonCommand = new AddCommand(this);
            ViewButtonCommand = new ViewCommand(this);
            EditButtonCommand = new EditCommand(this);
            SaveButtonCommand = new SaveCommand(this);
            SaveAndNewButtonCommand = new SaveAndNewCommand(this);
            DeleteButtonCommand = new DeleteCommand(this);
            SearchButtonCommand = new SearchCommand(this);
            ChangeEditModeButtonCommand = null;
        }

        private void InitializeProperties()
        {
            ReadOnly = true;
            Enabled = false;
            ToolTipForEditButton = "Редактировать текущую запись";
        }

        private void OnChangedSelectedHighSchool(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Model.SelectedHighSchool))
            {
                if (oldHighSchool != null)
                {
                    oldHighSchool.PropertyChanged -= OnChangedHighSchoolProperties;
                }

                oldHighSchool = SelectedItem;

                if (oldHighSchool != null)
                {
                    oldHighSchool.PropertyChanged += OnChangedHighSchoolProperties;
                }

                HasChanges = Model?.HasChanges ?? false;
                OnPropertyChanged(nameof(SelectedItem));
                OnPropertyChanged(nameof(Code));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Active));
                OnPropertyChanged(nameof(Created));
                OnPropertyChanged(nameof(LastModify));
                OnPropertyChanged(nameof(UserModify));
            }

        }

        private void OnEntityException(object sender, EntityExceptionEventArgs e)
        {
            ShowEntityException(e.EntityException);
        }

        private void ShowEntityException(EntityException exception)
        {
            string header = exception.Source +
                            " - Ошибка соединения с сервером баз данных (" + exception.HResult + ")";
            string message = exception.Message;

            if (!string.IsNullOrWhiteSpace(exception.HelpLink))
            {
                message = message + Environment.NewLine + Environment.NewLine +
                          "Дополнительную информацию об ошибке можно посмотреть перейдя по нижеприведенной ссылке: " +
                          Environment.NewLine + exception.HelpLink;
            }

            message = message + Environment.NewLine + Environment.NewLine +
                      "Данная ошибка возникла в следующем методе: " + exception.TargetSite +
                      Environment.NewLine + Environment.NewLine + exception.StackTrace;

            MessageBox.Show(message, header, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK);
        }

        private bool CanShowEntityException(EntityException exception)
        {
            return true;
        }

        private void OnChangedHighSchoolProperties(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
            HasChanges = Model?.HasChanges ?? false;
        }

        public void ApplySearchCriteria()
        {
            Model.ApplySearchCriteria();
        }

        public void Add()
        {
            ReadOnly = false;
            GoToEditControl();
            HasChanges = Model.HasChanges;
            Model.Add();
        }

        public void View()
        {
            if (SelectedItem != null)
            {
                ReadOnly = true;
                GoToEditControl();
                HasChanges = Model.HasChanges;
            }

        }

        public void Edit()
        {
            if (SelectedItem != null)
            {
                bool oldReadOnly = ReadOnly;
                Save();
                ReadOnly = !oldReadOnly;
                GoToEditControl();
                HasChanges = Model.HasChanges;
            }

        }

        public void Save()
        {
            if (HasChanges && !ReadOnly && Validate())
            {
                ReadOnly = true;
                Model.Save();
                HasChanges = Model.HasChanges;
            }

        }

        public void SaveAndAdd()
        {
            if (HasChanges && !ReadOnly)
            {
                Save();
                Add();
            }

        }

        public void Delete()
        {
            if (Model != null && SelectedItem != null &&
                MessageBox.Show("Текущая запись учебного заведения" +
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
                                MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                Model.Delete();
                ApplySearchCriteria();

                if (IsEditControl)
                {
                    SendBackMessage();
                }

            }

        }

        public void Back()
        {
            if (Model != null && Model.HasChanges)
            {
                MessageBoxResult result = MessageBox.Show("В текущую запись учебного заведения" + 
                                                          Environment.NewLine +
                                                          "были внесены изменения." + 
                                                          Environment.NewLine + Environment.NewLine +
                                                          "Сохранить внесенные изменения?",
                                                          "Сохранение текущей записи",
                                                          MessageBoxButton.YesNoCancel,
                                                          MessageBoxImage.Question,
                                                          MessageBoxResult.Yes);
                if (result != MessageBoxResult.Cancel)
                {
                    if (result == MessageBoxResult.Yes)
                    {
                        Model.Save();
                    }
                    else
                    {
                        Model.Rollback();
                    }

                    SendBackMessage();
                }

            }
            else
            {
                SendBackMessage();
            }

        }

        private void SendBackMessage()
        {
            if (Messenger != null)
            {
                IHighSchoolEntity oldSelectedItem = SelectedItem;
                Model.ApplySearchCriteria();

                if (oldSelectedItem != null && HighSchools.All(x => x.Id != oldSelectedItem.Id))
                {
                    MessageBox.Show("Критерии поиска не включают" + Environment.NewLine +
                                    "добавленные или измененные записи" + Environment.NewLine +
                                    "учебных заведений. Для их отображения" + Environment.NewLine +
                                    "измените критерии поиска.", 
                                    "Поиск записей учебных заведений",
                                    MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                }

                GoToSearchControl();
            }

        }

        private void GoToEditControl()
        {
            if (Messenger != null && !IsEditControl)
            {
                IsEditControl = true;
                Messenger.Send(CommandName.SetEntryControl, new MenuChangedEventArgs(MenuItemName.HighSchool));
            }

        }

        private void GoToSearchControl()
        {
            if (Messenger != null && IsEditControl)
            {
                IsEditControl = false;
                Messenger.Send(CommandName.SetEntryControl, new MenuChangedEventArgs(MenuItemName.HighSchool));
            }

        }

        private bool Validate()
        {
            bool result = true;

            if (Model.ValidateRequiredCode())
            {
                MessageBox.Show("Поле \"Код:\" не заполнено. Данное поле" + Environment.NewLine +
                                "является обязательным. Заполните это поле" + Environment.NewLine +
                                "и посторите попытку сохранения снова.",
                                "Ошибка сохранения!",
                                MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                result = false;
            }
            else if (Model.ValidateUniqueCode())
            {
                MessageBox.Show("Поле \"Код:\" является уникальным. Данное поле" + Environment.NewLine +
                                "содержит данные, которые уже были внесены в одну" + Environment.NewLine + 
                                "из сохраненных записей. Измените значение этого" + Environment.NewLine +
                                "поля и посторите попытку сохранения снова.",
                                "Ошибка сохранения!",
                                MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                result = false;
            }

            return result;
        }

        #endregion
    }
}
