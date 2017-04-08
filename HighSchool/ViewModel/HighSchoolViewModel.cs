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

        public IHighSchoolEntity SelectedHighSchool
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
                return SelectedHighSchool?.Code;
            }

            set
            {
                if (SelectedHighSchool != null)
                {
                    SelectedHighSchool.Code = value;
                }

            }

        }

        public string Name
        {
            get
            {
                return SelectedHighSchool?.Name;
            }

            set
            {
                if (SelectedHighSchool != null)
                {
                    SelectedHighSchool.Name = value;
                }

            }

        }

        public bool Active
        {
            get
            {
                return SelectedHighSchool != null && SelectedHighSchool.Active;
            }

            set
            {
                if (SelectedHighSchool != null)
                {
                    SelectedHighSchool.Active = value;
                }

            }

        }
        public long RectorId
        {
            get
            {
                return SelectedHighSchool?.Rector ?? 0;
            }

            set
            {
                if (SelectedHighSchool != null)
                {
                    SelectedHighSchool.Rector = value;
                }
            }
        }

        public DateTime Created => SelectedHighSchool?.Created ?? DateTime.MinValue;

        public DateTime LastModify => SelectedHighSchool?.LastModify ?? DateTime.MinValue;

        public string UserModify => SelectedHighSchool?.UserModify;

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

                if (SelectedHighSchool != null)
                {
                    SelectedHighSchool.PropertyChanged += OnChangedHighSchoolProperties;
                }

            }

        }
        private void InitializeButtons()
        {
            BackButtonCommand = null;
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
                OnPropertyChanged(nameof(SelectedHighSchool));
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

                oldHighSchool = SelectedHighSchool;

                if (SelectedHighSchool != null)
                {
                    SelectedHighSchool.PropertyChanged += OnChangedHighSchoolProperties;
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
