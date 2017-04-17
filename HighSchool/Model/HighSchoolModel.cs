using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Common.Data.Notifier;
using Common.DomainContext;
using Common.Event;
using DataService.DataService;
using DataService.Entity.HighSchool;
using DataService.Model;
using Domain.Model;
using Domain.SearchCriteria;
using Domain.SearchCriteria.HighSchool;

namespace HighSchool.Model
{
    public class HighSchoolModel : CommonModel<DataService.Model.HighSchool>, IHighSchoolModel, INotifyPropertyChanged
    {
        #region Members

        //private IHighSchoolEntity selectedItem;
        private ObservableCollection<Employee> employees;
        //private ObservableCollection<IHighSchoolEntity> entities;
        private bool employeesIsLoaded;
        //private bool entitiesIsLoaded;

        #endregion

        #region Constructors

        public HighSchoolModel(IDomainContext domainContext) : base(domainContext)
        {
            //DomainContext = domainContext;
            employeesIsLoaded = false;
            //entitiesIsLoaded = false;
            //InitializeDataService();
            //InitializeSearchCriteria();
        }

        #endregion

        #region Properties

        //public ISearchCriteria SearchCriteria { get; private set; }

        //public IHighSchoolEntity SelectedItem
        //{
        //    get
        //    {
        //        return selectedItem;
        //    }

        //    set
        //    {
        //        if (selectedItem != value)
        //        {
        //            selectedItem = value;
        //            OnPropertyChanged();
        //        }

        //    }

        //}

        //public ObservableCollection<IHighSchoolEntity> Entities
        //{
        //    get
        //    {
        //        if (!entitiesIsLoaded && entities == null)
        //        {
        //            InitializeHighSchools();
        //            entitiesIsLoaded = true;
        //        }

        //        return entities;
        //    }

        //    private set
        //    {
        //        if (entities != value)
        //        {
        //            entities = value;
        //            OnPropertyChanged();
        //        }

        //    }

        //}

        public ObservableCollection<Employee> Employees
        {
            get
            {
                if (!employeesIsLoaded)
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

        //public bool HasChanges
        //{
        //    get
        //    {
        //        bool hasChanges = false;

        //        try
        //        {
        //            hasChanges = SelectedItem != null && DbContext?.Entry(SelectedItem.Entity)?.State != EntityState.Unchanged;
        //        }
        //        catch (EntityException e)
        //        {
        //            OnEntityException(e);
        //        }

        //        return hasChanges;
        //    }

        //}

        //public string DataBaseServer
        //{
        //    get
        //    {
        //        string dataBaseServer = string.Empty;

        //        try
        //        {
        //            dataBaseServer = DbContext?.Database?.Connection?.DataSource;
        //        }
        //        catch (EntityException e)
        //        {
        //            OnEntityException(e);
        //        }

        //        return dataBaseServer;
        //    }

        //}

        //private IDomainContext DomainContext { get; }

        //private IDataService DataService { get; set; }

        //private TimeTableEntities DbContext => DataService?.DBContext;

        #endregion

        #region Methods

        //private void InitializeDataService()
        //{
        //    try
        //    {
        //        DataService = new DataService.DataService.DataService
        //        {
        //            UserName = DomainContext.UserName
        //        };

        //    }
        //    catch (EntityException e)
        //    {
        //        OnEntityException(e);
        //    }

        //}

        //private void InitializeSearchCriteria()
        //{
        //    SearchCriteria = new HighSchoolSearchCriteria
        //    {
        //        Code = string.Empty,
        //        Name = string.Empty,
        //        Active = true,
        //        CteatedTo = null,
        //        CteatedFrom = null,
        //        LastModifyTo = null,
        //        LastModifyFrom = null,
        //        UserModify = string.Empty
        //    };
        //}

        //private void InitializeHighSchools()
        //{
        //    var tempHighSchools = new ObservableCollection<IHighSchoolEntity>();
        //    long position = 1;

        //    try
        //    {
        //        foreach (DataService.Model.HighSchool item in DbContext.HighSchools.ToList())
        //        {
        //            tempHighSchools.Add(new HighSchoolEntity(DataService, DomainContext.Messenger, item, position));
        //        }

        //        Entities = tempHighSchools;
        //    }
        //    catch (EntityException e)
        //    {
        //        OnEntityException(e);
        //    }

        //}

        protected override List<DataService.Model.HighSchool> SelectEntities()
        {
            List<DataService.Model.HighSchool> result = base.SelectEntities();
            HighSchoolSearchCriteria searchCriteria = SearchCriteria as HighSchoolSearchCriteria;

            if (searchCriteria != null)
            {
                result = base.SelectEntities()
                    .Where(x => string.IsNullOrWhiteSpace(searchCriteria.Code) ||
                                x.Code.ToUpperInvariant()
                                    .Contains(searchCriteria.Code.ToUpperInvariant())).ToList()
                    .Where(x => string.IsNullOrWhiteSpace(searchCriteria.Name) ||
                                x.Name.ToUpperInvariant()
                                    .Contains(searchCriteria.Name.ToUpperInvariant())).ToList()
                    .Where(x => !searchCriteria.Active || x.Active).ToList()
                    .Where(x => (!searchCriteria.CteatedFrom.HasValue ||
                                 x.Created >= searchCriteria.CteatedFrom.Value) &&
                                (!searchCriteria.CteatedTo.HasValue ||
                                 x.Created < searchCriteria.CteatedTo.Value.AddDays(1))).ToList()
                    .Where(x => (!searchCriteria.LastModifyFrom.HasValue ||
                                 x.LastModify >= searchCriteria.LastModifyFrom.Value) &&
                                (!searchCriteria.LastModifyTo.HasValue ||
                                 x.LastModify < searchCriteria.LastModifyTo.Value.AddDays(1))).ToList()
                    .Where(x => string.IsNullOrWhiteSpace(searchCriteria.UserModify) ||
                                x.UserModify.ToUpperInvariant()
                                    .Contains(searchCriteria.UserModify.ToUpperInvariant())).ToList()
                    .Where(x => searchCriteria.RectorId <= 0L || x.Rector == searchCriteria.RectorId).ToList();
            }

            return result;
        }

        //public void ApplySearchCriteria()
        //{
        //    Entities.Clear();
        //    long position = 1;

        //    try
        //    {
        //        HighSchoolSearchCriteria searchCriteria = SearchCriteria as HighSchoolSearchCriteria;

        //        if (searchCriteria != null)
        //        {
        //            List<DataService.Model.HighSchool> selectedListHighSchool =
        //                DbContext.HighSchools.ToList()
        //                    .Where(x => string.IsNullOrWhiteSpace(searchCriteria.Code) ||
        //                                x.Code.ToUpperInvariant().Contains(searchCriteria.Code.ToUpperInvariant()))
        //                    .ToList()
        //                    .Where(x => string.IsNullOrWhiteSpace(searchCriteria.Name) ||
        //                                x.Name.ToUpperInvariant().Contains(searchCriteria.Name.ToUpperInvariant()))
        //                    .ToList()
        //                    .Where(x => !searchCriteria.Active || x.Active).ToList()
        //                    .Where(
        //                        x =>
        //                            (!searchCriteria.CteatedFrom.HasValue ||
        //                             x.Created >= searchCriteria.CteatedFrom.Value) &&
        //                            (!searchCriteria.CteatedTo.HasValue ||
        //                             x.Created < searchCriteria.CteatedTo.Value.AddDays(1))).ToList()
        //                    .Where(x => (!searchCriteria.LastModifyFrom.HasValue ||
        //                                 x.LastModify >= searchCriteria.LastModifyFrom.Value) &&
        //                                (!searchCriteria.LastModifyTo.HasValue ||
        //                                 x.LastModify < searchCriteria.LastModifyTo.Value.AddDays(1))).ToList()
        //                    .Where(x => string.IsNullOrWhiteSpace(searchCriteria.UserModify) ||
        //                                x.UserModify.ToUpperInvariant()
        //                                    .Contains(searchCriteria.UserModify.ToUpperInvariant()))
        //                    .ToList()
        //                    .Where(x => searchCriteria.RectorId <= 0L || x.Rector == searchCriteria.RectorId).ToList();

        //            foreach (DataService.Model.HighSchool item in selectedListHighSchool)
        //            {
        //                Entities.Add(new HighSchoolEntity(DataService, DomainContext.Messenger, item, position));
        //            }

        //            OnPropertyChanged(nameof(Entities));
        //        }
        //    }
        //    catch (EntityException e)
        //    {
        //        OnEntityException(e);
        //    }

        //}

        //public void Add()
        //{
        //    SelectedItem = new HighSchoolEntity(DataService, DomainContext.Messenger);
        //}

        //public void Rollback()
        //{
        //    try
        //    {
        //        if (DbContext != null)
        //        {
        //            DbEntityEntry entry = DbContext.Entry(SelectedItem.Entity);

        //            if (entry != null)
        //            {
        //                switch (entry.State)
        //                {
        //                    case EntityState.Modified:
        //                        entry.State = EntityState.Unchanged;
        //                        break;
        //                    case EntityState.Deleted:
        //                        entry.Reload();
        //                        break;
        //                    case EntityState.Added:
        //                        entry.State = EntityState.Detached;
        //                        SelectedItem = null;
        //                        break;
        //                }

        //            }

        //            Save();
        //        }

        //    }
        //    catch (EntityException e)
        //    {
        //        OnEntityException(e);
        //    }

        //}

        //public bool ValidateRequiredCode()
        //{
        //    return string.IsNullOrWhiteSpace(SelectedItem.Code);
        //}

        //public bool ValidateUniqueCode()
        //{
        //    return DbContext.HighSchools.ToList().All(x => x.Code != SelectedItem.Code);
        //}

        //public void Save()
        //{
        //    try
        //    {
        //        if (HasChanges)
        //        {
        //            DbContext.SaveChanges();
        //        }

        //    }
        //    catch (EntityException e)
        //    {
        //        OnEntityException(e);
        //    }

        //}

        //public void Delete()
        //{
        //    try
        //    {
        //        if (SelectedItem != null)
        //        {
        //            DbContext.HighSchools.Remove(SelectedItem.Entity);
        //            Save();
        //            SelectedItem = null;
        //        }

        //    }
        //    catch (EntityException e)
        //    {
        //        OnEntityException(e);
        //    }

        //}

        //private void OnEntityException(EntityException e)
        //{
        //    EntityException?.Invoke(this, new EntityExceptionEventArgs(e));
        //}

        #endregion

        //#region Events

        //public event EntityExceptionEventHandler EntityException = delegate { };

        //#endregion

    }

}
