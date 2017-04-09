using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Common.Data.Notifier;
using Domain.Messenger;
using Domain.DomainContext;
using Domain.Entity.HighSchool;
using Domain.Entry;
using HighSchool.Model;
using HighSchool.ViewModel.Command;

namespace HighSchool.ViewModel
{
    public class HighSchoolViewModel : Notifier, IHighSchoolViewModel
    {
        #region Members

        private IHighSchoolEntity oldHighSchool;
        
        #endregion

        #region Constructors
        public HighSchoolViewModel(IDomainContext context)
        {
            DomainContext = context;
            Model = new HighSchoolModel(context);
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

        public ICommand BackButtonCommand { get; private set; }

        public ICommand ForwardButtonCommand { get; private set; }

        public ICommand NewButtonCommand { get; private set; }

        public ICommand EditButtonCommand { get; private set; }

        public ICommand SaveButtonCommand { get; private set; }

        public ICommand SearchButtonCommand { get; private set; }

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
            BackButtonCommand = new BackCommand(this);
            ForwardButtonCommand = null;
            NewButtonCommand = null;
            EditButtonCommand = new EditCommand(this);
            SaveButtonCommand = null;
            SearchButtonCommand = new SearchCommand(this);
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

                if (SelectedItem != null)
                {
                    SelectedItem.PropertyChanged += OnChangedHighSchoolProperties;
                }

            }

        }

        private void OnChangedHighSchoolProperties(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        public void ApplySearchCriteria()
        {
            Model.ApplySearchCriteria();
        }

        #endregion

    }
}
