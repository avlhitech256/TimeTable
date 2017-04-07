using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Common.Data.Criteria;
using Common.Data.Notifier;
using Common.Messenger;
using DataService.Entity.HighSchool;
using Domain.DomainContext;
using HighSchool.Model;
using HighSchool.ViewModel.Command;

namespace HighSchool.ViewModel
{
    public class HighSchoolViewModel : Notifier, IHighSchoolViewModel
    {
        #region Constructors
        public HighSchoolViewModel(IDomainContext context)
        {
            DomainContext = context;
            Model = new HighSchoolModel(context);
            Model.PropertyChanged += OnCangedSelectedHighSchool;
            InitializeButtons();
        }

        #endregion

        #region Properties

        private IHighSchoolModel Model { get; }

        public HighSchoolSearchCriteria SearchCriteria => Model.SearchCriteria;

        public IHighSchoolEntity SelectedHighSchool => Model.SelectedHighSchool;

        public ObservableCollection<IHighSchoolEntity> HighSchools => Model.HighSchools;

        public ObservableCollection<DataService.Model.Employee> Employees => Model.Employees;

        public IDomainContext DomainContext { get; }

        public IMessenger Messenger => DomainContext.Messenger;

        public string Code
        {
            get
            {
                return SelectedHighSchool.Code;
            }

            set
            {
                if (SelectedHighSchool.Code != value)
                {
                    SelectedHighSchool.Code = value;
                    OnPropertyChanged();
                }
            }

        }

        public string Name
        {
            get
            {
                return SelectedHighSchool.Name;
            }

            set
            {
                if (SelectedHighSchool.Name != value)
                {
                    SelectedHighSchool.Name = value;
                    OnPropertyChanged();
                }

            }
        }

        public bool Active
        {
            get
            {
                return SelectedHighSchool.Active;
            }

            set
            {
                if (SelectedHighSchool.Active != value)
                {
                    SelectedHighSchool.Active = value;
                    OnPropertyChanged();
                }

            }

        }

        public DateTimeOffset Cteated
        {
            get
            {
                return SelectedHighSchool.Cteated;
            }

            set
            {
                if (SelectedHighSchool.Cteated != value)
                {
                    SelectedHighSchool.Cteated = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTimeOffset LastModify
        {
            get
            {
                return SelectedHighSchool.LastModify;
            }

            set
            {
                if (SelectedHighSchool.LastModify != value)
                {
                    SelectedHighSchool.LastModify = value;
                    OnPropertyChanged();
                }
            }
        }

        public string UserModify
        {
            get
            {
                return SelectedHighSchool.UserModify;
            }

            set
            {
                if (SelectedHighSchool.UserModify != value)
                {
                    SelectedHighSchool.UserModify = value;
                    OnPropertyChanged();
                }

            }
        }

        public ICommand SearchButtonCommand { get; private set; }

        #endregion

        #region Methods

        private void InitializeButtons()
        {
            SearchButtonCommand = new SearchCommand(this);
        }

        private void OnCangedSelectedHighSchool(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Model.SelectedHighSchool))
            {
                OnPropertyChanged(nameof(SelectedHighSchool));
                OnPropertyChanged(nameof(Code));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Active));
                OnPropertyChanged(nameof(Cteated));
                OnPropertyChanged(nameof(LastModify));
                OnPropertyChanged(nameof(UserModify));
            }

        }

        public void ApplySearchCriteria()
        {
            Model.ApplySearchCriteria();
        }

        #endregion

    }
}
