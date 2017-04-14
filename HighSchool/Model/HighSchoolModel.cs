using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Common.Data.Notifier;
using DataService.DataService;
using DataService.Model;
using Domain.DomainContext;
using Domain.Entity.HighSchool;
using Domain.Event;
using HighSchool.ViewModel;

namespace HighSchool.Model
{
    public class HighSchoolModel : Notifier, IHighSchoolModel
    {
        #region Members

        private IHighSchoolEntity selectedHighSchool;
        private ObservableCollection<Employee> employees;
        private ObservableCollection<IHighSchoolEntity> highSchools;
        private bool employeesIsLoaded;
        private bool highSchoolsIsLoaded;

        #endregion

        #region Constructors

        public HighSchoolModel(IDomainContext domainContext)
        {
            DomainContext = domainContext;
            employeesIsLoaded = false;
            highSchoolsIsLoaded = false;
            InitializeDataService();
            InitializeSearchCriteria();
        }

        #endregion

        #region Properties

        public HighSchoolSearchCriteria SearchCriteria { get; private set; }

        public IHighSchoolEntity SelectedHighSchool
        {
            get
            {
                return selectedHighSchool;
            }

            set
            {
                if (selectedHighSchool != value)
                {
                    selectedHighSchool = value;
                    OnPropertyChanged();
                }

            }

        }

        public ObservableCollection<IHighSchoolEntity> HighSchools
        {
            get
            {
                if (!highSchoolsIsLoaded && highSchools == null)
                {
                    InitializeHighSchools();
                    highSchoolsIsLoaded = true;
                }

                return highSchools;
            }

            private set
            {
                if (highSchools != value)
                {
                    highSchools = value;
                    OnPropertyChanged();
                }

            }

        }

        public ObservableCollection<Employee> Employees
        {
            get
            {
                if (!employeesIsLoaded && employees == null)
                {
                    try
                    {
                        employees = new ObservableCollection<Employee>();
                        Employee item0 = new Employee() { Id = 0, Name = "Любой ректор" };
                        employees.Add(item0);
                        DbContext.Employees.OrderBy(x => x.Name).ToList().ForEach(x => employees.Add(x));
                        OnPropertyChanged();
                        employeesIsLoaded = true;
                    }
                    catch (EntityException e)
                    {
                        OnEntityException(e);
                    }

                }

                return employees;
            }

        }

        public bool HasChanges
        {
            get
            {
                bool hasChanges = false;

                try
                {
                    hasChanges = SelectedHighSchool != null && DbContext?.Entry(SelectedHighSchool.HighSchool)?.State != EntityState.Unchanged;
                }
                catch (EntityException e)
                {
                    OnEntityException(e);
                }

                return hasChanges;
            }

        }

        public string DataBaseServer
        {
            get
            {
                string dataBaseServer = string.Empty;

                try
                {
                    dataBaseServer = DbContext?.Database?.Connection?.DataSource;
                }
                catch (EntityException e)
                {
                    OnEntityException(e);
                }

                return dataBaseServer;
            }

        }

        private IDomainContext DomainContext { get; }

        private IDataService DataService { get; set; }

        private TimeTableEntities DbContext => DataService?.DBContext;

        #endregion

        #region Methods

        private void InitializeDataService()
        {
            try
            {
                DataService = new DataService.DataService.DataService
                {
                    UserName = DomainContext.UserName
                };

            }
            catch (EntityException e)
            {
                OnEntityException(e);
            }

        }

        private void InitializeSearchCriteria()
        {
            SearchCriteria = new HighSchoolSearchCriteria
            {
                Code = string.Empty,
                Name = string.Empty,
                Active = true,
                CteatedTo = null,
                CteatedFrom = null,
                LastModifyTo = null,
                LastModifyFrom = null,
                UserModify = string.Empty
            };
        }

        private void InitializeHighSchools()
        {
            var tempHighSchools = new ObservableCollection<IHighSchoolEntity>();
            long position = 1;

            try
            {
                foreach (DataService.Model.HighSchool item in DbContext.HighSchools.ToList())
                {
                    tempHighSchools.Add(new HighSchoolEntity(DataService, DomainContext.Messenger, item, position));
                }

                HighSchools = tempHighSchools;
            }
            catch (EntityException e)
            {
                OnEntityException(e);
            }

        }

        public void ApplySearchCriteria()
        {
            HighSchools.Clear();
            long position = 1;

            try
            {
                List<DataService.Model.HighSchool> selectedListHighSchool =
                    DbContext.HighSchools.ToList()
                    .Where(x => string.IsNullOrWhiteSpace(SearchCriteria.Code) ||
                                x.Code.ToUpperInvariant().Contains(SearchCriteria.Code.ToUpperInvariant())).ToList()
                    .Where(x => string.IsNullOrWhiteSpace(SearchCriteria.Name) ||
                                x.Name.ToUpperInvariant().Contains(SearchCriteria.Name.ToUpperInvariant())).ToList()
                    .Where(x => !SearchCriteria.Active || x.Active).ToList()
                    .Where(x => (!SearchCriteria.CteatedFrom.HasValue || x.Created >= SearchCriteria.CteatedFrom.Value) &&
                             (!SearchCriteria.CteatedTo.HasValue ||
                              x.Created < SearchCriteria.CteatedTo.Value.AddDays(1))).ToList()
                    .Where(x => (!SearchCriteria.LastModifyFrom.HasValue ||
                                 x.LastModify >= SearchCriteria.LastModifyFrom.Value) &&
                                (!SearchCriteria.LastModifyTo.HasValue ||
                                 x.LastModify < SearchCriteria.LastModifyTo.Value.AddDays(1))).ToList()
                    .Where(x => string.IsNullOrWhiteSpace(SearchCriteria.UserModify) ||
                                x.UserModify.ToUpperInvariant().Contains(SearchCriteria.UserModify.ToUpperInvariant()))
                                .ToList()
                    .Where(x => SearchCriteria.RectorId <= 0L || x.Rector == SearchCriteria.RectorId).ToList();

                foreach (DataService.Model.HighSchool item in selectedListHighSchool)
                {
                    HighSchools.Add(new HighSchoolEntity(DataService, DomainContext.Messenger, item, position));
                }

                OnPropertyChanged(nameof(HighSchools));
            }
            catch (EntityException e)
            {
                OnEntityException(e);
            }

        }

        public void Add()
        {
            SelectedHighSchool = new HighSchoolEntity(DataService, DomainContext.Messenger);
        }

        public void Rollback()
        {
            try
            {
                if (DbContext != null)
                {
                    DbEntityEntry entry = DbContext.Entry(SelectedHighSchool.HighSchool);

                    if (entry != null)
                    {
                        switch (entry.State)
                        {
                            case EntityState.Modified:
                                entry.State = EntityState.Unchanged;
                                break;
                            case EntityState.Deleted:
                                entry.Reload();
                                break;
                            case EntityState.Added:
                                entry.State = EntityState.Detached;
                                SelectedHighSchool = null;
                                break;
                        }

                    }

                    Save();
                }

            }
            catch (EntityException e)
            {
                OnEntityException(e);
            }

        }

        public bool ValidateRequiredCode()
        {
            return string.IsNullOrWhiteSpace(SelectedHighSchool.Code);
        }

        public bool ValidateUniqueCode()
        {
            return DbContext.HighSchools.ToList().All(x => x.Code != SelectedHighSchool.Code);
        }

        public void Save()
        {
            try
            {
                if (HasChanges)
                {
                    DbContext.SaveChanges();
                }

            }
            catch (EntityException e)
            {
                OnEntityException(e);
            }

        }

        public void Delete()
        {
            try
            {
                if (SelectedHighSchool != null)
                {
                    DbContext.HighSchools.Remove(SelectedHighSchool.HighSchool);
                    Save();
                    SelectedHighSchool = null;
                }

            }
            catch (EntityException e)
            {
                OnEntityException(e);
            }

        }

        private void OnEntityException(EntityException e)
        {
            EntityException?.Invoke(this, new EntityExceptionEventArgs(e));
        }

        #endregion

        #region Events

        public event EntityExceptionEventHandler EntityException = delegate { };

        #endregion

    }

}
