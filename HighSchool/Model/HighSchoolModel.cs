using System.Collections.ObjectModel;
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

        #endregion

        #region Constructors

        public HighSchoolModel(IDomainContext domainContext)
        {
            DomainContext = domainContext;
            InitializeSearchCriteria();
            InitializeHighSchools();
            InitializeEmployee();
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

            private set
            {
                if (selectedHighSchool != value)
                {
                    selectedHighSchool = value;
                    OnPropertyChanged();
                }

            }

        }

        public ObservableCollection<IHighSchoolEntity> HighSchools { get; private set; }

        public ObservableCollection<Employee> Employees { get; private set; }

        private IDomainContext DomainContext { get; }

        private IDataService DataService => DomainContext.DataService;

        private TimeTableEntities DBContext => DataService.DBContext;

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

            foreach (DataService.Model.HighSchool item in DBContext.HighSchools)
            {
                HighSchools.Add(new HighSchoolEntity(DomainContext, item, position));
            }

        }

        private void InitializeEmployee()
        {
            Employees = new ObservableCollection<Employee>();

            foreach (Employee item in DBContext.Employees)
            {
                Employees.Add(item);
            }

        }

        public void ApplySearchCriteria()
        {
            HighSchools.Clear();
            long position = 1;

            foreach (DataService.Model.HighSchool item in DBContext.HighSchools.ToList()
                .Where(x => string.IsNullOrWhiteSpace(SearchCriteria.Code) || 
                            x.Code.ToUpperInvariant().Contains(SearchCriteria.Code.ToUpperInvariant())).ToList()
                .Where(x => string.IsNullOrWhiteSpace(SearchCriteria.Name) || 
                            x.Name.ToUpperInvariant().Contains(SearchCriteria.Name.ToUpperInvariant())).ToList()
                .Where(x => !SearchCriteria.Active || x.Active).ToList()
                .Where(x => (!SearchCriteria.CteatedFrom.HasValue || x.Cteated >= SearchCriteria.CteatedFrom.Value) && 
                            (!SearchCriteria.CteatedTo.HasValue || x.Cteated < SearchCriteria.CteatedTo.Value.AddDays(1))).ToList()
                .Where(x => (!SearchCriteria.LastModifyFrom.HasValue || x.LastModify >= SearchCriteria.LastModifyFrom.Value) &&
                            (!SearchCriteria.LastModifyTo.HasValue || x.LastModify < SearchCriteria.LastModifyTo.Value.AddDays(1))).ToList()
                .Where(x => string.IsNullOrWhiteSpace(SearchCriteria.UserModify) || 
                            x.UserModify.ToUpperInvariant().Contains(SearchCriteria.UserModify.ToUpperInvariant())).ToList()
                .Where(x => SearchCriteria.RectorId <= 0L || x.Id == SearchCriteria.RectorId))
            {
                HighSchools.Add(new HighSchoolEntity(DomainContext, item, position));
            }

            OnPropertyChanged(nameof(HighSchools));

            if (HighSchools.Count == 1)
            {
                SelectedHighSchool = HighSchools[0];
            }

        }

        private IHighSchoolEntity GetHighSchool(long id)
        {
            IHighSchoolEntity highSchool =
                HighSchools.FirstOrDefault(x => x.Id == id);
            return highSchool;
        }

        public void UpdateHighSchool()
        {
            
        }

        #endregion

    }

}
