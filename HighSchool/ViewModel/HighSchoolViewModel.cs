using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Common.Data.Notifier;
using Domain.Data.Enum;
using Domain.Messenger;
using Domain.DomainContext;
using Domain.Entity.HighSchool;
using Domain.Entry;
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

        #endregion

        #region Constructors
        public HighSchoolViewModel(IDomainContext context)
        {
            DomainContext = context;
            Model = new HighSchoolModel(DomainContext);
            DomainContext.DataBaseServer = Model.DataBaseServer;
            oldHighSchool = Model?.SelectedHighSchool;
            SubscribeEvents();
            ReadOnly = true;
            IsEditControl = false;
            InitializeButtons();
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
        public long RectorId
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

        public ICommand ForwardButtonCommand { get; private set; }

        public ICommand NewButtonCommand { get; private set; }

        public ICommand EditButtonCommand { get; private set; }

        public ICommand SaveButtonCommand { get; private set; }

        public ICommand DeleteButtonCommand { get; private set; }

        public ICommand SearchButtonCommand { get; private set; }

        public ICommand ChangeEditModeButtonCommand { get; private set; }

        public bool ReadOnly { get; set; }

        public bool IsEditControl { get; set; }

        #endregion

        #region Methods

        private void SubscribeEvents()
        {
            if (Model != null)
            {
                Model.PropertyChanged += OnChangedSelectedHighSchool;

                if (SelectedItem != null)
                {
                    SelectedItem.PropertyChanged += OnChangedHighSchoolProperties;
                }

            }

        }
        private void InitializeButtons()
        {
            BackToSearchButtonCommand = new BackCommand(this);
            ForwardButtonCommand = null;
            NewButtonCommand = null;
            EditButtonCommand = new EditCommand(this);
            SaveButtonCommand = new SaveCommand(this);
            DeleteButtonCommand = new DeleteCommand(this);
            SearchButtonCommand = new SearchCommand(this);
            ChangeEditModeButtonCommand = null;
        }

        private void OnChangedSelectedHighSchool(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Model.SelectedHighSchool))
            {
                OnPropertyChanged(nameof(SelectedItem));
                OnPropertyChanged(nameof(Code));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Active));
                OnPropertyChanged(nameof(Created));
                OnPropertyChanged(nameof(LastModify));
                OnPropertyChanged(nameof(UserModify));

                if (oldHighSchool != null)
                {
                    oldHighSchool.PropertyChanged -= OnChangedHighSchoolProperties;
                }

                oldHighSchool = SelectedItem;

                if (oldHighSchool != null)
                {
                    oldHighSchool.PropertyChanged += OnChangedHighSchoolProperties;
                }

            }

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

        public void Edit()
        {
            if (Messenger != null)
            {
                ReadOnly = false;
                IsEditControl = true;
                Messenger.Send(CommandName.SetEntryControl, new MenuChangedEventArgs(MenuItemName.HighSchool));
                HasChanges = Model.HasChanges;
            }

        }

        public void Save()
        {
            Model.Save();
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
                SendBackMessage();
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
                IsEditControl = false;
                Messenger.Send(CommandName.SetEntryControl, new MenuChangedEventArgs(MenuItemName.HighSchool));
            }

        }

        #endregion

    }
}
