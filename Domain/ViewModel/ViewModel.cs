﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Common.Data.Notifier;
using Common.Messenger;
using Common.Messenger.Impl;
using DataService.Entity;
using Domain.Data.Enum;
using Domain.Data.SearchCriteria;
using Domain.DomainContext;
using Domain.Event;
using Domain.Model;
using Domain.ViewModel.Command;
using ValueEnum = Domain.Data.Enum.ValueEnum;

namespace Domain.ViewModel
{
    public class ViewModel<T> : Notifier, IDataViewModel<T> where T : class 
    {
        #region Members

        private IDomainEntity<T> oldSelectedItem;
        private bool hasChanges;
        private bool readOnly;
        private bool isEditControl;
        private string toolTipForEditButton;
        private bool enabled;

        #endregion

        #region Constructors
        public ViewModel(IDomainContext context, IModel<T> model)
        {
            DomainContext = context;
            Model = model;
            DomainContext.DataBaseServer = Model.DataBaseServer;
            oldSelectedItem = Model?.SelectedItem;
            SubscribeEvents();
            ReadOnly = true;
            IsEditControl = false;
            StartEditToolTip = "Начать редактирование текущей записи";
            FinishEditToolTip = "Закончить редактирование текущей записи";
            InitializeButtons();
            InitializeProperties();
        }

        #endregion

        #region Properties

        protected IModel<T> Model { get; }

        public ISearchCriteria SearchCriteria => Model.SearchCriteria;

        public IDomainEntity<T> SelectedItem
        {
            get
            {
                return Model?.SelectedItem;
            }

            set
            {
                if (Model != null)
                {
                    Model.SelectedItem = value;
                }

            }

        }

        public ObservableCollection<IDomainEntity<T>> Entities => Model?.Entities;

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

        protected string StartEditToolTip { get; }
        protected string FinishEditToolTip { get; }

        public IMessenger Messenger => DomainContext.Messenger;

        public ICommand BackToSearchButtonCommand { get; private set; }

        public ICommand NewButtonCommand { get; private set; }

        public ICommand ViewButtonCommand { get; private set; }

        public ICommand EditButtonCommand { get; private set; }

        public ICommand SaveButtonCommand { get; private set; }

        public ICommand SaveAndNewButtonCommand { get; private set; }

        public ICommand DeleteButtonCommand { get; private set; }

        public ICommand SearchButtonCommand { get; private set; }

        public ICommand ClearButtonCommand { get; private set; }

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
                    ToolTipForEditButton = value ? FinishEditToolTip : StartEditToolTip;
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
                Model.PropertyChanged += OnChangedSelectedItem;
                oldSelectedItem = SelectedItem;

                if (SelectedItem != null)
                {
                    SelectedItem.PropertyChanged += OnChangedItemProperties;
                }

            }

        }

        private void InitializeButtons()
        {
            BackToSearchButtonCommand = new BackCommand<T>(this);
            NewButtonCommand = new AddCommand<T>(this);
            ViewButtonCommand = new ViewCommand<T>(this);
            EditButtonCommand = new EditCommand<T>(this);
            SaveButtonCommand = new SaveCommand<T>(this);
            SaveAndNewButtonCommand = new SaveAndNewCommand<T>(this);
            DeleteButtonCommand = new DeleteCommand<T>(this);
            SearchButtonCommand = new SearchCommand<T>(this);
            ClearButtonCommand = new ClearCommand<T>(this);
        }

        private void InitializeProperties()
        {
            ReadOnly = true;
            Enabled = false;
            ToolTipForEditButton = StartEditToolTip;
        }

        private void OnChangedSelectedItem(object sender, PropertyChangedEventArgs e)
        {
            if (Model != null && e.PropertyName == nameof(Model.SelectedItem))
            {
                if (oldSelectedItem != null)
                {
                    oldSelectedItem.PropertyChanged -= OnChangedItemProperties;
                }

                oldSelectedItem = SelectedItem;

                if (oldSelectedItem != null)
                {
                    oldSelectedItem.PropertyChanged += OnChangedItemProperties;
                }

                HasChanges = Model.HasChanges;
                OnPropertyChanged(nameof(SelectedItem));
            }

        }

        private void OnChangedItemProperties(object sender, PropertyChangedEventArgs e)
        {
            HasChanges = Model?.HasChanges ?? false;
        }

        public void ApplySearchCriteria()
        {
            Model.ApplySearchCriteria();
        }

        public void Clear()
        {
            SearchCriteria?.Clear();
            ApplySearchCriteria();
        }

        public void Add()
        {
            ReadOnly = false;
            GoToEditControl();
            HasChanges = Model?.HasChanges ?? false;
            Model?.Add();
        }

        public void View()
        {
            if (SelectedItem != null)
            {
                ReadOnly = true;
                GoToEditControl();
                HasChanges = Model?.HasChanges ?? false;
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
                HasChanges = Model?.HasChanges ?? false;
            }

        }

        public bool Save()
        {
            bool result = false;

            if (HasChanges && !ReadOnly && Validate() && Model != null)
            {
                ReadOnly = true;
                Model.Save();
                HasChanges = Model.HasChanges;
                result = true;
            }

            return result;
        }

        public void SaveAndAdd()
        {
            if (HasChanges && !ReadOnly)
            {
                if (Save())
                {
                    Add();
                }

            }

        }

        public void Delete()
        {
            Messenger?.Send(CommandName.RequestForDelete, new EventArgs());
        }

        public void SetResponseForDelete(ValueEnum response)
        {
            if (response == ValueEnum.Yes && Model != null)
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
            if (Model != null && Model.HasChanges && Messenger != null)
            {
                Messenger.Send(CommandName.RequestForBack, new EventArgs());
            }
            else
            {
                SendBackMessage();
            }

        }

        public void SetResponseForBack(ValueEnum response)
        {
            if (response != ValueEnum.Cancel)
            {
                if (response == ValueEnum.Yes)
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

        protected void SendBackMessage()
        {
            if (Messenger != null)
            {
                IDomainEntity<T> currentSelectedItem = SelectedItem;
                Model.ApplySearchCriteria();

                if (currentSelectedItem != null && Entities.All(x => x.Id != currentSelectedItem.Id))
                {
                    Messenger?.Send(CommandName.ShowMismatchSearchCriteriaMessage, new EventArgs());
                }

                GoToSearchControl();
            }

        }

        protected void GoToEditControl()
        {
            if (Messenger != null && !IsEditControl)
            {
                IsEditControl = true;
                Messenger.Send(CommandName.SetEntryControl, new MenuChangedEventArgs(MenuItemName.HighSchool));
            }

        }

        protected void GoToSearchControl()
        {
            if (Messenger != null && IsEditControl)
            {
                IsEditControl = false;
                Messenger.Send(CommandName.SetEntryControl, new MenuChangedEventArgs(MenuItemName.HighSchool));
            }

        }

        protected virtual bool Validate()
        {
            bool result = true;

            if (!Model.ValidateRequiredCode())
            {
                Messenger?.Send(CommandName.ShowInvalidRequiredCodeMessage, new EventArgs());
                result = false;
            }
            else if (!Model.ValidateUniqueCode())
            {
                Messenger?.Send(CommandName.ShowInvalidateUniqueCodeMessage, new EventArgs());
                result = false;
            }

            return result;
        }

        #endregion
    }

}