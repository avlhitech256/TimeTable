using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Common.Data.Notifier;
using DataService.DataService;
using DataService.Model;
using Domain.DomainContext;
using Domain.Entity.HighSchool;
using HighSchool.ViewModel;

namespace HighSchool.Model
{
    public class HighSchoolModel : Notifier, IHighSchoolModel
    {
        #region Members

        private IHighSchoolEntity selectedHighSchool;
        private ObservableCollection<Employee> employees;

        #endregion

        #region Constructors

        public HighSchoolModel(IDomainContext domainContext)
        {
            DomainContext = domainContext;

            DataService = new DataService.DataService.DataService()
            {
                UserName = DomainContext.UserName
            };

            InitializeSearchCriteria();
            InitializeHighSchools();
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

        public ObservableCollection<IHighSchoolEntity> HighSchools { get; private set; }

        public ObservableCollection<Employee> Employees
        {
            get
            {
                if (employees == null)
                {
                    employees = new ObservableCollection<Employee>();
                    Employee item0 = new Employee() {Id = 0, Name = "Любой ректор"};
                    employees.Add(item0);
                    DbContext.Employees.OrderBy(x => x.Name).ToList().ForEach(x => employees.Add(x));
                    OnPropertyChanged();
                }

                return employees;
            }

        }
        public bool HasChanges => SelectedHighSchool != null && DbContext?.Entry(SelectedHighSchool.HighSchool)?.State != EntityState.Unchanged;
        public string DataBaseServer => DbContext?.Database?.Connection?.DataSource;

        private IDomainContext DomainContext { get; }

        private IDataService DataService { get; }

        private TimeTableEntities DbContext => DataService?.DBContext;

        #endregion

        #region Methods

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
            HighSchools = new ObservableCollection<IHighSchoolEntity>();
            long position = 1;

            foreach (DataService.Model.HighSchool item in DbContext.HighSchools.ToList())
            {
                HighSchools.Add(new HighSchoolEntity(DataService, item, position));
            }

        }

        public void ApplySearchCriteria()
        {
            HighSchools.Clear();
            long position = 1;

            foreach (DataService.Model.HighSchool item in DbContext.HighSchools.ToList()
                .Where(x => string.IsNullOrWhiteSpace(SearchCriteria.Code) || 
                            x.Code.ToUpperInvariant().Contains(SearchCriteria.Code.ToUpperInvariant())).ToList()
                .Where(x => string.IsNullOrWhiteSpace(SearchCriteria.Name) || 
                            x.Name.ToUpperInvariant().Contains(SearchCriteria.Name.ToUpperInvariant())).ToList()
                .Where(x => !SearchCriteria.Active || x.Active).ToList()
                .Where(x => (!SearchCriteria.CteatedFrom.HasValue || x.Created >= SearchCriteria.CteatedFrom.Value) && 
                            (!SearchCriteria.CteatedTo.HasValue || x.Created < SearchCriteria.CteatedTo.Value.AddDays(1))).ToList()
                .Where(x => (!SearchCriteria.LastModifyFrom.HasValue || x.LastModify >= SearchCriteria.LastModifyFrom.Value) &&
                            (!SearchCriteria.LastModifyTo.HasValue || x.LastModify < SearchCriteria.LastModifyTo.Value.AddDays(1))).ToList()
                .Where(x => string.IsNullOrWhiteSpace(SearchCriteria.UserModify) || 
                            x.UserModify.ToUpperInvariant().Contains(SearchCriteria.UserModify.ToUpperInvariant())).ToList()
                .Where(x => SearchCriteria.RectorId <= 0L || x.Rector == SearchCriteria.RectorId))
            {
                HighSchools.Add(new HighSchoolEntity(DataService, item, position));
            }

            OnPropertyChanged(nameof(HighSchools));
        }

        public void Add()
        {
            SelectedHighSchool = new HighSchoolEntity(DataService);
        }

        public void Rollback()
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
                        break;
                }

            }

        }

        public void Save()
        {
            DbContext.SaveChanges();
        }

        public void Delete()
        {
            if (SelectedHighSchool != null)
            {
                DbContext.HighSchools.Remove(SelectedHighSchool.HighSchool);
                DbContext.SaveChanges();
                SelectedHighSchool = null;
            }

        }

        #endregion

    }

}
